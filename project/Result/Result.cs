using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Result
{
    public partial class Result : UserControl
    {
        public Result()
        {
            InitializeComponent();
        }

        private PassOrFailed _result = PassOrFailed.None;
        [CategoryAttribute("自定义属性")]
        [Description("输出结果")]
        public PassOrFailed ShowResult
        {
            get { return _result; }
            set 
            {
                _result = value;
                base.Invalidate(true);//这句激活Paint事件
            }
        }

        private int _fontsize = 16;
        [Description("结果字体大小")]
        public int Fontsize
        {
            get { return _fontsize; }
            set 
            {
                _fontsize = value;
                base.Invalidate(true);//这句激活Paint事件,paint事件里包括onpaint方法                
            }
        }

        [Description("控件标题")]
        private string _title = "测试结果：";
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                base.Invalidate(true);//这句激活Paint事件
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = this.CreateGraphics();

            Font aFont = new Font("Arial", _fontsize, FontStyle.Bold | FontStyle.Italic);
            Rectangle rect = new Rectangle(5, 25, this.Width - 10, this.Height - 10);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            switch (_result)
            {
                case PassOrFailed.None:
                    g.DrawString("", aFont, Brushes.Blue, rect, sf);
                    break;
                case PassOrFailed.Pass:
                    g.DrawString("Pass",aFont,Brushes.Green,rect,sf);
                    break;
                case PassOrFailed.Failed:
                    g.DrawString("Failed", aFont, Brushes.Red, rect, sf);
                    break;
                default:
                    break;
            }

            //g.DrawString("Pass", Font,Brushes.Black,rect);
        }

        /// <summary>
        /// 绘制控件标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Result_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = this.CreateGraphics();
            Font aFont = new Font("Arial", 10, FontStyle.Regular );
            Rectangle rect = new Rectangle(0, 0, this.Width - 20, this.Height - 30);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            g.DrawString(_title, aFont, Brushes.Black, rect, sf);
        }
    }
}
