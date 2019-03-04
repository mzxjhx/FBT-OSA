using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FormUI
{
    /// <summary>
    /// 定制状态栏
    /// </summary>
    public class StatusStripCommand : StatusStrip
    {

        /// <summary>
        /// 部门名称
        /// </summary>
        ToolStripStatusLabel TimeBarPanel = new ToolStripStatusLabel();
        /// <summary>
        /// 用户名
        /// </summary>
        ToolStripStatusLabel UserNameBarPanel = new ToolStripStatusLabel();
        /// <summary>
        /// 连接状态
        /// </summary>
        ToolStripStatusLabel LinkStatusPanel = new ToolStripStatusLabel();
        /// <summary>
        /// 器件类型
        /// </summary>
        ToolStripStatusLabel TypePanel = new ToolStripStatusLabel();
        /// <summary>
        /// 中心波长
        /// </summary>
        ToolStripStatusLabel CenterWavePanel = new ToolStripStatusLabel();
        /// <summary>
        /// 带宽
        /// </summary>
        ToolStripStatusLabel PBPanel = new ToolStripStatusLabel();
        /// <summary>
        /// 公司名，右侧显示
        /// </summary>
        ToolStripStatusLabel CompanyNameStatusBarPanel = new ToolStripStatusLabel();
        ToolStripStatusLabel springLabel = new ToolStripStatusLabel();

        Timer _timer = new Timer();

        Control ctl;
        string _userName = "";
        string _status = "断开";
        Model _model = Model.CWDM;
        /// <summary>
        /// 服务器连接状态
        /// </summary>
        public string Status
        {
            get { return _status; }
            set {
                //if (_status == value)
                //    return;
                _status = value;
                LinkStatusPanel.Text = " 服务器状态：" + value;
            }
        }

        public Control Ctrl
        {
            get { return ctl; }
            set { ctl = value; }
        }
 
        public string UserName
        {
            get { return _userName; }
            set { 
                _userName = value;
                UserNameBarPanel.Text = " 工号： " + value;
            }
        }

        public Model Model
        {
            get { return _model; }
            set {
                _model = value;
                TypePanel.Text = " 器件类型：" + value.ToString();
            }
        }

        public string Wave
        {
            set {
                CenterWavePanel.Text = " 中心波长：" + value;
            }
        }

        public string PB
        {
            set {
                PBPanel.Text = " 带宽：" + value;
            }
        }

        public StatusStripCommand()
        {

            springLabel.Spring = true;

            TimeBarPanel.AutoSize = false;
            TimeBarPanel.Width = 180;

            UserNameBarPanel.AutoSize = false;
            UserNameBarPanel.Width = 110;

            CompanyNameStatusBarPanel.AutoSize = true;
            CompanyNameStatusBarPanel.Text = " 山东锐择光电科技有限公司 ";

            LinkStatusPanel.AutoSize  =false;
            LinkStatusPanel.Width = 130;
            LinkStatusPanel.Text = " 服务器状态：断开 ";

            TypePanel.AutoSize = false;
            TypePanel.Width = 150;
            TypePanel.Text = "  器件类型：CWDM ";

            CenterWavePanel.AutoSize = false;
            CenterWavePanel.Width = 160;
            CenterWavePanel.Text= " 中心波长： ";
            PBPanel.AutoSize = false;
            PBPanel.Width = 100;
            PBPanel.Text = "  带宽：";

            SizingGrip = false;

            Items.AddRange(new ToolStripItem[] { TimeBarPanel, UserNameBarPanel, LinkStatusPanel, TypePanel, 
                                                 CenterWavePanel,PBPanel, springLabel, CompanyNameStatusBarPanel });

            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            this.TimeBarPanel.Text = " 时间：" + DateTime.Now;
        }
    }
}
