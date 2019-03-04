using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
/*
 * 修改日期 2015-5-18
 * 修改内容 添加扫描灵敏度变量_sens。DWDM扫描0.05nm分辨率，5001点，high1灵敏度
 *          客户端程序做对应修改。每次发送请求命令添加扫描点数和分辨率
 */
namespace OsaServer
{

    public delegate void RecFromOSA(string fromOsa);
    /// <summary>
    /// OSA网络通信类，做为客户端
    /// </summary>
    public class OSA
    {
        //作为客户端，与OSA通讯
        TcpClient _myclient;
        StreamWriter _sw;
        StreamReader _sr;
        BinaryWriter _bw;
        BinaryReader _br;
        NetworkStream ns;

         Thread th;
         volatile bool _AboutTh = false;

        public TcpClient Myclient
        {
            get { return _myclient; }
            set { _myclient = value; }
        }

        string _command = "open";
        /// <summary>
        /// 命令状态
        /// </summary>
        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public SweepCfg Csweepcfg = new SweepCfg();
        public SweepCfg Dsweepcfg = new SweepCfg();

        string _res = "0.5NM";
        string _Points = "10000";
        string _sens = "mid";
        ///// <summary>
        ///// 分辨率 1NM 0.05NM
        ///// </summary>
        //public string Res
        //{
        //    get { return _res; }
        //    set { 
        //        _res = value;
        //    }
        //}


        ///// <summary>
        ///// 扫描灵敏度
        ///// </summary>
        //public string Sens
        //{
        //    get { return _sens; }
        //    set { _sens = value; }
        //}


        ///// <summary>
        ///// 扫描点数
        ///// </summary>
        //public string Points
        //{
        //    get { return _Points; }
        //    set { 
        //        _Points = value; 
        //    }
        //}



        private string _StartWave = "1260";
        /// <summary>
        /// 起始扫描点
        /// </summary>
        public string StartWave
        {
            get { return _StartWave; }
            set
            {
                _StartWave = value;
            }
        }

        private string _StopWave = "1650";
        /// <summary>
        /// 终止扫描波长
        /// </summary>
        public string StopWave
        {
            get { return _StopWave; }
            set
            {
                     _StopWave = value;
                    if (_StopWave == "1650")
                    {
                        _Points = Csweepcfg.Points;
                        _res = Csweepcfg.Res;
                        _sens = Csweepcfg.Sens;
                    }
                    else if (_StopWave == "1610")
                    {
                        _Points = Dsweepcfg.Points;
                        _res = Dsweepcfg.Res;
                        _sens = Dsweepcfg.Sens;
                    }
            }
        }

        public event RecFromOSA RecFromOSAHandler;

        public OSA() {
            _myclient = new TcpClient();

        }

        public OSA(TcpClient Client)
        {
            _myclient = Client;
            ns = _myclient.GetStream();
            _br = new BinaryReader(ns);
            _bw = new BinaryWriter(ns);
            _sw = new StreamWriter(ns);
            _sr = new StreamReader(ns);

            //Byte[] recBuf = new Byte[2048];
            //ns.BeginRead(recBuf, 0, recBuf.Length, new AsyncCallback(myReadCallBack), 0);
        }

        //void myReadCallBack(IAsyncResult ar)
        //{
        //    int s = ns.EndRead(ar);
        //    _sr = new StreamReader(ns);
        //    string ss = _sr.ReadLine();
        //}

        public void Init()
        {
            ns = _myclient.GetStream();
            _br = new BinaryReader(ns);
            _bw = new BinaryWriter(ns);
            _sw = new StreamWriter(ns);
            _sr = new StreamReader(ns);

            th = new Thread(Rec);
            th.Start();
            _AboutTh = true;
        }

        void Rec()
        {
            while (_AboutTh)
            {
                try
                {
                    if (RecFromOSAHandler != null)
                        RecFromOSAHandler(_sr.ReadLine());
                }
                catch (EndOfStreamException ex)
                {
                    break;
                }
                catch (IOException ex)
                {
                    break;
                }
            }
        }

        public void Close()
        {
            if (_br != null)
            {
                _br.Close();
            }
            if (_bw != null)
            {
                _bw.Close();
            }
            if (_myclient != null)
            {
                _myclient.Close();
            }
            if (th != null)
                th.Abort();
            _AboutTh = false;
        }

        /// <summary>
        /// 扫描
        /// </summary>
        public void Sweep()
        {
            _bw.Write(":sens:wav:star " + _StartWave + "NM\r\n");
            _bw.Write(":sens:wav:stop " + _StopWave + "NM\r\n");
            _bw.Write(":sens:sens " + _sens + "\r\n");
            //if (_StartWave > 1260)
            //{
            _bw.Write(":SENSe:BWIDth:RES " + _res + "\r\n");                   //精度：高精度
            _bw.Write(":sens:sweep:points " + _Points + "\r\n");
            //}
            //else
            //{
            //    bw.Write(":SENSe:BWIDth:RES 1NM\r\n");                   //精度：高精度
            //    bw.Write(":sens:sweep:points " + _Points.ToString() + "\r\n");                
            //}

            _bw.Write(":init:smode 1\r\n");                //1 = SINGle  2 = REPeat  3 = AUTO  4 = SEGMent

            _bw.Write("*CLS\r\n");
            _bw.Write(":init\r\n");
            //OsaServer.Command = ":stat:oper:even?\r\n";                       //扫描是否完成的状态
            _bw.Write(":stat:oper:even?\r\n");
        }

        public void OSAInit()
        {
            _bw.Write("open \"anonymous\"\r\n");
            _bw.Write(" " + "\r\n");
            _bw.Write("CFORM1\r\n");                       //1对应AQ6370C
            _bw.Write(":TRACe:ACTive TRA\r\n");
            _bw.Write(":TRACe:ATTRibute :TRA WRITE\r\n");
            _bw.Write(":TRACE:STATE :TRA ON\r\n");
        }

        /// <summary>
        /// 指定扫描命令扫描
        /// </summary>
        /// <param name="OsaServer.Command"></param>
        public void SendCmd()
        {
            _bw.Write(_command);
        }

        public void SendCmd(string cmd)
        {
            _bw.Write(cmd);
        }

        public string ReadLine()
        {
            return _sr.ReadLine();
        }
    }
}
