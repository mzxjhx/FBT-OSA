using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptionForm;
using CalcSpec;
using System.Text.RegularExpressions;
using FormUI;
namespace CWDM1To4.Option
{
    public partial class FormSignle : BaseOptionForm
    {
        public FormSignle()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            //base.LoadPanelContents();
            txtBoxPB1.Text = inifile.ReadString("CWDM", "PB1", "1");
            textBoxCenterWave1.Text = inifile.ReadString("CWDM", "CenterWave1", "1");
        }

        public override bool StorePanelContents()
        {
            if (!(Regex.IsMatch(textBoxCenterWave1.Text, @"^1\d{3}$|^1\d{3}\.\d{1,3}$") && Regex.IsMatch(txtBoxPB1.Text, @"^\d$|^\d\.\d{1,3}$")))
            {
                MessageBox.Show(" 不是有效数字！ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }

            inifile.WriteString("CWDM", "PB1", txtBoxPB1.Text);
            inifile.WriteString("CWDM", "CenterWave1", textBoxCenterWave1.Text);
            Server.OnSignleChanged(new SignleEventArgs()
            {
                CW1 = double.Parse(textBoxCenterWave1.Text),
                PB1 = double.Parse(txtBoxPB1.Text)
            });
            if (Sweep.GetInstance().TestModel == SweepModel.CWDM)
            {
                StatusBarServices.CenterWave = textBoxCenterWave1.Text;
                StatusBarServices.PB = txtBoxPB1.Text;
            }
            return true;
            //return base.StorePanelContents();
        }
    }
}
