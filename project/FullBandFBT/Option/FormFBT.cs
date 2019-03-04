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
    public partial class FormFBT : BaseOptionForm
    {
        public FormFBT()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox1.TextChanged += new EventHandler(comboBox1_TextChanged);
        }

        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            Wavechange();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Wavechange();
        }

        void Wavechange()
        {
            dataGridViewSet.Rows.Clear();
            //读规格参数
            for (int i = 0, len = int.Parse(comboBox1.Text); i < len; i++)
            {
                dataGridViewSet.Rows.Add();
                string ss = "CH" + (i + 1) + "";
                dataGridViewSet.Rows[i].Cells[0].Value = inifile.ReadString("CH" + (i + 1) + "", "WAVE1", "1");
                dataGridViewSet.Rows[i].Cells[1].Value = inifile.ReadString("CH" + (i + 1) + "", "WAVE2", "1");

            }
        }

        public override void LoadPanelContents()
        {
            base.LoadPanelContents();
        }

        public override bool StorePanelContents()
        {
            //return base.StorePanelContents();
            for (int i = 0, len = int.Parse(comboBox1.Text); i < len; i++)
            {
                inifile.WriteString("CH" + (i + 1) + "", "WAVE1", dataGridViewSet.Rows[i].Cells[0].Value.ToString());
                inifile.WriteString("CH" + (i + 1) + "", "WAVE2", dataGridViewSet.Rows[i].Cells[1].Value.ToString());

            }
            return true;

        }
    }
}
