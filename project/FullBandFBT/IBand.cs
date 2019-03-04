using System;
using System.Collections.Generic;
using System.Text;

namespace CWDM1To4
{
    /// <summary>
    /// 接口：每输出端起止波长，计算规格值
    /// </summary>
    public interface IBand
    {
        double Start { get; set; }
        double Stop { get; set; }

        //BandPar getBandPar(double[] refpass, double[] y);
        void CalcBandPar(double[] refpass, double[] y);
    }

    /// <summary>
    /// 比较各带宽内的IL值
    /// </summary>
    public interface ICompareIL
    {
        /// <summary>
        /// 比较，合格返回True
        /// </summary>
        /// <returns></returns>
        bool IsPass { get; set; }
    }
}
