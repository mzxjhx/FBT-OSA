using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace FormUI
{
    /*
     * 状态栏控制类
     */
    public static class StatusBarServices
    {
        static StatusStripCommand statusBar = null;
        static string _status;
        static Model _model = Model.CWDM;
        static string _id;

        public static string ID
        {
            set {
                _id = value;
                statusBar.UserName = _id;
            }
        }

        /// <summary>
        /// 连接扫描状态
        /// </summary>
        public static string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                statusBar.Status = value;
            }
        }
        public static Model Model
        {
            get { return _model; }
            set {
                _model = value;
                statusBar.Model = value;
            }
        }

        public static void Initialize()
        {
            statusBar = new StatusStripCommand();
        }

        public static Control Control
        {
            get
            {
                return statusBar;
            }
        }

        public static string CenterWave
        {
            set {
                statusBar.Wave = value;
            }
        }

        public static string PB
        {
            set {
                statusBar.PB = value;
            }
        }
    }

    public enum Model
    {
        CWDM,
        DualCwdm,
        FWDM,
        DWDM
    }
}
