using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptionForm;
using FormUI;
using CalcSpec;
namespace CWDM1To4.Option
{
    public partial class FormDual : BaseOptionForm
    {
        public FormDual()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            //base.LoadPanelContents();
            txtBoxPB1.Text = inifile.ReadString("DUAL", "PB1", "1");
            txtBoxPB2.Text = inifile.ReadString("DUAL", "PB2", "1");
            textBoxCenterWave1.Text = inifile.ReadString("DUAL", "CenterWave1", "1");
            textBoxCenterWave2.Text = inifile.ReadString("DUAL", "CenterWave2", "1");
        }

        public override bool StorePanelContents()
        {
            inifile.WriteString("DUAL", "PB1", txtBoxPB1.Text);
            inifile.WriteString("DUAL", "PB2", txtBoxPB2.Text);
            inifile.WriteString("DUAL", "CenterWave1", textBoxCenterWave1.Text);
            inifile.WriteString("DUAL", "CenterWave2", textBoxCenterWave2.Text);
            if (Sweep.GetInstance().TestModel == SweepModel.Dual)
            {
                StatusBarServices.CenterWave = textBoxCenterWave1.Text + " + " + textBoxCenterWave2.Text;
                StatusBarServices.PB = txtBoxPB1.Text + " + " + txtBoxPB2.Text;
            }
            return true;
            //return base.StorePanelContents();
        }
    }
}
