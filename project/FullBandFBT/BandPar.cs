using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWDM1To4
{
   /// <summary>
    /// 计算所得值：带宽内最大、最小值及最大值对应波长
    /// </summary>
    public class BandPar{

        double maxWave, maxValue, minValue;

        public double MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public double MaxWave
        {
            get { return maxWave; }
            set { maxWave = value; }
        }

        public BandPar() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mw">最大值波长</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        public BandPar(double mw, double max, double min)
        {
            maxWave = mw;
            maxValue = max;
            minValue = min;
        }
    }
}
