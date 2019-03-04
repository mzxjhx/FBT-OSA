using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace OsaServer
{
    /// <summary>
    /// OSA网络通信类
    /// </summary>
    class OSA
    {
        //作为OSA客户端
        private string _osaip;
        public string OSAIp
        {
            get { return _osaip; }
            set { _osaip = value; }
        }

        private int _osaport;
        public int Osaport
        {
            get { return _osaport; }
            set { _osaport = value; }
        }

        //作为客户端，与OSA通讯
        public TcpClient myclient;
        public StreamWriter sw;
        public StreamReader sr;
        public BinaryWriter bw;
        public BinaryReader br;
        public NetworkStream ns;
        public OSA(TcpClient Client)
        {
            this.myclient = Client;
            ns = myclient.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);
        }
        public void Close()
        {
            br.Close();
            bw.Close();
            myclient.Close();
        }
    }
}
