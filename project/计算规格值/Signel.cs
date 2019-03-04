using System;
using System.Collections.Generic;
using System.Text;

using OptionForm;
namespace CalcSpec
{
    /// <summary>
    /// 单透透射
    /// </summary>
    public class CalcSignle : BaseWDM, ICalcAxisFormWaveLength, ICalc
    {

        int AxisLeftPB1 = 0;
        int AxisRightPB1 = 0;

        int AxisLeftITU = 0;
        int AxisRightITU = 0;

        int AxisStart = 0;
        int AxisStop = 0;
        /// <summary>
        /// 是否1通道器件，如1311、1511
        /// </summary>
        bool _isCh1 = false;

        public CalcSignle()
        {
            base.SweepWave.Type = SweepModel.CWDM;
        }

        public double CenterWave
        {
            get
            {
                return _centerWave;
            }
            set
            {
                _centerWave = value;
                if (_centerWave % 10 == 1)
                    _isCh1 = true;
            }
        }

        public double PB
        {
            get
            {
                return _pb;
            }
            set
            {
                _pb = value;
                CalcAxisFormWaveLength();//重新计算带宽
            }
        }

        public void CalcAxisFormWaveLength()
        {
            

            AxisLeftPB1 = ReturnAxis(_centerWave - _pb);
            AxisRightPB1 = ReturnAxis(_centerWave + _pb);

            AxisLeftITU = ReturnAxis(_centerWave - (20 - _pb));
            AxisRightITU = ReturnAxis(_centerWave + (20 - _pb));

            AxisStart = ReturnAxis(SweepWave.StartWave);
            AxisStop = ReturnAxis(1620);

        }

        public override void UpdateDGV()
        {
            //base.UpdateDGV();
            base.DataGridView.Rows[0].Cells[1].Value = (_centerWave - _pb).ToString() + " ~ " + (_centerWave + _pb);
            base.DataGridView.Rows[0].Cells[2].Value = SweepWave.StartWave + " ~ " + (_centerWave - _pb).ToString() + " & " + (_centerWave + _pb).ToString() + " ~ " + SweepWave.StopWave;
        }

        public void PropertyChanged(object r, SignleEventArgs e)
        {
            //base.PropertyChanged(r, e);
            _centerWave = e.CW1;
            _pb = e.PB1;
            CalcAxisFormWaveLength();
            UpdateDGV();
        }

        /// <summary>
        /// 计算单中心波长规格参数
        /// CalcILRipple返回il和ripple
        /// 为兼容双透器件，单透器件计算透射，也返回四个数据，D_IL值给0
        /// </summary>
        /// <param name="refpass"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double[] CalcPass(double[] refpass, double[] y)
        {
            double[] re = new double[4];
            double Iso = 0;
            double[] par = Calculate.CalcILRipple(refpass, y, AxisLeftPB1, AxisRightPB1);
            /*
             *透射ISO：反射波段内最小值-IL

            if (AxisStart > AxisLeftITU)
                Iso = MinPoint(refpass, y, AxisRightITU, AxisStop) - par[0];
            else
            {
                double minl = MinPoint(refpass, y, AxisStart, AxisLeftITU);
                double minr = MinPoint(refpass, y, AxisRightITU, AxisStop);
                Iso = (minl < minr ? minl : minr) - par[0];                
            }
             */
            /*
             * 左右ITU比较单点 
             */
            if (AxisStart >= AxisLeftITU)//只算右边
                Iso = refpass[AxisRightITU] - y[AxisRightITU];
            else if (AxisStop < AxisRightITU)//只算左边
                Iso = refpass[AxisLeftITU] - y[AxisLeftITU];
            else//两边都算
                Iso = Math.Min(refpass[AxisLeftITU] - y[AxisLeftITU], refpass[AxisRightITU] - y[AxisRightITU]);

            //if (_centerWave > 1470 || _centerWave <= 1480 && StartWave == 1260 && _centerWave > 1280)         //中心波长》1470的，透射ISO算两边
            //{
            //    Iso = Math.Min(refpass[AxisLeftITU] - y[AxisLeftITU], refpass[AxisRightITU] - y[AxisRightITU]);
            //}
            //else                            //中心波长小于1470，只算右边ITU的ISO;
            //{
            //    Iso = refpass[AxisRightITU] - y[AxisRightITU];
            //}

            re[0] = Calculate.Float2(par[0]);
            re[1] = Calculate.Float2(par[1]);
            re[2] = Calculate.Float2(Iso);
            re[3] = 0;
            return re;
        }

        /// <summary>
        /// 计算指定带宽内的插损、ripple、隔离度
        /// 输入参考值，y轴坐标点
        /// 由于隔离度是pb+-12.5范围，所以根据中心波长及计算波长，要情况计算
        /// 存在半波器件和全波器件 ，半波器件是以1460为界
        /// </summary>
        /// <param name="refpass"></param>
        /// <param name="y"></param>
        /// <returns>输出数组，il和ripple</returns>
        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[3];

            //double[] max = new double[_ISOBand.Length];
            //double[] min = new double[_ISOBand.Length];

            //if (_ISOBand.Length == 2)   //单波，反射有两个波段
            //{
            //    string[] band = _ISOBand[0].Split('~');
            //    double[] point1 = MaxMinPoint(refpass, y, AxisStart, ReturnAxis(double.Parse(band[0])));
            //    double[] point2 = MaxMinPoint(refpass, y, ReturnAxis(double.Parse(band[1])), AxisStop);

            //    par[0] = point1[1] > point2[1] ? point1[1] : point2[1]; //IL,最大值
            //    par[1] = point1[0] < point2[0] ? point1[0] : point2[0];
            //    par[1] = par[0] - par[1];                               //Ripple
            //}

            if (AxisStart >= AxisLeftITU)//只算右边
            {
                double[] minr = Calculate.MaxMinPoint(refpass, y, AxisRightITU, AxisStop);
                par[0] = minr[1];
                par[1] = minr[1] - minr[0];
            }
            else if (AxisStop <= AxisRightITU)//只算左边
            {
                double[] minl = Calculate.MaxMinPoint(refpass, y, AxisStart, AxisLeftITU);
                par[0] = minl[1];
                par[1] = minl[1] - minl[0];
            }
            else//两边都算
            {
                double[] minl = Calculate.MaxMinPoint(refpass, y, AxisStart, AxisLeftITU);
                double[] minr = Calculate.MaxMinPoint(refpass, y, AxisRightITU, AxisStop);
                par[0] = minl[1] > minr[1] ? minl[1] : minr[1];
                par[1] = par[0] - (minl[0] < minr[0] ? minl[0] : minr[0]);                
            }
            //par[0] = minl[1] > minr[1] ? minl[1] : minr[1];
            //par[1] = par[0] - (minl[0] < minr[0] ? minl[0] : minr[0]);

            //在反射曲线中，反射带宽内插损的最大值和PB通道透射带宽内插损的最小值的差值，即为反射隔离度（ISO-r）
            par[2] = Calculate.MinPoint(refpass, y, AxisLeftPB1, AxisRightPB1) - par[0];    //ISO

            par[0] = Calculate.Float2(par[0]);    //il
            par[1] = Calculate.Float2(par[1]);    //ripple
            par[2] = Calculate.Float2(par[2]);    //iso
            return par;

        }

        public override void Load()
        {
            base.Load();
            inifile.FileName = inifilename;
            PB = double.Parse(inifile.ReadString("CWDM", "PB1", "1"));
            CenterWave = double.Parse(inifile.ReadString("CWDM", "CenterWave1", "1"));
            CalcAxisFormWaveLength();
            UpdateDGV();
        }
    }
}
