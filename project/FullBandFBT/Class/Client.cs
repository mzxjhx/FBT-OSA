using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using FormUI;

namespace CWDM1To4.Class
{
    public class ServerClient
    {
        #region 服务器定义
        /// <summary>
        /// 定义服务器类
        /// </summary>
        string _serverIp;
        string _serverPort;

        BinaryWriter bw;
        BinaryReader br;
        NetworkStream network;
        TcpClient client;
        Thread threadclient;

        public string ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }
        public string ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }
        public TcpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        #endregion

        public delegate void DataFromOSA(string message,string da);
        public event DataFromOSA FromOSACallBack;

        public delegate void Link(string ss);
        public event Link LinkEventHandler;

        /// <summary>
        /// 同步监听线程
        /// </summary>
        public void LinkToServer(object render, EventArgs e)
        {
            try
            {
                client = new TcpClient(_serverIp, Int16.Parse(_serverPort));
                StatusBarServices.Status = "连接中…… ";
            }
            catch //(Exception ee)
            {
                //MessageBox.Show(ee.Message);
                StatusBarServices.Status ="  未找到服务器  ！ ";
                return;
            }
            StatusBarServices.Status = "连接";
            LinkEventHandler("连接");
            network = client.GetStream();
            br = new BinaryReader(network);
            bw = new BinaryWriter(network);

            threadclient = new Thread(new ThreadStart(RecMsg));
            threadclient.Start();

        }

        /// <summary>
        /// 断开服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Unlink(object sender, EventArgs e)
        {
            if (client != null)
            {
                network.Close();
                client.Close();
            }
            LinkEventHandler("断开");
            StatusBarServices.Status = "断开";
        }

        /// <summary>
        /// socket异步接收
        /// </summary>
        public void RecMsg()
        {
            string receiveString = null;
            while (true)
            {
                try
                {
                    receiveString = br.ReadString();
                }
                catch
                {
                    break;
                }

                //拆分服务器消息，以"="为间隔
                string[] message = receiveString.Split('=');
                switch (message[0])
                {
                    case "Status":      //

                        switch (message[1])
                        {
                            case "Use":
                                
                                StatusBarServices.Status = "扫描中";
                                break;
                            case "Wait":
                                StatusBarServices.Status ="排队 " + message[2];
                                break;
                            case "Idle":
                                StatusBarServices.Status ="空闲";
                                break;
                            case "Finish":
                                StatusBarServices.Status ="完成";
                                break;
                            case "Off":
                                StatusBarServices.Status ="断开";
                                if (client != null)
                                {
                                    network.Close();
                                    client.Close();
                                }
                                LinkEventHandler("断开");
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Data":        //接收数据 文件或者数据,显示光谱曲线
                        FromOSACallBack(message[1],message[2]);
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 向服务器发送命令
        /// </summary>
        /// <param name="wave"></param>
        public void SendMessage(string wave)
        {
            if (client != null)
            {
                bw.Write(client.Client.LocalEndPoint.ToString() + "+" + "Step.ToString()" + wave);
                bw.Flush();
            }
        }
    }
}
