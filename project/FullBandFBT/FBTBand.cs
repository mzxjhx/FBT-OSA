using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalcSpec;
namespace CWDM1To4
{
    /// <summary>
    /// 每段带宽一个类，分别计算该带宽里IL的最大值和最小值，以及最大值对应的波长值
    /// </summary>
    public class FBTBand :　IBand,ICompareIL
    {
        BandPar par = new BandPar();

        bool _isPass = false;
        /// <summary>
        /// 该波段合格
        /// </summary>
        public bool IsPass
        {
            get { return _isPass; }
            set { _isPass = value; }
        }

        /// <summary>
        /// IL值
        /// </summary>
        public BandPar Par
        {
            get { return par; }
            set { par = value; }
        }

        private double start;
        private double stop;
        /// <summary>
        /// 带宽的起始波长
        /// </summary>
        public double Start
        {
            get{
                return start;
            }
            set{
                start = value;
            }
        }
        /// <summary>
        /// 带宽的终止波长
        /// </summary>
        public double Stop
        {
            get{
                return stop;
            }
            set{
                stop = value;
            }
        }

        public FBTBand() { }

        public FBTBand(double art,double top)
        {
            this.start = art;
            this.stop = top;

        }

        //public BandPar getBandPar(double[] refpass, double[] y)
        //{
        //    BandPar par = new BandPar();
        //    if (start > stop)
        //    {
        //        par.MaxValue = 0;
        //        par.MinValue = 0;
        //    }
        //    else if(stop == start)//(stop - start < 0.2 && stop - start > 0)
        //    {
        //        par.MinValue = par.MaxValue = Math.Round(refpass[ReturnAxis(start)] - y[ReturnAxis(start)], 2);
        //    }
        //    else
        //    {
        //        double[] p = Calculate.MaxMinPoint(
        //                        refpass,
        //                        y,
        //                        ReturnAxis(start),
        //                        ReturnAxis(stop)
        //                        );
        //        par.MinValue = Math.Round(p[0], 2);
        //        par.MaxValue = Math.Round(p[1], 2);
        //    }
        //    return par;
        //}

        /// <summary>
        /// 计算带宽内的最大和最小值
        /// </summary>
        /// <param name="refpass"></param>
        /// <param name="y"></param>
        public void CalcBandPar(double[] refpass, double[] y)
        {
            if (start > stop)
            {
                par.MaxValue = 0;
                par.MinValue = 0;
            }
            else if (stop == start)//(stop - start < 0.2 && stop - start > 0)
            {
                par.MinValue = par.MaxValue = Math.Round(refpass[ReturnAxis(start)] - y[ReturnAxis(start)], 2);
            }
            else
            {
                double[] p = Calculate.MaxMinPoint(
                                refpass,
                                y,
                                ReturnAxis(start),
                                ReturnAxis(stop)
                                );
                par.MinValue = Math.Round(p[0], 2);
                par.MaxValue = Math.Round(p[1], 2);
            }
        }

        /// <summary>
        /// 根据波长从光谱曲线中找x轴数组坐标点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public int ReturnAxis(double wave)
        {
            int re = (int)((wave - 1260) * (SweepWave.Cpoints - 1) / (1650 - 1260));
            if (re < 0) re = 0;
            return re;
        }

        #region ICompareIL 成员

        /// <summary>
        /// 判断一个输出端的所有波段值，有一个不合格返回false
        /// </summary>
        /// <returns></returns>
        public bool ILPass()
        {
            return true;
        }

        #endregion
    }
}
