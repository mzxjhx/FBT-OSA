using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

/*
 * 
 *数据查询
 *数据筛选
 *数据导出
 *
 */
namespace CWDM1To4
{
    public partial class FormReview : Form
    {
        public FormReview()
        {
            InitializeComponent();
        }

        private string TimeFrom = "", TimeTo = "";

        DataSet dsCWDM = new DataSet();     //定义器件数据库
 
        System.Data.DataTable dt;

        BackgroundWorker bgw = new BackgroundWorker();
        string IP = @"server=192.168.0.222;uid=sa;pwd=rayzer;database=db_FBT";

        private void FormReview_Load(object sender, EventArgs e)
        {
 
        }

        /// <summary>
        /// 统计数据库信息
        /// </summary>
        private void Tongji()
        {
            int Pass = 0;
            labelTotal.Text = (dgvCWDM.RowCount - 1).ToString();
            for (int i = 0; i < dgvCWDM.RowCount-1; i++)
            {
                if (dgvCWDM.Rows[i].Cells["合格"].Value.ToString() == "Pass")
                {
                    Pass++;
                }
            }
            labelPass.Text = Pass.ToString();
            float per = (float)Pass / (dgvCWDM.RowCount - 1);
            labelPercent.Text = (per * 100).ToString("0.00") + "%"; 
        }

        /// <summary>
        /// 查找子函数
        /// </summary>
        /// <param name="select"></param>
        private void Query(string select)
        {
            try
            {

                DataSet ds = new DataSet();

                string query = @"select tb_FBTFullBand.*,tb_FullBandOut1.*,tb_FullBandOut2.*,tb_FullBandOut3.*,tb_FullBandOut4.                                * from tb_FBTFullBand 
                                left join tb_FullBandOut1 on tb_FBTFullBand.SN = tb_FullBandOut1.SN 
                                left join tb_FullBandOut2 on tb_FullBandOut1.SN = tb_FullBandOut2.SN
                                left join tb_FullBandOut3 on tb_FullBandOut2.SN = tb_FullBandOut3.SN
                                left join tb_FullBandOut4 on tb_FullBandOut3.SN = tb_FullBandOut4.SN where " + select;
                using (SqlConnection con = new SqlConnection(IP))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    con.Close();
                }

                dt = ds.Tables[0];
                dgvCWDM.DataSource = dt;
                //Tongji();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// 条件累加筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            string select = "";
            if (checkBoxProductNo.Checked == true)
            {
                select += "tb_FBTFullBand.制造单号 = '" + textBoxPNo.Text + "'";
            }
            if (checkBoxSPEC.Checked == true)
            {
                select += "and tb_FBTFullBand.器件类型 = '" + textBoxSPEC.Text + "'";
            }
            if (checkBoxSN.Checked == true)
            {
                select += "and tb_FBTFullBand.SN = '" + textBoxSN.Text + "'";
            }
            if (checkBoxID.Checked == true)
            {
                select += "and tb_FBTFullBand.工号 = '" + textBoxID.Text + "'";
            }
            if (checkBoxDate.Checked == true)
            {
                TimeFrom = dateTimePickerFrom.Value.ToShortDateString();
                TimeTo = dateTimePickerTo.Value.ToShortDateString();
                select += "and tb_FBTFullBand.日期 >= '" + TimeFrom + "' and tb_FBTFullBand.日期 <='" + TimeTo + " 23:59'";

            }
            if (select == "")
            {
                MessageBox.Show(" 请选择筛选条件 ！" ,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (select.Substring(0, 3) == "and")
            {
                select = select.Substring(3, select.Length - 3);
            }

            Query(select);
        }

        /// <summary>
        /// 按日期查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDate_Click(object sender, EventArgs e)
        {
            TimeFrom = dateTimePickerFrom.Value.ToShortDateString();
            //TimeFrom += "00:00";
            TimeTo = dateTimePickerTo.Value.ToShortDateString();
            TimeTo += " 23:59";
            Query("tb_FBTFullBand.日期 >= '" + TimeFrom + "' and tb_FBTFullBand.日期<='" + TimeTo + "'");
        }

        /// <summary>
        /// 导出Excel数据分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "(*.xls)|*.xls";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                OutCWDM(sf.FileName);
                MessageBox.Show("  导出完毕 ！","保存",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// 导出数据分析函数
        /// 器件格式
        /// </summary>
        /// <param name="savepath">传入保存地址</param>
        private void OutCWDM(string savepath)
        {
            //ExcelDoc = ExcelApp.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\CWDM.xls");// (@"D:\CWDM.xls");
            //ws = (Excel.Worksheet)ExcelDoc.Sheets[1];

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        ws.Cells[i + 3, j + 1] = dt.Rows[i][j];
            //    }
            //}
            //ExcelDoc.SaveAs(savepath, Nothing, Nothing, Nothing, Nothing, Nothing,
            //                Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);
            //ExcelDoc.Close(Nothing, System.Windows.Forms.Application.StartupPath + "\\CWDM.xls", Nothing);
            //ExcelApp.Quit();
        }
    }
}
