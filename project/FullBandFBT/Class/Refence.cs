using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalcSpec;

namespace CWDM1To4.Class
{
    public class Refence
    {
        static DataGridView dgvreference = null;
        public static void PassRefence(DataGridView dgv,double[] r,SweepModel model)
        {
            dgvreference = dgv;
            if (model != SweepModel.DWDM)
            {
                dgvreference.Rows[0].Cells[1].Value = r[ReturnAxis(1260, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[2].Value = r[ReturnAxis(1360, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[3].Value = r[ReturnAxis(1460, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[4].Value = r[ReturnAxis(1560, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[5].Value = r[ReturnAxis(1610, SweepModel.CWDM)].ToString("0.00");
            }
            else
            {
                dgvreference.Rows[0].Cells[1].Value = r[ReturnAxis(1525, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[2].Value = r[ReturnAxis(1540, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[3].Value = r[ReturnAxis(1560, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[4].Value = r[ReturnAxis(1580, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[0].Cells[5].Value = r[ReturnAxis(1600, SweepModel.DWDM)].ToString("0.00");
            }
        }

        public static void RefRefence(DataGridView dgv, double[] r, SweepModel model)
        {
            dgvreference = dgv;
            if (model != SweepModel.DWDM)
            {
                dgvreference.Rows[1].Cells[1].Value = r[ReturnAxis(1260, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[2].Value = r[ReturnAxis(1360, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[3].Value = r[ReturnAxis(1460, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[4].Value = r[ReturnAxis(1560, SweepModel.CWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[5].Value = r[ReturnAxis(1610, SweepModel.CWDM)].ToString("0.00");
            }
            else
            {
                dgvreference.Rows[1].Cells[1].Value = r[ReturnAxis(1525, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[2].Value = r[ReturnAxis(1540, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[3].Value = r[ReturnAxis(1560, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[4].Value = r[ReturnAxis(1580, SweepModel.DWDM)].ToString("0.00");
                dgvreference.Rows[1].Cells[5].Value = r[ReturnAxis(1600, SweepModel.DWDM)].ToString("0.00");
            }
        }
        /// <summary>
        /// 根据波长从光谱曲线中找x轴数组坐标点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public int ReturnAxis(double wave)
        {
            return (int)((wave - 1260) * 10000 / (1650 - 1260));
        }
        /// <summary>
        /// 按1260～1650，C系列扫描10001点。DWDM扫描5001点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public static int ReturnAxis(double wave, SweepModel ty)
        {
            int re = 0;

            if (ty == SweepModel.DWDM)
            {
                re = (int)((wave - 1525) * 5000 / (1610 - 1525));
                //if (re >= 21500) re = 5000;
            }
            else
            {
                re = (int)((wave - 1260) * 10000 / (1650 - 1260));
                //if (re >= 10000) re = 10000;
            }
            return re;
        }
    }
}
