using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/*
 * 单一颜色按键
 */

namespace FormUI
{
    public partial class SignleButton : Button//UserControl
    {
        ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }
        
        public enum MouseActionType
        {
            None,
            Hover,
            Click
        }

        public enum Style
        { 
            signle,
            Gradient
        }

        private MouseActionType mouseAction;
        private Style _colorstyle = Style.Gradient;

        [Description("背景颜色风格：纯色或渐变色")]
        [Category("外观")]
        public Style Colorstyle
        {
            get { return _colorstyle; }
            set { _colorstyle = value; }
        }

        private Color _baseColor = Color.DodgerBlue;
        private Color _enterColor = Color.RoyalBlue;
        private Color _clickColor = Color.DarkBlue;

        [Description("鼠标按下颜色")]
        [Category("外观")]
        public Color ClickColor
        {
            get { return _clickColor; }
            set { _clickColor = value; }
        }

        [Description("鼠标进入色")]
        [Category("外观")]
        public Color EnterColor
        {
            get { return _enterColor; }
            set { _enterColor = value; }
        }

        [Description("背景色")]
        [Category("外观")]
        public Color BaseColor
        {
            get { return _baseColor; }
            set 
            {
                _baseColor = value;
                this.Invalidate();
            }
        }

        public SignleButton()
        {
            //InitializeComponent();
            mouseAction = MouseActionType.None;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
               ControlStyles.UserPaint |
               ControlStyles.ResizeRedraw |
               ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.Opaque, false);
        }

        private GraphicsPath GetGraphicsPath(Rectangle rc, int r)
        {
            int x = rc.X, y = rc.Y, w = rc.Width, h = rc.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, r, r, 180, 90);				//Upper left corner
            path.AddArc(x + w - r, y, r, r, 270, 90);			//Upper right corner
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90);		//Lower right corner
            path.AddArc(x, y + h - r, r, r, 90, 90);			//Lower left corner
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            int btnOffset = 0;
            Color clr = this.BackColor;
            switch (mouseAction)
            {
                case MouseActionType.None:
                    clr = _baseColor;
                    break;
                case MouseActionType.Hover:
                    clr = _enterColor;
                    break;
                case MouseActionType.Click:
                    clr = _clickColor;
                    break;
                default:
                    break;
            }
            ///
            /// 创建按钮本身的图形
            /// 
            Rectangle rc = new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            GraphicsPath path1 = this.GetGraphicsPath(rc, 10);
            if (_colorstyle == Style.signle)
            {
                SolidBrush br1 = new SolidBrush(clr);
                g.FillPath(br1, path1);
                DrawString(g);
                br1.Dispose();
            }
            else
            {
                LinearGradientBrush br1 = new LinearGradientBrush(rc, Color.LightBlue, clr, LinearGradientMode.Vertical);
                g.FillPath(br1, path1);
                br1.Dispose();
            }
            DrawString(g);
        }

        public void DrawString(Graphics gr)
        {
            if (this.Text != "")
            {
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                Rectangle stringRectangle = this.ClientRectangle;
                gr.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), stringRectangle, drawFormat);

            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseAction = MouseActionType.Click;
                this.Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.mouseAction = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.mouseAction = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseHover(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.mouseAction = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.mouseAction = MouseActionType.None;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Command != null)
                Command.Run();
        }
    }
}
