using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace OsaServer
{
    public delegate void FindUser(TcpClient user);

    /// <summary>
    /// 服务器，处理客户端请求
    /// </summary>
    public class Server
    {

        private TcpListener mylistener;
        /// <summary>
        /// 服务器IP
        /// </summary>
        private IPAddress _ServerIP;  
        private int _port = 10001;
        Thread threadListen;
        volatile bool _listen = true;

        public TcpListener Mylistener
        {
            get { return mylistener; }
            set { mylistener = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        /// <summary>
        /// 服务器端监听到客户端连接
        /// </summary>
        public event FindUser FindUserHandler;

        public Server() {
            Init();
        }

        public void Init()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            //服务器端，用DNS解析本机ip地址
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))    //ipv4地址
                    _ServerIP = ip;
            }
            //_ServerIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            _port = 20001;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void BeginListen()
        {
            mylistener = new TcpListener(_ServerIP, _port);
            mylistener.Start();
            threadListen = new Thread(ListenClient);
            threadListen.Start();
            _listen = true;
        }


        /// <summary>
        /// 监听线程，新建Client
        /// </summary>
        private void ListenClient()
        {
            TcpClient newClient = null;
            while (_listen)
            {
                try
                {
                    newClient = mylistener.AcceptTcpClient();
                }
                catch(SocketException ex)
                {

                }
                if (FindUserHandler != null)
                    FindUserHandler(newClient);
            }
        }

        public void Close()
        {
            if (mylistener != null)
                mylistener.Stop();
            _listen = false;
            if (threadListen != null)
                threadListen.Abort();
        }
    }
}
