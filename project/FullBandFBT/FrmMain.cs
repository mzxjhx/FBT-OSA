using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Result;

using CalcSpec;
using CWDM1To4.Class;
using CWDM1To4.Option;

using FormUI;
using System.Runtime.InteropServices;
/*************************************************************************
 *  日期 2015-12
 *  说明 ：
 *      使用OSA扫描FBT产品，扫描波长1260～1650，分辨率0.5nm，精度mid
 *      计算1310、1550、1490及1625 4个点IL
 *      1310±40  1550±40范围IL
 *      
 *  日期 2016-2
 *  说明 ：
 *      改成可以设计任务数量带宽，带宽内容自定义
 **************************************************************************/

namespace CWDM1To4
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        ServerClient client = new ServerClient();

        public SaveInfo info = new SaveInfo();
        ///<summary>
        /// 实例化配置文件
        ///</summary>
        public IniFile inifile = new IniFile();

        #region 变量定义

        public static string WorkID = null;

        /// <summary>
        /// 4通道参数
        /// </summary>
        private Par[] par = new Par[2];

        /// <summary>
        /// 记录保存路径
        /// </summary>
        private string Savepath = "";
        /// <summary>
        /// 用于清空表格右键菜单，确定清哪个表格
        /// </summary>
        private string CLearDgv = "";

        #endregion

        #region 定义图形显示所要数据变量

        PointPairList listpass = new PointPairList();       //器件曲线
        PointPairList listref = new PointPairList();        //参考值曲线
        PointPairList listIl = new PointPairList();         //差值，实际损耗曲线

        //定义图表显示所用曲线，透射曲线和反射曲线
        /// <summary>
        /// 器件测试曲线
        /// </summary>
        LineItem curvepass;
        /// <summary>
        /// 参考值曲线
        /// </summary>
        LineItem curveref;
        /// <summary>
        /// IL曲线，可直观地看到IL情况
        /// </summary>
        LineItem curveIl;

        #endregion

        string IP = @"server=192.168.0.222;uid=sa;pwd=rayzer;database=db_FBT";

        PropertyServer Server = new PropertyServer();

        SweepWave sweepWave = new SweepWave();

        List<FBTBand> fbtBands = new List<FBTBand>();
        /// <summary>
        /// 输出端对应扫描波段列表，索引从1开始
        /// </summary>
        Dictionary<int, List<FBTBand>> map = new Dictionary<int, List<FBTBand>>();

        /// <summary>
        /// 当前输出端，从1开始
        /// </summary>
        int intCurOut = 0;
        /// <summary>
        /// 总输出端，从1开始
        /// </summary>
        int intTotalOuts = 0;
        /// <summary>
        /// 每输出端波段数
        /// </summary>
        int intTotalBands = 0;
        DataGridView[] dgvs = new DataGridView[4];
        List<IPCorespond> IPlist;
        IPAddress _ServerIP;
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            inifile.FileName = Application.StartupPath.ToString() + "\\config.ini";
            //ParCompare.Load(Path.Combine(Application.StartupPath.ToString(), "config.ini"));

            StatusBarServices.Initialize();
            StatusStripCommand statusStripCommand = (StatusStripCommand)StatusBarServices.Control;
            statusStripCommand.Ctrl = this;
            this.Controls.Add(statusStripCommand);
            statusStripCommand.Dock = DockStyle.Bottom;
            statusStripCommand.UserName = WorkID;

            info.IsFinish = false;
            intCurOut = 1;
            intTotalOuts = 2;
            comboBoxOuts.Text = "2";

            clientInit();

            dgvreference.Rows[0].Cells[0].Value = "参考值";
            dgv4.Rows.Add();

            ChartInit();

            Sweep.GetInstance().TestModel = SweepModel.CWDM;

            toolStripBtnUnlink.Enabled = false;

            btnTest.Enabled = false;
            btnSave.Enabled = true;

            //Savepath = @"D:\CWDM记录\" + DateTime.Now.ToString("D");          //日期为单位建文件夹
            //Savepath = @"D:\CWDM记录\" + DateTime.Now.ToString("d");          //日期为单位建文件夹
            Savepath = @"D:\FBT数据记录\" + DateTime.Now.ToString("Y");            //月为单位建文件夹
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }

            info.PassRef = new double[SweepWave.Cpoints];
            info.TraceX = new double[SweepWave.Cpoints];
            info.TraceYPN = new double[2,SweepWave.Cpoints];

            toolBarCommandAbout.Command = new About();

            for (int i = 0; i < 4; i++)
            {
                dgvs[i] = new DataGridView();
            }

            dgvs[0] = dgv1;
            dgvs[1] = dgv2;
            dgvs[2] = dgv3;
            dgvs[3] = dgv4;

            //IPlist = IPDao.Read();

            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            //服务器端，用DNS解析本机ip地址
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))    //ipv4地址
                    _ServerIP = ip;
            }
            //foreach (var item in IPlist)
            //{
            //    if (item.Terminal == _ServerIP.ToString())
            //    {
            //        client.ServerIp = item.Server;
            //    }
            //}

            result1.ShowResult = PassOrFailed.Pass;
        }

        private void clientInit()
        {
            client.ServerIp = inifile.ReadString("Server", "IP", "1");
            client.ServerPort = inifile.ReadString("Server", "Port", "1");
            toolStripBtnLink.Click += client.LinkToServer;
            toolStripBtnUnlink.Click += client.Unlink;
            //接收数据
            client.FromOSACallBack += RecMsg;
            client.LinkEventHandler += LinkStatus;

        }

        /// <summary>
        /// 连接状态
        /// </summary>
        /// <param name="ss"></param>
        private void LinkStatus(string ss)
        {
            if (ss == "断开")
            {
                toolStripBtnLink.Enabled = true;
                toolStripBtnUnlink.Enabled = false;
                btnTest.Enabled = false;
            }
            else if (ss == "连接")
            {
                toolStripBtnLink.Enabled = false;
                toolStripBtnUnlink.Enabled = true;
                btnTest.Enabled = true;
            }
        }

        /// <summary>
        /// 光谱曲线图显示初始化
        /// </summary>
        private void ChartInit()
        {
            #region 图表初始化
            //图表标题
            zedGraphControl.GraphPane.Title.Text = "OSA Trace";
            zedGraphControl.GraphPane.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.Title.FontSpec.Size = 18f;
            zedGraphControl.GraphPane.Title.FontSpec.Family = "宋体";//"楷体_GB2312";

            zedGraphControl.GraphPane.XAxis.Title.Text = "波长(nm)";
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 12f;
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.Family = "宋体"; //"楷体_GB2312";


            zedGraphControl.GraphPane.YAxis.Title.Text = "功率(dBm)";
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 12f;
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.Family = "宋体"; //"楷体_GB2312";

            zedGraphControl.GraphPane.XAxis.Color = Color.White;
            zedGraphControl.GraphPane.YAxis.Color = Color.White;

            zedGraphControl.GraphPane.YAxis.Scale.MaxAuto = true;
            zedGraphControl.GraphPane.YAxis.Scale.Min = -100;

            zedGraphControl.GraphPane.YAxis.Scale.MajorStep = 10;

            zedGraphControl.GraphPane.YAxis.MajorGrid.Color = Color.White;
            zedGraphControl.GraphPane.XAxis.MajorGrid.Color = Color.White;

            zedGraphControl.GraphPane.YAxis.MinorGrid.Color = Color.White;
            zedGraphControl.GraphPane.XAxis.MinorGrid.Color = Color.White;

            zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 12;
            zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 12;

            zedGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;

            //zedGraphControl.GraphPane.XAxis.MinorGrid.IsVisible = true;
            //zedGraphControl.GraphPane.YAxis.MinorGrid.IsVisible = true;

            zedGraphControl.GraphPane.Title.Text = "OSA TRACE";
            curvepass = zedGraphControl.GraphPane.AddCurve("器件曲线", listpass, Color.Red, SymbolType.None);
            curvepass.Line.Width = 1.5f;

            curveref = zedGraphControl.GraphPane.AddCurve("参考值曲线", listref, Color.Yellow, SymbolType.None);
            curveref.Line.Width = 1.5f;

            curveIl = zedGraphControl.GraphPane.AddCurve("差值曲线", listIl, Color.Green, SymbolType.None);
            curveIl.Line.Width = 1.5f;

            zedGraphControl.GraphPane.XAxis.Type = ZedGraph.AxisType.Linear;

            //背景色
            zedGraphControl.GraphPane.Chart.Fill = new Fill(Color.Black);
            #endregion
        }

        /// <summary>
        /// 发送透射测试命令
        /// </summary>
        public void PassCmd()
        {

            if (Sweep.GetInstance().TestModel != SweepModel.DWDM)
                client.SendMessage("+1260+1650+" + SweepWave.Cpoints + "+0.5NM");
            else
                client.SendMessage("+1525+1610+" + SweepWave.Dpoints + "+0.05NM");

        }


        /************************************************************************************************
                                          在这里处理接收数据
        ************************************************************************************************/
        /// <summary>
        /// </summary>
        public void RecMsg(string xy, string receiveString)
        {

            string[] trace = receiveString.Split(',');
            if (xy == "X")
            {
                if (Sweep.GetInstance().Step == SweepStep.RassRef || Sweep.GetInstance().Step == SweepStep.ReflectRef)
                {
                    for (int i = 0; i < trace.Length; i++)
                    {
                        info.TraceX[i] = double.Parse(trace[i]) * 1000000000;
                    }
                }
            }
            else if (xy == "Y")                 //接收到Y轴坐标值,根据测试阶段给8条曲线
            {
                if (Sweep.GetInstance().Step == SweepStep.PassNormal)
                {
                    //double[] rec;
                    double[] tracetemp = new double[trace.Length];
                    for (int i = 0,len = trace.Length; i < len; i++)
                    {
                        info.TraceYPN[intCurOut - 1, i] = double.Parse(trace[i]);
                        tracetemp[i] = info.TraceYPN[intCurOut - 1, i];
                    }
                    //循环计算
                    foreach (IBand item in map[intCurOut - 1])
                    {
                        item.CalcBandPar(info.PassRef, tracetemp);
                    }

                    for (int i = 0; i < intTotalBands; i++)
                        map[intCurOut - 1][i].IsPass = ParCompare.CmpIL(intCurOut - 1, i, map[intCurOut - 1][i].Par);

                    UpdateDgv();

                    //todo 当测试到最后一个输出端时，提示是否保存
                    if (intCurOut == intTotalOuts)
                    {

                        //for (int i = 0; i < intTotalBands; i++)
                        for (int i = 0; i < intTotalOuts; i++)
                        foreach (ICompareIL item in map[i])
                        {
                            if (item.IsPass == false)
                            {
                                info.IsPass = false;
                                break;
                            }
                        }
                        this.Invoke((EventHandler)delegate {
                            result1.ShowResult = info.IsPass == true ? PassOrFailed.Pass : PassOrFailed.Failed;
                        });

                        DialogResult result = MessageBox.Show(" 已经完成测试，注意输入SN号并保存 ", " 保存 ", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            SaveCSV();
                            info.IsFinish = false;
                            intCurOut = 1;

                            for (int i = 0; i < dgv1.Rows.Count; i++)
                            {
                                dgv1.Rows[i].Cells[2].Value = "";
                                dgv1.Rows[i].Cells[3].Value = "";
                            }
                            for (int i = 0; i < dgv2.Rows.Count; i++)
                            {
                                dgv2.Rows[i].Cells[2].Value = "";
                                dgv2.Rows[i].Cells[3].Value = "";
                            }
                            for (int i = 0; i < dgv3.Rows.Count; i++)
                            {
                                dgv3.Rows[i].Cells[2].Value = "";
                                dgv3.Rows[i].Cells[3].Value = "";
                            }
                            for (int i = 0; i < dgv4.Rows.Count; i++)
                            {
                                dgv4.Rows[i].Cells[2].Value = "";
                                dgv4.Rows[i].Cells[3].Value = "";
                            }
                            txtboxSN.Text = "";
                        }
                        info.IsFinish = true;
                    }

                }
                else if (Sweep.GetInstance().Step == SweepStep.RassRef)
                {
                    for (int i = 0; i < trace.Length; i++)
                    {
                        info.PassRef[i] = double.Parse(trace[i]);
                    }
                    UpdateChart(info.TraceX, info.PassRef);        //显示光谱图    
                }

                //UpdateDgv();  //包括测试结束和重测TDL计算及更新
                if (info.IsFinish == true)
                {
                    //产品测试完，判断合格与否

                }
            }
        }

        /// <summary>
        /// 调用代理，给表格控件传值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="centerwave"></param>
        public void UpdateDgv()
        {
            if (this.InvokeRequired)
            {
                UpdateUI d = new UpdateUI(UpdateDgv);
                this.Invoke(d);
            }
            else
            {
                switch (Sweep.GetInstance().Step)          
                {
                    case SweepStep.PassNormal:   

                        for (int i = 0; i < intTotalBands; i++)
                        {
                            dgvs[intCurOut - 1].Rows[i].Cells[2].Value = map[intCurOut - 1][i].Par.MaxValue;
                            dgvs[intCurOut - 1].Rows[i].Cells[3].Value = map[intCurOut - 1][i].Par.MinValue;

                            //dgvs[intCurOut - 1].Rows[i].Cells[2].Style.ForeColor = ParCompare.CompareIL(intCurOut - 1, i, map[intCurOut][i].Par.MaxValue);
                            //dgvs[intCurOut - 1].Rows[i].Cells[3].Style.ForeColor = ParCompare.CompareIL(intCurOut - 1, i, map[intCurOut][i].Par.MinValue);
                            dgvs[intCurOut - 1].Rows[i].Cells[2].Style.ForeColor = ParCompare.CompareIL(intCurOut - 1, i, map[intCurOut - 1][i].Par);
                            dgvs[intCurOut - 1].Rows[i].Cells[3].Style.ForeColor = ParCompare.CompareIL(intCurOut - 1, i, map[intCurOut - 1][i].Par);
                            
                        }
                        
                        break;
                    case SweepStep.RassRef:
                        Refence.PassRefence(dgvreference, info.PassRef, Sweep.GetInstance().TestModel);

                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 调用代理显示光谱图
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UpdateChart(double[] x, double[] y)
        {
            if (this.InvokeRequired)
            {
                UpdateChar d = new UpdateChar(UpdateChart);
                this.Invoke(d, new object[] { x, y });
            }
            else
            {
                if (listIl != null)
                {
                    listIl.Clear();
                }
                if (listref != null)
                {
                    listref.Clear();
                }
                listpass.Clear();
                zedGraphControl.AxisChange();
                zedGraphControl.Refresh();
                listpass.Add(x, y);
                zedGraphControl.AxisChange();
                zedGraphControl.Refresh();
            }
        }

        /// <summary>
        /// 参数设置，同步回调参数读取及状态栏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnSet_Click(object sender, EventArgs e)
        {
            //FormOption fo = new FormOption();
            //fo.ShowDialog();

            FrmSet fs = new FrmSet();
            fs.ShowDialog();
        }

        private void toolStripbtnReview_Click(object sender, EventArgs e)
        {
            FormReview fmreview = new FormReview();
            fmreview.ShowDialog();
        }

        /// <summary>
        /// 三个温度阶段的透射
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxtype.Text == "" || txtBoxProductNO.Text == "" || txtboxSN.Text == "")
                {
                    MessageBox.Show(" 请正确填写产品规格信息 ", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }

                btnSave.Enabled = true;
                Sweep.GetInstance().Step = SweepStep.PassNormal;
                PassCmd();
                if (radioButton1.Checked)
                {
                    intCurOut = 1;
                }
                else if (radioButton2.Checked)
                {
                    intCurOut = 2;
                }
                else if (radioButton3.Checked)
                {
                    intCurOut = 3;
                }
                else if (radioButton4.Checked)
                {
                    intCurOut = 4;
                }
                intCurOut = intCurOut < int.Parse(comboBoxOuts.Text) ? intCurOut : int.Parse(comboBoxOuts.Text);
                //ClearAllDgv();

            }
            catch
            {
                if (client.Client.Connected == false)
                {
                    MessageBox.Show(" 请连接服务器！", "警告");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                toolStripBtnUnlink.PerformClick();
            }
        }

        /// <summary>
        /// 用于图形显示清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (listpass != null)
            {
                listpass.Clear();
            }
            if (listref != null)
            {
                listref.Clear();
            }
            if (listIl != null)
            {
                listIl.Clear();
            }
            zedGraphControl.AxisChange();
            zedGraphControl.Refresh();
        }

        /// <summary>
        /// 重测当前通道的右键状态选择，透射测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvpass_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CLearDgv = "dgvpass";

            }
        }

        /// <summary>
        /// 右键菜单选择清空表格显示
        /// </summary>
        private void dgvClear()
        {
            dgv1.Rows.Clear();
            dgv2.Rows.Clear();
            dgv3.Rows.Clear();
            dgv4.Rows.Clear();
            for (int i = 0; i < intTotalOuts; i++)
            {
                for (int j = 0; j < intTotalBands; j++)
                {
                    dgvs[i].Rows[j].Cells[2].Value = "";
                    dgvs[i].Rows[j].Cells[3].Value = "";
                }

            }

        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvClear();
        }

        /// <summary>
        /// 保存当前数据，保存之后再允许下一次测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            //if (txtBoxProductNO.Text.Trim().Length != 9 || txtboxSN.Text.Trim().Length != 12 || txtBoxtype.Text == "")
            //{
            //    MessageBox.Show(" 所要保存的产品规格信息错误，请检查输入字符长度 ", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    return;
            //}

            DialogResult result = MessageBox.Show(" 确定保存数据 ？ ", " 保存 ", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //SaveSql();
                SaveCSV();
                //txtboxSN.Text = txtboxSN.Text.Substring(0, txtboxSN.Text.Length - 3);

                info.IsFinish = false;
                intCurOut = 1;

                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    dgv1.Rows[i].Cells[2].Value = "";
                    dgv1.Rows[i].Cells[3].Value = "";
                }
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    dgv2.Rows[i].Cells[2].Value = "";
                    dgv2.Rows[i].Cells[3].Value = "";
                }
                for (int i = 0; i < dgv3.Rows.Count; i++)
                {
                    dgv3.Rows[i].Cells[2].Value = "";
                    dgv3.Rows[i].Cells[3].Value = "";
                }
                for (int i = 0; i < dgv4.Rows.Count; i++)
                {
                    dgv4.Rows[i].Cells[2].Value = "";
                    dgv4.Rows[i].Cells[3].Value = "";
                }
                txtboxSN.Text = "";
            }
        }


        /// <summary>
        /// 将记录保存为CSV文件，包括测试信息和光谱曲线
        /// </summary>
        public void SaveCSV()
        {
            using (StreamWriter sw = new StreamWriter(Savepath + "\\" + txtboxSN.Text + ".CSV",false, Encoding.GetEncoding("GB2312")))
            {
                sw.Write("制造单号,");
                sw.Write("规格,");
                sw.Write("SN,");
                sw.Write("等级,");
                sw.WriteLine("工号,");

                sw.Write(txtBoxProductNO.Text + ",");
                sw.Write(txtBoxtype.Text + ",");
                sw.Write(txtboxSN.Text + ",");
                sw.Write(info.Grade + ",");
                sw.WriteLine(WorkID + ",");

                sw.WriteLine("");
                sw.WriteLine("波段,最大值,最小值");

                foreach (var item in map)
                {
                    sw.WriteLine("输出端");
                    foreach (FBTBand value in item.Value)
                    {
                        sw.WriteLine(value.Start + "~" + value.Stop + "," + value.Par.MaxValue + "," + value.Par.MinValue);
                    }
                }

                sw.WriteLine("波长,输出1IL,输出2IL,输出3IL,输出4IL");

                for (int i = 0; i < info.TraceX.Length; i++)
                {
                    sw.Write(info.TraceX[i] + ",");
                    sw.Write((info.PassRef[i] - info.TraceYPN[0, i]) + ",");
                    sw.WriteLine((info.PassRef[i] - info.TraceYPN[1, i]) + ",");

                }
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="procName"></param>
        private void SaveSql()
        {
            string pass = info.IsPass == true ? "Pass" : "Failed";
            string SN = txtboxSN.Text;
            using (SqlConnection connection = new SqlConnection(IP))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
                cmd.CommandText = "select * from tb_FBTFullBand where SN ='" + txtboxSN.Text + "'";
                SqlDataReader adr = cmd.ExecuteReader();
                if (adr.HasRows)
                {
                    adr.Close();
                    //todo update
                    cmd.CommandText = "update tb_FBTFullBand set "
                        + "日期='" + DateTime.Now.ToString() + "',"
                        + "制造单号='" + txtBoxProductNO.Text.Trim() + "',"
                        + "工号='" + WorkID + "',"
                        + "器件类型='" + txtBoxtype.Text.Trim() + "',"
                        + "端口类别='" + comboBoxOuts.Text + "',"
                        + "分光比='" + textBoxCR.Text + "',"
                        + "等级='" + info.Grade + "',"
                        + "合格='" + pass + "'"
                        + "where SN='" + SN + "'";
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < par.Length; i++)
                    {
                        cmd.CommandText = "";
                        cmd.CommandText = "update tb_FullBandOut" + (i + 1) +" set "
                            + "日期='" + DateTime.Now.ToString() + "','"
                            + "IL1310='" + par[i].Il1310 + ","
                            + "IL1490='" + par[i].Il1490 + ","
                            + "IL1550='" + par[i].Il1550 + ","
                            + "IL1625='" + par[i].Il1625 + ","
                            + "ILMax1310='" + par[i].IlMax1310 + ","
                            + "ILMin1310='" + par[i].IlMin1310 + ","
                            + "ILMax1550='" + par[i].IlMax1550 + ","
                            + "ILMin1550='" + par[i].IlMin1550 + ""
                        +"where SN='" + SN + "'";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    adr.Close();
                    //todo insert
                    cmd.CommandText = "insert into tb_FBTFullBand values('"
                        + DateTime.Now.ToString() + "','"
                        + txtBoxProductNO.Text.Trim() + "','"
                        + SN + "','"
                        + WorkID + "','"
                        + txtBoxtype.Text.Trim() + "','"
                        + comboBoxOuts.Text + "','"
                        + textBoxCR.Text + "','"
                        + info.Grade + "','"
                        + pass + "')";
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < par.Length; i++)
                    {
                        cmd.CommandText = "";
                        cmd.CommandText += "insert into tb_FullBandOut" + (i + 1) + " values('"
                                + DateTime.Now.ToString() + "','"
                                + SN + "',"
                                + par[i].Il1310 + ","
                                + par[i].Il1490 + ","
                                + par[i].Il1550 + ","
                                + par[i].Il1625 + ","
                                + par[i].IlMax1310 + ","
                                + par[i].IlMin1310 + ","
                                + par[i].IlMax1550 + ","
                                + par[i].IlMin1550 + ")";

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #region 测试参考值操作

        private void 测试参考值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(" 请对接好WDM器件光路 ,单击<确定>测试参考值", "测试参考值", MessageBoxButtons.OKCancel)
                 == DialogResult.OK)
                {
                    if (Sweep.GetInstance().Step == SweepStep.RassRef)
                    {
                        PassCmd();
                    }
                }
            }
            catch
            {
                MessageBox.Show(" 未检测到服务器，请先连接服务器。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 参考值右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvreference_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex == 0)
                {
                    Sweep.GetInstance().Step = SweepStep.RassRef;
                }
                //if (e.RowIndex == 1)
                //{
                //    Sweep.GetInstance().Step = SweepStep.ReflectRef;
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 导入参考值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.RestoreDirectory = true;
            of.Filter = "(*.CSV)|*.CSV";
            int N = SweepWave.Cpoints;
            if (Sweep.GetInstance().TestModel == SweepModel.DWDM)
                N = SweepWave.Dpoints;          //()
            if (of.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = of.FileName.ToString();

                using (FileStream fs = new FileStream(localFilePath, FileMode.Open))
                {
                    StreamReader sr = new StreamReader(fs);
                    int i = 0;
                    while (i < N)
                    {
                        string sss = sr.ReadLine();
                        string[] str = sss.Split(',');

                        info.TraceX[i] = double.Parse(str[0]);
                        info.PassRef[i] = double.Parse(str[1]);

                        i++;
                    }
                }
                Refence.PassRefence(dgvreference, info.PassRef, Sweep.GetInstance().TestModel);
            }

        }

        private void 导出参考值ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog.Filter = "(*.CSV)|*.CSV";
            //保存对话框是否记忆上次打开的目录 
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog.FileName.ToString();
                //string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                using (FileStream fs = new FileStream(localFilePath, FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < info.PassRef.Length; i++)
                    {
                        sw.WriteLine(info.TraceX[i] + "," + info.PassRef[i] + "," );
                    }

                    sw.Close();
                }
                MessageBox.Show("  导出成功 ！ ", "导出参考值", MessageBoxButtons.OKCancel);
            }
        }

        #endregion

        private void toolStripBtnReview_Click_1(object sender, EventArgs e)
        {
            FormReview fr = new FormReview();
            fr.ShowDialog();
        }

        /// <summary>
        /// 图形曲线查看功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowChart_Click(object sender, EventArgs e)
        {
            try
            {
                listpass.Clear();
                listref.Clear();
                listIl.Clear();
                //zedGraphControl.AxisChange();
                //zedGraphControl.Refresh();    
                //listref.Add(info.TraceX, info.PassRef);
                //显示差值曲线

                int N = SweepWave.Cpoints;

                double[] traceY = new double[N];

                for (int i = 0; i < N; i++)
                {
                    traceY[i] = info.PassRef[i] - info.TraceYPN[comboBoxChart.SelectedIndex  ,i];
                }

                listIl.Add(info.TraceX, traceY);

                zedGraphControl.AxisChange();
                zedGraphControl.Refresh();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
                //throw;
            }
        }

        /// <summary>
        /// 数据表里添加扫描按键,实现重扫和扫描功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvpass_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                Sweep.GetInstance().Step = SweepStep.PassNormal;

                if (e.RowIndex + 1 > intTotalOuts)
                {
                    MessageBox.Show(" 器件只有 " + intTotalOuts + " 个输出端 ");
                    return;
                }
                else
                {
                    intCurOut = e.RowIndex + 1;
                }
                PassCmd();
            }

        }

        /// <summary>
        /// 改变输出端口数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxOuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //par = new Par[int.Parse(comboBoxOuts.Text)];
            comboBoxChart.Items.Clear();
            map.Clear();
            for (int i = 0; i < int.Parse(comboBoxOuts.Text); i++)
            {
                //par[i] = new Par();
                comboBoxChart.Items.Add("输出端" + (i + 1));
                //map.Add((i + 1), fbtBands);
                map.Add((i), fbtBands);
            }
            intTotalOuts = int.Parse(comboBoxOuts.Text);
            info.TraceYPN = new double[intTotalOuts, SweepWave.Cpoints];
            //DgvInit(par.Length);
        }

        /// <summary>
        /// 加载波段内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            intTotalBands = int.Parse(comboBoxselect.Text);
            Wavechange();
        }

        /// <summary>
        /// 确定波段内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            fbtBands.Clear();
            map.Clear();
            for (int i = 0; i < intTotalOuts; i++)
            {
                dgvs[i].Rows.Clear();
            }

            for (int i = 0; i < int.Parse(comboBoxOuts.Text); i++)
            {
                List<FBTBand> list = new List<FBTBand>();
                for (int j = 0, len = int.Parse(comboBoxselect.Text); j < len; j++)
                {
                    FBTBand band = new FBTBand()
                    {
                        Start = double.Parse(dataGridViewSet.Rows[j].Cells[0].Value.ToString()),
                        Stop = double.Parse(dataGridViewSet.Rows[j].Cells[1].Value.ToString())
                    };

                    //fullBands.Add(new FullBand() { Band = band, Par = new BandPar() });
                    list.Add(band);

                }
                //map.Add((i + 1), list);
                map.Add(i, list);
            }
            for (int i = 0; i < intTotalOuts; i++)
            {
                for (int j = 0; j < intTotalBands; j++)
                {
                    dgvs[i].Rows.Add();
                    dgvs[i].Rows[j].Cells[1].Value = dataGridViewSet.Rows[j].Cells[0].Value.ToString() + "~" + dataGridViewSet.Rows[j].Cells[1].Value.ToString();
                }
            }
        }

        void Wavechange()
        {
            dataGridViewSet.Rows.Clear();
            //读规格参数
            for (int i = 0, len = int.Parse(comboBoxselect.Text); i < len; i++)
            {
                dataGridViewSet.Rows.Add();
                string ss = "CH" + (i + 1) + "";
                dataGridViewSet.Rows[i].Cells[0].Value = inifile.ReadString("CH" + (i + 1) + "", "WAVE1", "1");
                dataGridViewSet.Rows[i].Cells[1].Value = inifile.ReadString("CH" + (i + 1) + "", "WAVE2", "1");

            }
        }

    }
}
