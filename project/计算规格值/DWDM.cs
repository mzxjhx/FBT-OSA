using System;
using System.Collections.Generic;
using System.Text;
using OptionForm;
/*
DWDM100G产品规格测试计算方法
	用OSA扫描，得出器件的损耗曲线曲线，横坐标是波长纵坐标是损耗值。
1、	计算IL
带宽内检索损耗值的最大点，即IL
2、	计算Ripple
带宽内损耗值最大点-最小点，即抖动值
3、	计算ISO
左ITU波长对应IL值与右ITU波长应对IL值进行比较，小的一个即是隔离度
4、	中心波长
带宽PB内检索出IL的最小值，该小最值对应波长记为Wave，损耗值记为ILmin，在该值基础上加3dB记为ILRef作为基准点进行比较。ITU左->Wave波长范围内查找IL值与ILRef最相近的波长，记为WaveLeft ; Wave->ITU右波长范围内查找IL值与ILRef最相近的波长，记为WaveRight 。
		中心波长= （WaveLeft+WaveRight）/2
*/
namespace CalcSpec
{
    public class CalcDWDM : BaseWDM, ICalcAxisFormWaveLength, ICalc
    {

        private bool _is100G = true;
        private double _Region = 0.05;
        /// <summary>
        /// 密波分复用器件的ITU带宽，100G-0.8。200G-1.6
        /// </summary>
        double DWDMPB = 0.8;
        int AxisLeftPB1 = 0;
        int AxisRightPB1 = 0;

        int AxisStart = 1525;
        int AxisStop = 1610;

        double leftitu = 0;
        double rightitu = 0;

        public double CenterWave
        {
            get { return base._centerWave; }
        }

        public CalcDWDM()
        {
            SweepWave.Type = SweepModel.DWDM;
        }

        public void CalcAxisFormWaveLength()
        {

            AxisLeftPB1 = ReturnAxis(_centerWave - _pb, SweepModel.DWDM); 
            AxisRightPB1 = ReturnAxis(_centerWave + _pb, SweepModel.DWDM);
            AxisStart = ReturnAxis(SweepWave.StartWave, SweepModel.DWDM);
            AxisStop = ReturnAxis(SweepWave.StopWave, SweepModel.DWDM);
            if (_is100G == true)
                DWDMPB = 0.8;
            else
                DWDMPB = 1.6;

            leftitu = _centerWave - (DWDMPB - _pb);
            rightitu = _centerWave + (DWDMPB - _pb);
        }

        public override void UpdateDGV()
        {
            //base.UpdateDGV();
            DataGridView.Rows[0].Cells[1].Value = (_centerWave - _pb) + " ~ " + (_centerWave + _pb);
            DataGridView.Rows[0].Cells[2].Value = SweepWave.StartWave + " ~ " + leftitu + " & " + rightitu + " ~ " + SweepWave.StopWave;
        }

        public void PropertyChanged(object r, DwdmEventArgs e)
        {
            _centerWave = (e).CW1;
            _pb = (e).PB1;
            _is100G = (e).Is100G;
            _Region = (e).Region;
            CalcAxisFormWaveLength();
            UpdateDGV();
        }

        public double[] CalcPass(double[] refpass, double[] y)
        {
            double[] re = new double[4];
            double Iso = 0;
            double[] par = Calculate.CalcILRipple(refpass, y, AxisLeftPB1, AxisRightPB1); 

            #region 一段范围内找最小点，定义ISO

            Iso = Math.Min(refpass[ReturnAxis(leftitu, SweepModel.DWDM)] - y[ReturnAxis(leftitu, SweepModel.DWDM)], refpass[ReturnAxis(rightitu, SweepModel.DWDM)] - y[ReturnAxis(rightitu, SweepModel.DWDM)]);

            /*
            double[] isot = new double[_ISOBand.Length];
            for (int i = 0; i < _ISOBand.Length; i++)
            {
                string[] band = _ISOBand[i].Split('~');
                isot[i] = MinPoint(refpass, y, ReturnAxis(double.Parse(band[0]), _type), ReturnAxis(double.Parse(band[1]), _type));
            }
            Iso = isot[0];
            for (int i = 0; i < _ISOBand.Length; i++)
            {
                if (Iso > isot[i])
                    Iso = isot[i];
            }
            */
            #endregion

            //先算左边
            //Iso = refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1];

            //以下计算中心波长
            int left = ReturnAxis(_centerWave - DWDMPB, SweepModel.DWDM);
            int right = ReturnAxis(_centerWave + DWDMPB, SweepModel.DWDM);

            double waveleft = _centerWave - (DWDMPB - _pb);////////////////原来初始值为0
            double waveright = _centerWave + (DWDMPB - _pb);

            double minpoint = refpass[AxisLeftPB1] - y[AxisLeftPB1];
            int wavepoint = AxisLeftPB1;     //最小值对应数组索引
            for (int i = AxisLeftPB1; i <= AxisRightPB1; i++)   //带宽内检索IL最小值
            {
                if (minpoint > refpass[i] - y[i])
                {
                    minpoint = refpass[i] - y[i];
                    wavepoint = i;
                }
            }

            minpoint += 0.5;    //在最小值上下降0.5db，左右两边分别与该值比较，得出中心波长

            //分别向最小值的左右两边查找与minpoint最相匹配的点
            double temp = Math.Abs(refpass[wavepoint] - y[wavepoint] - minpoint);

            left = ReturnAxis(_centerWave - (DWDMPB - _pb), SweepModel.DWDM);
            right = ReturnAxis(_centerWave + (DWDMPB - _pb), SweepModel.DWDM);
            for (int i = wavepoint; i > left; i--)
            {
                double ff = Math.Abs(refpass[i] - y[i] - minpoint);
                if (temp > ff)
                {
                    temp = ff;
                    waveleft = Calculate.ReturnWave(i);
                }
            }

            temp = Math.Abs(refpass[wavepoint] - y[wavepoint] - minpoint);
            for (int i = wavepoint; i < right; i++)
            {
                double ff = Math.Abs(refpass[i] - y[i] - minpoint);
                if (temp > ff)
                {
                    temp = ff;
                    waveright = Calculate.ReturnWave(i);
                }
            }

            re[0] = Calculate.Float2(par[0]); //IL
            re[1] = Calculate.Float2(par[1]); //Ripple
            re[2] = Calculate.Float2(Iso);    //ISO
            re[3] = Calculate.Float2((waveleft + waveright) / 2);//中心波长
            return re;
        }

        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[3];

            //double[] max = new double[_ISOBand.Length];
            //double[] min = new double[_ISOBand.Length];

            //if (_ISOBand.Length == 2)   //单波，反射有两个波段
            //{
            double[] point1 = Calculate.MaxMinPoint(refpass, y, AxisStart, ReturnAxis(leftitu, SweepModel.DWDM));
            double[] point2 = Calculate.MaxMinPoint(refpass, y, ReturnAxis(rightitu,SweepModel.DWDM), AxisStop);

                //string[] band1 = _ISOBand[0].Split('~');
                //string[] band2 = _ISOBand[1].Split('~');
                //double[] point1 = MaxMinPoint(refpass, y, ReturnAxis(double.Parse(band1[0]), _type), ReturnAxis(double.Parse(band1[1]), _type));
                //double[] point2 = MaxMinPoint(refpass, y, ReturnAxis(double.Parse(band2[0]), _type), ReturnAxis(double.Parse(band2[1]), _type));
                par[0] = point1[1] > point2[1] ? point1[1] : point2[1]; //IL,最大值
                par[1] = point1[0] < point2[0] ? point1[0] : point2[0];
                par[1] = par[0] - par[1];                               //Ripple

            //}

            //在反射曲线中，反射带宽内插损的最大值和PB通道透射带宽内插损的最小值的差值，即为反射隔离度（ISO-r）
                par[2] = Calculate.MinPoint(refpass, y, AxisLeftPB1, AxisRightPB1) - par[0];    //ISO

                par[0] = Calculate.Float2(par[0]);    //il
                par[1] = Calculate.Float2(par[1]);    //ripple
                par[2] = Calculate.Float2(par[2]);    //iso
            return par;
        }

    }
}
