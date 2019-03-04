using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpPlus.Ini;
using OptionForm;
using CWDM1To4.Class;

namespace CWDM1To4.Option
{
    public partial class FormParameter : BaseOptionForm
    {
        public FormParameter()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            //base.LoadPanelContents();

            //规格参数
            txtBoxIL1310.Text = inifile.ReadString("Parameter", "IL1310", "1");
            txtBoxIL1490.Text = inifile.ReadString("Parameter", "IL1490", "1");
            txtBoxIL1550.Text = inifile.ReadString("Parameter", "IL1550", "1");
            txtBoxIL1625.Text = inifile.ReadString("Parameter", "IL1625", "1");
            //txtBoxMax1310.Text = inifile.ReadString("Parameter", "ILMax1310", "1");
            //txtBoxMin1310.Text = inifile.ReadString("Parameter", "ILMin1310", "1");
            //txtBoxMax1550.Text = inifile.ReadString("Parameter", "ILMax1550", "1");
            //txtBoxMin1550.Text = inifile.ReadString("Parameter", "ILMin1550", "1");
        }

        public override bool StorePanelContents()
        {
            //return base.StorePanelContents();
            //写规格设定参数
            inifile.WriteString("Parameter", "IL1310", txtBoxIL1310.Text);
            inifile.WriteString("Parameter", "IL1490", txtBoxIL1490.Text);
            inifile.WriteString("Parameter", "IL1550", txtBoxIL1550.Text);
            inifile.WriteString("Parameter", "IL1625", txtBoxIL1625.Text);
            //inifile.WriteString("Parameter", "ILMax1310", txtBoxMax1310.Text);
            //inifile.WriteString("Parameter", "ILMin1310", txtBoxMin1310.Text);
            //inifile.WriteString("Parameter", "ILMax1550", txtBoxMax1550.Text);
            //inifile.WriteString("Parameter", "ILMin1550", txtBoxMin1550.Text);
            //Parameter.GetSingleton().Load(inifile.FileName);
            ParCompare.Load(inifile.FileName);
            return true;
        }
    }
}
