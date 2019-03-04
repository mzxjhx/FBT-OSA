using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using OsaServer;

using SharpPlus.Ini;
using System.IO;
using System.Diagnostics;
using System.IO.Ports;
/***************************************************************************
 本版本程序说明 ：
 
     由于服务器本身通过网络与OSA连接，所以服务器本身对于OSA仪器来说是客户端，
 程序整体分成了数据共享的网络服务器主机和OSA客户端两部分，均采用TCP协议连接
 通讯。
    客户端的请求命令包括：测试、重测、测试参考值及保存(完成)。
    软件目前要求将生产线上合格品与不合格品数据全部保存下来，所以每次测试完的数据要自动保存
 保存命令可以解释为该客户端测试完成。
        
    * 客户端==>服务器通讯协议：
    * "IP+Port+测试阶段+起始波长+终止波长" ，以"+"分隔  Save \ finish 

    * 服务器==>客户端通讯协议：
    * "Status=状态"  状态：Idle、Use、Wait三种
    * "Data=数据"         表示接收到的是数据，分别是X轴数据和Y轴数据
 * 服务器所用到OSA测试设置参数，为简单起见，以ini文件形式保存，
 * 
 *
 * 修改日期：2013-9.11
 * 修改说明：
 * 按照生产线上CWDM的工艺流程，OSA测试是倒数第二道工序，仅做熔接光纤、常温、低温和高温测试，
 * 而OSA扫描一次的时间很短，设置一个当前用户，一个等待用户，只接收扫描命令，不保存数据。
 * 将数据返回，交由客户端显示保存
 *
 * 服务器只负责数据交换，由客户端各自保存自己测试的数据
 * 
 * 添加一个user临时变量，设置一个等待用户。因为实测中走高温和低温都需要2分秒等待
 * 时间，2分秒可以扫描很多次。为提高效率，必须添加等待用列表
 * 
 * 修改日期：2013-9-12
 *     两个客户端测试，一个等待用户情况下服务器运行正常
 * 
 * 修改日期：2013-9-23
 *     将OsaServer类、User类和Step枚举从主函数中分离出来，便于管理
 *     
 * 9-24
 *      按工作台地址添加工作客户端列表          这个有问题
 *      新增判断连接客户端是否在当前用户列表中
 * 2014-5-26
 *      添加DWDM扫描，扫描范围1525-1610，分辨率0.02nm，扫描点数21500
 *      
 * 2015-5-27
	版本号"1.15.0.6"
	CWDM和DWDM两种器件扫描方案改变。
	C系列10001点，0.5nm分辨率，mid灵敏度
	D系列5001点，0.05nm分辨率，high1灵敏度
****************************************************************************/
namespace OsaServer
{
    public partial class FormOSAServer : Form
    {
        public FormOSAServer()
        {
            InitializeComponent();
        }

        Server server = new Server();

        OSA OsaServer = new OSA();

        string _OsaIp = "";
        int _OsaPort = 10001;

        //扫描起始波长和
        private string _startWave = "1260";
        /// <summary>
        /// 终止波长
        /// </summary>
        private string _stopWave = "1650";
        /// <summary>
        /// 采样点数
        /// </summary>
        private string _points = "10001";

        private string _res = "1NM";

        /// <summary>
        /// 光源光开关
        /// </summary>
        private string _SourceCOM = "COM1";
        /// <summary>
        /// OSA开关
        /// </summary>
        private string _OSACOM = "";

        //保存连接的所有用户
        private List<User> userList = new List<User>();

        //  等待用户列表
        private List<User> userWait = new List<User>();

        //全局变量，记录当前客户端，以传送数据
        User userCurrent = null;

        //配置文件初始化
        private IniFile Config = new IniFile();
        private String address = Application.StartupPath + "\\config.ini";

        /// <summary>
        /// OSA切换光开关通讯命令，四川绵羊版本
        /// </summary>
        string[] Switch = new string[4] 
        { 
            "<AD01_S_01>",
            "<AD01_S_02>",
            "<AD01_S_03>",
            "<AD01_S_04>"
        };

        //程序初始化
        private void FormTCPServer_Load(object sender, EventArgs e)
        {

            Config.FileName = address;
            ReadConfig();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            try
            {
                serialPortOsa.PortName = _OSACOM;// portNames[1];
                serialPortAse.PortName = _SourceCOM;// portNames[2];   

                if (!serialPortAse.IsOpen)
                {
                    PortAse_Open();
                }
                if (!serialPortOsa.IsOpen)
                {
                    PortOsa_Open();
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(" 找不到扩展RS232接口！", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                MessageBox.Show(" 串口错误：" + ex.Message);
                return;
            }

            //一拖四，定义用户最大数量为8个，等待用户为3个。
            userList.Capacity = 8;
            userWait.Capacity = 4;

            FormClosing += new FormClosingEventHandler(FormOSAServer_FormClosing);
            server.FindUserHandler += ListenClient;
        }

        void FormOSAServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //server.Close();
            //OsaServer.Close();
            try
            {
                server.Close();
                foreach (User user in userList)
                {
                    user.SendCmd("Status=Off");
                    user.Close();
                }
                //清空几个用户列表
                userWait.Clear();
                userList.Clear();
                labelWait.Text = "";
                richTextBoxStatus.AppendText("  OSA服务器已关闭  \r\n");

            }
            catch (SocketException )
            {
                return;
            }
            catch (IOException )
            {
                return;
            }

            try
            {
                if (OsaServer != null)
                {
                    OsaServer.Close();
                }
                //richTextBoxStatus.AppendText(" 断开OSA连接 ！\r\n");
            }
            catch { }
        }

        /// <summary>
        /// 扫描配置
        /// </summary>
        private void ReadConfig()
        {
            _startWave = Config.ReadString("Sweep", "StartWave", "1");
            _stopWave = Config.ReadString("Sweep", "StopWave", "1");

            _OsaIp = Config.ReadString("OSA", "IP", "192.168.0.100");
            _SourceCOM = Config.ReadString("COM", "Source", "com1");
            _OSACOM = Config.ReadString("COM", "OSA", "com2");

            ipAddressOSA.Text = _OsaIp;
            //ipAddressServer.Text = _ServerIP.ToString();
            comboBoxSource.Text = _SourceCOM;
            comboBoxOSA.Text = _OSACOM;

            OsaServer.Csweepcfg.Points = Config.ReadString("Sweep", "CPoints", "10001");
            textBoxCPoint.Text = OsaServer.Csweepcfg.Points;

            OsaServer.Csweepcfg.Res = Config.ReadString("Sweep", "CRes", "0.5nm");
            comboBoxCRes.Text = OsaServer.Csweepcfg.Res;

            OsaServer.Csweepcfg.Sens = Config.ReadString("Sweep", "Csens", "mid");
            comboBoxCSens.Text = OsaServer.Csweepcfg.Sens;

            OsaServer.Dsweepcfg.Points = Config.ReadString("Sweep", "DPoints", "5001");
            textBoxDpoint.Text = OsaServer.Dsweepcfg.Points;

            OsaServer.Dsweepcfg.Res = Config.ReadString("Sweep", "DRes", "0.05nm");
            comboBoxDres.Text = OsaServer.Dsweepcfg.Res;

            OsaServer.Dsweepcfg.Sens = Config.ReadString("Sweep", "Dsens", "high1");
            comboBoxDses.Text = OsaServer.Dsweepcfg.Sens;
        }

        public void PortAse_Open()
        {
            try
            {
                serialPortAse.BaudRate = 9600;
                serialPortAse.ReadBufferSize = 20;
                serialPortAse.DataBits = 8;
                serialPortAse.Open();

            }
            catch (Exception)
            {
                MessageBox.Show("串口被占用");
            }
        }

        private void PortOsa_Open()
        {
            try
            {
                serialPortOsa.BaudRate = 9600;
                serialPortOsa.ReadBufferSize = 20;
                serialPortOsa.DataBits = 8;
                serialPortOsa.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("串口被占用");
            }
        }

        /// <summary>
        /// 讯泉1*2光开关通讯函数： 0xcd 0x01  0xcd 0x02
        /// </summary>
        /// <param name="i"></param>
        private void ChannelChange(int ccc)
        {
            try
            {
                byte[] bbb = new byte[2];
                bbb[0] = 0xcd;
                bbb[1] = (byte)ccc;

                serialPortAse.Write(bbb, 0, 2);
                serialPortOsa.Write(bbb, 0, 2);
            }
            catch
            {

            }
        }


        /// <summary>
        /// 监听线程，新建Client
        /// </summary>
        private void ListenClient(TcpClient newClient)
        {
            //每接收一个客户连接，创建一个线程接收客户端消息
            User user = new User(newClient);
            //客户端地址  IP:Port  后期改为只记IP
            user.IpAddress = user.Client.Client.RemoteEndPoint.ToString().Split(':')[0];

            //读配置文件里客户端通道地址 ， 根据IP确定OSA及ASE光开关所要切换通道
            user.UserChannel = Int16.Parse(Config.ReadString(user.IpAddress, "channel", "1"));
            //任何其它IP加入，均设置通道为1
            if (user.UserChannel == 0)
            {
                user.UserChannel = 1;
            }
            //判断是否存在，如果没有将当前连接写入用户列表
            userList.Add(user);

            Thread threadRec = new Thread(CmdFromTerminal);
            threadRec.Start(user);
            this.Invoke((EventHandler)delegate { 
                listBoxUser.Items.Add( "测试台:" + user.UserChannel + " " + user.IpAddress);            
            });

        }

        /// <summary>
        /// 处理生产线客户端测试命令
        /// </summary>
        /// <param name="userstate"></param>
        private void CmdFromTerminal(Object userstate)
        {
            //userCurrent = (User)userstate;
            User temp = (User)userstate;        //临时变量，读取命令
            TcpClient client = temp.Client;
            while (true)
            {
                string Rec = null;
                try
                {
                    Rec = temp.Read();
                }
                catch(EndOfStreamException ex)  //客户端断开连接，引发此异常
                {
                    
                    this.Invoke((EventHandler)delegate { 
                        listBoxUser.Items.Remove("测试台:" + temp.UserChannel + " " + temp.IpAddress);                    
                    });

                    userList.Remove(temp);           //从客户端列表中删除掉线用户
                    temp.Close();
                    if (temp == userCurrent)
                    {
                        CheckWait();
                    }
                    return;
                    //break;
                }
                catch(IOException ex)
                {
                    break;
                }
                

                //拆分客户端命令申请 IP+测试类型/阶段+起始波长+终止波长+点数+分辨率
                string[] splitString = Rec.Split('+');

                _startWave = splitString[2];
                _stopWave = splitString[3];
                if (splitString.Length > 4)
                {
                    _res = splitString[5];
                    _points = splitString[4];
                }

                if (userCurrent == null)    //没有终端使用OSA    
                {
                    userCurrent = temp;

                    //将OSA和宽带光源的光开关置相应通道
                    serialPortAse.WriteLine(Switch[userCurrent.UserChannel - 1]);
                    serialPortOsa.WriteLine(Switch[userCurrent.UserChannel - 1]);
                    //ChannelChange(userCurrent.UserChannel - 1);

                    this.Invoke((EventHandler)delegate
                    {
                        labelCurrent.Text = userCurrent.IpAddress;
                    });
                    userCurrent.SendCmd("Status=Use");         //添加正在扫描状态
                    OsaServer.StartWave = _startWave;
                    OsaServer.StopWave = _stopWave;
                    //OsaServer.Points = _points;
                    //OsaServer.Res = _res;
                    OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
                    OsaServer.Sweep();

                }
                else
                {
                    if (userCurrent != temp && userWait.Count < 3)
                    {
                        //if (userWait.Count == 0)            //  添加后，在线等待客户端数为1
                        //{
                        //    userWait.Add(temp);

                        //    userWait[0].SendCmd("Status=Wait=1");
                        //    userWait[0].StartPoint = _startWave;
                        //    userWait[0].StopPoint = _stopWave;
                        //    labelWait.Text = temp.IpAddress;
                        //}
                        //else if (userWait.Count == 1)            //已经有一个，再添加一个
                        //{
                        //    userWait.Add(temp);
                        //    userWait[1].SendCmd("Status=Wait=2");
                        //    userWait[1].StartPoint = _startWave;
                        //    userWait[1].StopPoint = _stopWave;

                        //    labelWait.Text = userWait[0].IpAddress + "\r\n" + temp.IpAddress;
                        //}
                        //else if (userWait.Count == 2)
                        //{
                        //    userWait.Add(temp);
                        //    userWait[2].SendCmd("Status=Wait=3");
                        //    userWait[2].StartPoint = _startWave;
                        //    userWait[2].StopPoint = _stopWave;

                        //    labelWait.Text = userWait[0].IpAddress + "\r\n" + userWait[1].IpAddress + "\r\n" + userWait[2].IpAddress;
                        //}

                        userWait.Add(temp);
                        userWait[userWait.Count].SendCmd("Status=Wait=" + (userWait.Count + 1));
                        userWait[userWait.Count].StartPoint = _startWave;
                        userWait[userWait.Count].StopPoint = _stopWave;

                        //userWait[userWait.Count - 1].SendCmd("Status=Wait=" + (userWait.Count));
                        //userWait[userWait.Count - 1].StartPoint = _startWave;
                        //userWait[userWait.Count - 1].StopPoint = _stopWave;


                        string wait = "";
                        for (int i = 0; i < userWait.Count; i++)
                        {
                            wait += "" + userWait[i].IpAddress + "\r\n";
                        }
                        labelWait.Text = wait;
                    }
                }
            }
        }

        #region 将OSA使用情况发送给其它客户端
        /// <summary>
        /// 将OSA使用情况发送给其它客户端 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        private void SendToAll(User user, string message)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                /*
                 * 发送给其它客户端
                 */
                if (userList[i].IpAddress != user.IpAddress)
                {
                    userList[i].SendCmd("Status=" + message);
                    //userList[i]._bw.Flush();
                }
            }
        }
        #endregion

        #region 将OSA空闲状态发送给所有客户端
        /// <summary>
        /// 将OSA空闲状态发送给所有客户端 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        private void SendToAll()
        {
            for (int i = 0; i < userList.Count; i++)
            {
                userList[i].SendCmd("Status=Idle");
                //userList[i]._bw.Flush();
            }
        }
        #endregion

        #region 发送消息给客户端
        /// <summary>
        /// 发送消息给user
        /// </summary>
        /// <param name="user">指定发给哪个客户端</param>
        /// <param name="message">要回传的数据</param>
        private void SendToClient(User user, string message)
        {
            try
            {
                user.SendCmd(message);
                //user._bw.Flush();
                richTextBoxStatus.AppendText(" 服务器 >> " + user.IpAddress + " \r\n  ");
            }
            catch
            {

            }
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////
        /*  以下是网络服务器作为客户端访问OSA程序部分   */
        //////////////////////////////////////////////////////////////////////////////////////////////////

        #region OSA开关操作
        /// <summary>
        /// 连接OSA扫描仪，以太网接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonbtnLink_Click(object sender, EventArgs e)
        {
            LinkToOsa();
        }

        /// <summary>
        /// 断开OSA扫描仪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonBtnUnlink_Click(object sender, EventArgs e)
        {

            try
            {
                if (OsaServer != null)
                {
                    OsaServer.Close();
                }
                richTextBoxStatus.AppendText(" 断开OSA连接 ！\r\n");
            }
            catch { }

        }
        #endregion

        #region 同步监听线程
        /// <summary>
        /// 同步监听线程
        /// </summary>
        private void LinkToOsa()
        {
            TcpClient OsaClient ;
            try
            {
                OsaClient = new TcpClient(_OsaIp, _OsaPort);
            }
            catch (Exception ee)
            {
                MessageBox.Show("  ERROR FROM OSA: " + ee.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("  未找到OSA  ！ ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OsaServer.Myclient = OsaClient;
            OsaServer.Init();
            OsaServer.OSAInit();

            OsaServer.RecFromOSAHandler += new RecFromOSA(MsgFromOSA);

            richTextBoxStatus.AppendText(OsaClient.Client.RemoteEndPoint.ToString() + "\r\n");
            richTextBoxStatus.AppendText(OsaClient.Client.LocalEndPoint.ToString() + "\r\n");

        }
        #endregion

        /// <summary>
        /// 接收OSA返回消息，根据消息进行下一步动作。
        /// 目前程序只作扫描后读取光谱曲线的X、Y轴坐标值
        /// </summary>
        /// <param name="msg"></param>
        public void MsgFromOSA(string readmessage)
        {
            if (userCurrent == null) return;
            if (OsaServer.Command == ":stat:oper:even?\r\n")              //扫描完后发命令读出曲线坐标
            {
                if (readmessage == "0")
                {
                    OsaServer.Command = ":TRACE:X? TRA\r\n";
                    OsaServer.SendCmd();
                }
            }
            else if (OsaServer.Command == ":TRACE:X? TRA\r\n")
            {

                userCurrent.SendCmd("Data=X=" + readmessage);    //向当前终端发X轴数据
                OsaServer.Command = ":TRACE:Y? TRA\r\n";
                OsaServer.SendCmd();
            }
            else if (OsaServer.Command == ":TRACE:Y? TRA\r\n")
            {
                userCurrent.SendCmd("Data=Y=" + readmessage);  //向当前终端发Y轴数据。这里一次扫描发送数据完成。
                Invoke((EventHandler)delegate {
                    richTextBoxStatus.AppendText(" From OSA to " + userCurrent.IpAddress + "  " + DateTime.Now.ToLongTimeString() + "\r\n");
                });
                
                userCurrent.SendCmd("Status=Finish");//查看等待列表中是否有待测器件
                CheckWait();
            }

        }

        /// <summary>
        /// 从排队列表里找下一个扫描命令
        /// </summary>
        private void CheckWait()
        {
            if (userWait.Count == 0)
            {
                userCurrent = null;
                this.Invoke((EventHandler)delegate
                {
                    labelCurrent.Text = "";         //清当前测试客户端
                    labelWait.Text = "";
                });
                //SendToAll();
            }
            else if (userWait.Count > 0)
            {
                userCurrent = userWait[0];                   //重置当前客户端
                labelCurrent.Text = userCurrent.IpAddress;

                serialPortAse.WriteLine(Switch[userWait[0].UserChannel - 1]);
                serialPortOsa.WriteLine(Switch[userWait[0].UserChannel - 1]);
                //ChannelChange(userCurrent.UserChannel - 1);

                System.Threading.Thread.Sleep(100);
                OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
                OsaServer.StartWave = userWait[0].StartPoint;
                OsaServer.StopWave = userWait[0].StopPoint;

                OsaServer.Sweep();//StartSweep();
                userCurrent.SendCmd("Status=Use");         //给测试中状态显示
                string wait = "";
                userWait.RemoveAt(0);
                for (int i = 0; i < userWait.Count; i++)
                {
                    wait += "" + userWait[i].IpAddress + "\r\n";
                    userWait[i].SendCmd("Status=Wait=" + (i + 1));
                }
                labelWait.Text = wait;
            }
            
        }

        #region 窗体最小化及恢复程序
        /// <summary>
        /// 显示窗体及任务栏图标
        /// </summary>
        //public void ShowMainForm()
        //{
        //    this.Visible = true;
        //    this.WindowState = FormWindowState.Normal;
        //    notifyIcon.Visible = true;
        //    this.ShowInTaskbar = true;
        //}

        ///// <summary>
        ///// 隐藏窗体及任务栏图标
        ///// </summary>
        //public void HideMainForm()
        //{
        //    this.Visible = false;
        //    this.WindowState = FormWindowState.Minimized;
        //    notifyIcon.Visible = true;
        //    this.ShowInTaskbar = false;
        //}

        //private void FormTCPServer_Deactivate(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        HideMainForm();
        //    }
        //}

        //private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        if (this.WindowState == FormWindowState.Minimized)
        //        {
        //            ShowMainForm();
        //        }
        //        else
        //        {
        //            HideMainForm();
        //        }
        //    }
        //}
        #endregion

        #region OSA1拖4服务器开关操作
        /// <summary>
        /// 启动1拖4服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonBtnStartServer_Click(object sender, EventArgs e)
        {
            server.BeginListen();
            richTextBoxStatus.AppendText("等待客户端连接中……\r\n");
        }

        /// <summary>
        /// 关闭1拖4服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonbtnStop_Click(object sender, EventArgs e)
        {

            try
            {
                server.Close();
                foreach (User user in userList)
                {
                    user.SendCmd("Status=Off");
                    user.Close();
                }
                //清空几个用户列表
                userWait.Clear();
                userList.Clear();
                labelWait.Text = "";
                richTextBoxStatus.AppendText("  OSA服务器已关闭  \r\n");

            }
            catch (SocketException ee)
            {
                return;
            }
            catch(IOException ee)
            {
                return;
            }

        }
        #endregion

        /// <summary>
        /// 清空状态栏内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxStatus.Clear();
        }

        /// <summary>
        /// 异常情况下复位，将当前用户、等待用户全部清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 清空等待列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                labelCurrent.Text = "";
                labelWait.Text = "";
                userWait.Clear();
                userCurrent = null;

                清空等待列表ToolStripMenuItem.Enabled = false;
                //给所有客户端发空闲状态
                SendToAll();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
                //throw;
            }
        }

        private void listBoxUserWait_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                清空等待列表ToolStripMenuItem.Enabled = true;
                清空ToolStripMenuItem.Enabled = false;
            }
        }

        private void richTextBoxStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                清空等待列表ToolStripMenuItem.Enabled = false;
                清空ToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// 配置扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            Config.WriteString("OSA", "IP", ipAddressOSA.Text);

            Config.WriteString("COM", "Source", comboBoxSource.Text);

            Config.WriteString("COM", "OSA", comboBoxOSA.Text);

            OsaServer.Csweepcfg.Points = textBoxCPoint.Text;
            Config.WriteString("Sweep", "CPoints", textBoxCPoint.Text);

            OsaServer.Csweepcfg.Res = comboBoxCRes.Text;
            Config.WriteString("Sweep", "CRes", comboBoxCRes.Text);

            OsaServer.Csweepcfg.Sens = comboBoxCSens.Text;
            Config.WriteString("Sweep", "Csens", comboBoxCSens.Text);

            OsaServer.Dsweepcfg.Points = textBoxDpoint.Text;
            Config.WriteString("Sweep", "DPoints", textBoxDpoint.Text);

            OsaServer.Dsweepcfg.Res = comboBoxDres.Text;
            Config.WriteString("Sweep", "DRes", comboBoxDres.Text);

            OsaServer.Dsweepcfg.Sens = comboBoxDses.Text;
            Config.WriteString("Sweep", "Dsens", comboBoxDses.Text);

            tabControlEx.SelectedIndex = 0;
        }

    }
}

//else if (userWait.Count == 1)
//{
//    userCurrent = userWait[0];                   //重置当前客户端
//    labelCurrent.Text = userCurrent.IpAddress;

//    serialPortAse.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    serialPortOsa.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    System.Threading.Thread.Sleep(100);
//    OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
//    OsaServer.StartWave = userWait[0].StartPoint;
//    OsaServer.StopWave = userWait[0].StopPoint;
//    //OsaServer.Points = userWait[0].Points ;
//    //OsaServer.Res = userWait[0].Res ;

//    OsaServer.Sweep();//StartSweep();
//    userCurrent.SendCmd("Status=Use");         //给测试中状态显示

//    //listBoxUserWait.Invoke(LabelWaitCallback, userWait[0].IpAddress, false);
//    labelWait.Text = "";
//    userWait.Clear();
//}
//else if (userWait.Count == 2)                   //列表中有两个客户端在等待
//{
//    userCurrent = userWait[0];                   //重置当前客户端
//    //labelCurrent.Text = userCurrent.IpAddress;
//    labelCurrent.Text = userCurrent.IpAddress;

//    //置第列表第一个客户端通道的光开关
//    serialPortAse.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    serialPortOsa.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    System.Threading.Thread.Sleep(100);
//    OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
//    OsaServer.StartWave = userWait[0].StartPoint;
//    OsaServer.StopWave = userWait[0].StopPoint;
//    //OsaServer.Points = userWait[0].Points;
//    //OsaServer.Res = userWait[0].Res;

//    OsaServer.Sweep();
//    //StartSweep();
//    userCurrent.SendCmd("Status=Use");         //给测试中状态显示
//    //userWait[0] = userWait[1];
//    userWait.RemoveAt(0);
//    userWait[0].SendCmd("Status=Wait=1");
//    //listBoxUserWait.Invoke(LabelWaitCallback, userWait[1].IpAddress, false);
//    labelWait.Text =  userWait[0].IpAddress;
//    //userWait.Remove(userWait[1]);
//}
//else if (userWait.Count == 3)                   //列表中有两个客户端在等待
//{
//    userCurrent = userWait[0];                   //重置当前客户端
//    labelCurrent.Text =  userCurrent.IpAddress;

//    //置第列表第一个客户端通道的光开关
//    serialPortAse.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    serialPortOsa.WriteLine(Switch[userWait[0].UserChannel - 1]);
//    System.Threading.Thread.Sleep(100);
//    OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
//    OsaServer.StartWave = userWait[0].StartPoint;
//    OsaServer.StopWave = userWait[0].StopPoint;
//    //OsaServer.Points = userWait[0].Points;
//    //OsaServer.Res = userWait[0].Res;
//    OsaServer.Sweep();

//    userCurrent.SendCmd("Status=Use");         //给测试中状态显示
//    userWait[0] = userWait[1];
//    userWait[1] = userWait[2];
//    userWait[0].SendCmd("Status=Wait=1");
//    userWait[1].SendCmd("Status=Wait=2");
//    //listBoxUserWait.Invoke(LabelWaitCallback, userWait[2].IpAddress, false);
//    labelWait.Text = userWait[0].IpAddress + "\r\n" + userWait[1].IpAddress;
//    userWait.Remove(userWait[2]);

//}