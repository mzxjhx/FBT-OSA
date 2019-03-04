using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using FormUI;
namespace CWDM1To4
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        Point mouseOff;//鼠标移动位置变量

        bool leftFlag;//标签是否为左键
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            LoadHistroy();
            this.comBoxUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.comBoxUser.AutoCompleteSource = AutoCompleteSource.ListItems;
            signleButton1.Click += new EventHandler(btnLogin_Click);
            signleButton2.Click += new EventHandler(btnQuit_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (comBoxUser.Text == "" || textBoxPassWord.Text == "")//判断用户名和密码是否为空
            {
                MessageBox.Show("用户名或密码不能为空！",//弹出消息对话框
                    "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;//退出事件
            }
            else
            {
                string name = comBoxUser.Text.Trim();//移除用户名前部和后部的空格
                string pwd = textBoxPassWord.Text.Trim();//移除密码前部和后部的空格
                if (!this.comBoxUser.AutoCompleteCustomSource.Contains(name))
                {
                    this.comBoxUser.AutoCompleteCustomSource.Add(name);
                }
                FrmMain.WorkID = name;
                DialogResult = DialogResult.OK;
                this.Close();
            }
       }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X,-e.Y);
                leftFlag = true;
            }
        }
        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag == true)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                Location = mouseSet;
            }
        }
        private void FormLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag == true)
            {
                leftFlag = false;
            }
        }

        private void FormLogin_Paint(object sender, PaintEventArgs e)
        {
            //投影文字
            Graphics g = this.CreateGraphics();
            //设置文本输出质量
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Font newFont = new Font("隶书", 28, FontStyle.Italic);
            Matrix matrix = new Matrix();
            //投射
            matrix.Shear(-1.5f, 0.0f);
            //缩放
            matrix.Scale(1, 0.5f);
            //平移
            matrix.Translate(130, 88);
            //对绘图平面实施坐标变换、、
            g.Transform = matrix;
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            SolidBrush colorBrush = new SolidBrush(Color.White);//.BlueViolet);
            string text = "OSA测试系统";
            //绘制阴影
            g.DrawString(text, newFont, grayBrush, new PointF(10, 30));
            g.ResetTransform();
            //绘制前景
            g.DrawString(text, newFont, colorBrush, new PointF(10, 30));
        }
        // 写登陆成功的用户名
        private void SaveHistroy()
        {
            string fileName = Path.Combine(Application.StartupPath, @"History.txt");
            StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF32);
            foreach (string name in comBoxUser.AutoCompleteCustomSource)
            {
                writer.WriteLine(name);
            }
            writer.Flush();
            writer.Close();

        }

        // 读登陆成功的用户名
        private void LoadHistroy()
        {
            string fileName = Path.Combine(Application.StartupPath, @"History.txt");
            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName, Encoding.Default);
                string name = reader.ReadLine();
                while (name != null)
                {
                    this.comBoxUser.AutoCompleteCustomSource.Add(name);
                    this.comBoxUser.Items.Add(name);
                    name = reader.ReadLine();
                }
                reader.Close();
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHistroy();
        }


    }
}
