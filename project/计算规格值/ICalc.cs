using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    /// <summary>
    /// 计算透射接口
    /// </summary>
    public interface ICalc
    {
        /// <summary>
        /// 计算透射
        /// </summary>
        /// <param name="r"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double[] CalcPass(double[] r, double[] y);
        /// <summary>
        /// 计算反射
        /// </summary>
        /// <param name="r"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double[] CalcReflect(double[] r, double[] y);
    }
    ///// <summary>
    ///// 计算反射接口
    ///// </summary>
    //public interface ICalcReflect
    //{

    //}

    public interface ICalcAxisFormWaveLength
    {
        void CalcAxisFormWaveLength();
    }

}
