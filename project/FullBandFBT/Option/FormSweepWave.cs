using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptionForm;
using CalcSpec;
namespace CWDM1To4.Option
{
    public partial class FormSweepWave : BaseOptionForm
    {
        public FormSweepWave()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            txtBoxStartWave.Text = inifile.ReadString("Sweep", "StartWave", "1");
            textBoxStopWave.Text = inifile.ReadString("Sweep", "StopWave", "1");
            ipAddressControl.Text = inifile.ReadString("Server", "IP", "1");
        }

        public override bool StorePanelContents()
        {
            inifile.WriteString("Sweep", "StartWave", txtBoxStartWave.Text);
            inifile.WriteString("Sweep", "StopWave", textBoxStopWave.Text);
            inifile.WriteString("Server", "IP", ipAddressControl.Text);
            //同步到sweepwave类
            SweepWave.Load(base.inifile.FileName);
            return true;
        }
    }
}
