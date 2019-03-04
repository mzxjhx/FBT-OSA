using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using OsaServer;
namespace OsaServer
{
    /// <summary>
    /// 扫描客户端，接收客户端的请求，并回传扫描曲线数据
    /// </summary>
    public class User
    {

        TcpClient _client;
        NetworkStream _netstream;
        BinaryReader _br;
        BinaryWriter _bw;
        string _ipAddress;
        string _port = "10000";
        int _userChannle = 1;


        string _res = "1NM";
        /// <summary>
        /// 1NM 0.02NM
        /// </summary>
        public string Res
        {
            get { return _res; }
            set { _res = value; }
        }

        string _Points = "10000";
        /// <summary>
        /// 扫描点数
        /// </summary>
        public string Points
        {
            get { return _Points; }
            set { _Points = value; }
        }


        public TcpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }
        /// <summary>
        /// 1拖4对就测试台位置
        /// </summary>
        public int UserChannel
        {
            get { return _userChannle; }
            set { _userChannle = value; }
        }
        /// <summary>
        /// 通讯端口
        /// </summary>
        public string Port 
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 扫描起始波长
        /// </summary>
        public string StartPoint = "1260";
        /// <summary>
        /// 扫描终止波长
        /// </summary>
        public string StopPoint = "1650";

        public User() { }

        public User(TcpClient client)
        {
            _client = client;
            _netstream = _client.GetStream();
            /*
             * BinaryReader便于字符串操作
             */
            _br = new BinaryReader(_netstream);
            _bw = new BinaryWriter(_netstream);
        }

        public void Close()
        {
            //netstream.Close();
            //client.Close();
            if (_br != null)
            {
                _br.Close();
            }
            if (_bw != null)
            {
                _bw.Close();
            }
            if (_client != null)
            {
                _client.Close();
            }
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="cmd"></param>
        public void SendCmd(string cmd)
        {
            _bw.Write(cmd);
            _bw.Flush();
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            return _br.ReadString();
        }
    }
}
