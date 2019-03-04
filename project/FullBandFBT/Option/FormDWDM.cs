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
    public partial class FormDWDM : BaseOptionForm
    {
        public FormDWDM()
        {
            InitializeComponent();

        }
        public override void LoadPanelContents()
        {
            //base.LoadPanelContents();
            txtBoxPB1.Text = inifile.ReadString("DWDM", "PB1", "1");
            textBoxCenterWave1.Text = inifile.ReadString("DWDM", "CenterWave1", "1");
            if (inifile.ReadString("DWDM", "IS100G", "1") == "True")
                radioButton100G.Checked = true;
            else
                radioButton200G.Checked = true;
            textBoxWaveRegion.Text = inifile.ReadString("DWDM", "WaveRegion", "1");
            textBoxISOBand.Text = inifile.ReadString("DWDM", "ISOBand", "1");
        }

        public override bool StorePanelContents()
        {
            if (!(Regex.IsMatch(textBoxCenterWave1.Text, @"^1\d{3}$|^1\d{3}\.\d{1,3}$") && Regex.IsMatch(txtBoxPB1.Text, @"^\d$|^\d\.\d{1,3}$")))
            {
                MessageBox.Show(" 不是有效数字！ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }

            inifile.WriteString("DWDM", "PB1", txtBoxPB1.Text);
            inifile.WriteString("DWDM", "CenterWave1", textBoxCenterWave1.Text);
            inifile.WriteString("DWDM", "IS100G", radioButton100G.Checked.ToString());
            inifile.WriteString("DWDM", "WaveRegion", textBoxWaveRegion.Text);
            inifile.WriteString("DWDM", "ISOBand", textBoxISOBand.Text);

            DwdmEventArgs args = new DwdmEventArgs();
            args.CW1 = double.Parse(textBoxCenterWave1.Text);
            args.PB1 = double.Parse(txtBoxPB1.Text);
            args.Is100G = radioButton100G.Checked;
            args.Region = double.Parse(textBoxWaveRegion.Text);
            args.IsoBand = textBoxISOBand.Text;
            Server.OnDWDMChanged(args);
            if (Sweep.GetInstance().TestModel == SweepModel.DWDM)
            {
                StatusBarServices.CenterWave = textBoxCenterWave1.Text;
                StatusBarServices.PB = txtBoxPB1.Text;
            }
                return true;
        }
    }
}
