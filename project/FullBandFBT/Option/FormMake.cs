using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CalcSpec;
namespace CWDM1To4.Option
{
    public partial class FormMake : BaseOptionForm
    {
        public FormMake()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            txtBoxPB1.Text = inifile.ReadString("DWDMR", "PB1", "1");
            textBoxCenterWave1.Text = inifile.ReadString("DWDMR", "CenterWave1", "1");
            if (inifile.ReadString("DWDMR", "IS100G", "1") == "True")
                radioButton100G.Checked = true;
            else
                radioButton200G.Checked = true;
            textBoxJianju.Text = inifile.ReadString("DWDMR", "Jianju", "1");
        }

        public override bool StorePanelContents()
        {
            inifile.WriteString("DWDMR", "PB1", txtBoxPB1.Text);
            inifile.WriteString("DWDMR", "CenterWave1", textBoxCenterWave1.Text);
            inifile.WriteString("DWDMR", "IS100G", radioButton100G.Checked.ToString());
            inifile.WriteString("DWDMR", "Jianju", textBoxJianju.Text);

            Core.GetInstance().CW = double.Parse(textBoxCenterWave1.Text);
            Core.GetInstance().PB = double.Parse(txtBoxPB1.Text);
            Core.GetInstance().Is100G = radioButton100G.Checked;
            Core.GetInstance().Jianju = int.Parse(textBoxJianju.Text);
            Core.GetInstance().CalcAxisFormWaveLength();
            Core.GetInstance().DataGridView.Rows[0].Cells[2].Value = " DWDM反射：" + textBoxCenterWave1.Text;
            return true;
        }
    }
}
