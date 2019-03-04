using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CWDM1To4.Class;

namespace CWDM1To4
{
    public partial class FrmSet : Form
    {
        public FrmSet()
        {
            InitializeComponent();
        }
        IniFile Set = new IniFile();

        /// <summary>
        /// 只写器件的参数
        /// </summary>
        private void WriteConfig()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0, len = int.Parse(toolStripComboBox1.Text); i < len; i++)
                {
                    Set.WriteString("CH" + (j + 1), "Par" + (i + 1) + "min", dataGridViewSet.Rows[i].Cells[j * 2].Value.ToString());
                }
                for (int i = 0, len = int.Parse(toolStripComboBox1.Text); i < len; i++)
                {
                    Set.WriteString("CH" + (j + 1), "Par" + (i + 1) + "max", dataGridViewSet.Rows[i].Cells[j * 2 + 1].Value.ToString());
                }
            }

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSet.Rows.Clear();
                //读规格参数
                for (int i = 0, len = int.Parse(toolStripComboBox1.Text); i < len; i++)
                {
                    dataGridViewSet.Rows.Add();
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridViewSet.Rows[i].Cells[j * 2].Value = Set.ReadString("CH" + (j + 1) + "", "Par" + (i + 1) + "min", "1");
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridViewSet.Rows[i].Cells[j * 2 + 1].Value = Set.ReadString("CH" + (j + 1) + "", "Par" + (i + 1) + "max", "1");
                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "(*.ini)|*.ini";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Set.FileName = ofd.FileName;
                }
            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            WriteConfig();
            ParCompare.Load(Set.FileName);
            Close();
        }

    }
}
