using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CWDM1To4.Option
{
    public partial class FormCOM : BaseOptionForm
    {
        public FormCOM()
        {
            InitializeComponent();
        }

        public override void LoadPanelContents()
        {
            //base.LoadPanelContents();
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            foreach (var item in portNames)
            {
                comboBox1.Items.Add(item);
            }
        }

        public override bool StorePanelContents()
        {
            //return base.StorePanelContents();
            inifile.WriteString("COM", "port", comboBox1.Text);
            return true;
        }
    }
}
