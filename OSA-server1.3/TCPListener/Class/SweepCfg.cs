using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsaServer
{
    public class SweepCfg
    {
        string _res = "1NM";
        /// <summary>
        /// 分辨率 1NM 0.05NM
        /// </summary>
        public string Res
        {
            get { return _res; }
            set
            {
                _res = value;
            }
        }

        /// <summary>
        /// 扫描灵敏度
        /// </summary>
        string _sens = "mid";

        public string Sens
        {
            get { return _sens; }
            set { _sens = value; }
        }

        string _Points = "10001";
        /// <summary>
        /// 扫描点数
        /// </summary>
        public string Points
        {
            get { return _Points; }
            set
            {
                _Points = value;
            }
        }
    }
}
