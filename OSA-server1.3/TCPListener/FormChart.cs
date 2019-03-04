using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace OsaServer
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            #region 定义显示曲线
            PointPairList listpass = new PointPairList();
            PointPairList listreflect = new PointPairList();

            //定义图表显示所用曲线，透射曲线和反射曲线
            LineItem curvepass;
            #endregion

            #region 光谱显示图表初始化
            //图表标题
            zedGraphControl.GraphPane.Title.Text = "OSA Trace";
            zedGraphControl.GraphPane.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.Title.FontSpec.Size = 18f;
            zedGraphControl.GraphPane.Title.FontSpec.Family = "宋体";//"楷体_GB2312";

            zedGraphControl.GraphPane.XAxis.Title.Text = "波长(nm)";
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 12f;
            zedGraphControl.GraphPane.XAxis.Title.FontSpec.Family = "宋体"; //"楷体_GB2312";


            zedGraphControl.GraphPane.YAxis.Title.Text = "功率(dB)";
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.FontColor = Color.Black;
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 12f;
            zedGraphControl.GraphPane.YAxis.Title.FontSpec.Family = "宋体"; //"楷体_GB2312";

            zedGraphControl.GraphPane.XAxis.Color = Color.White;
            zedGraphControl.GraphPane.YAxis.Color = Color.White;

            //zedGraphControl.GraphPane.XAxis.Scale.Max = 1880;
            //zedGraphControl.GraphPane.XAxis.Scale.Min = 880;

            zedGraphControl.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl.GraphPane.YAxis.Scale.Min = -100;

            zedGraphControl.GraphPane.YAxis.Scale.MajorStep = 10;

            zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 12;
            zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 12;

            zedGraphControl.GraphPane.YAxis.MajorGrid.Color = Color.White;
            zedGraphControl.GraphPane.XAxis.MajorGrid.Color = Color.White;

            zedGraphControl.GraphPane.YAxis.MinorGrid.Color = Color.White;
            zedGraphControl.GraphPane.XAxis.MinorGrid.Color = Color.White;

            zedGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;

            //zedGraphControl.GraphPane.XAxis.MinorGrid.IsVisible = true;
            //zedGraphControl.GraphPane.YAxis.MinorGrid.IsVisible = true;

            zedGraphControl.GraphPane.XAxis.Type = ZedGraph.AxisType.Linear;

            //背景色
            zedGraphControl.GraphPane.Chart.Fill = new Fill(Color.Black);
            #endregion
        }
    }
}
