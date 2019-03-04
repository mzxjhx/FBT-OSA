using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    public class CalcFBT : BaseWDM, ICalcAxisFormWaveLength, ICalc
    {

        public void CalcAxisFormWaveLength()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 顺序1310 1490 1550 1625 1310大小 1550大小
        /// </summary>
        /// <param name="r"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double[] CalcPass(double[] refpass, double[] y)
        {
            double[] re = new double[8];
            double[] IL;

            IL = Calculate.MaxMinPoint(
                refpass, 
                y, 
                ReturnAxis(1310 - 1),
                ReturnAxis(1310 + 1)
                );
            re[0] = Math.Round(IL[1], 2);  //1310

            IL = Calculate.MaxMinPoint(
                refpass,
                y,
                ReturnAxis(1490 - 1),
                ReturnAxis(1490 + 1)
                );
            re[1] = Math.Round(IL[1], 2); //1490

            IL = Calculate.MaxMinPoint(
                refpass,
                y,
                ReturnAxis(1550 - 1),
                ReturnAxis(1550 + 1)
                );
            re[2] = Math.Round(IL[1], 2);  //1550

            IL = Calculate.MaxMinPoint(
                refpass,
                y,
                ReturnAxis(1625 - 1),
                ReturnAxis(1625 + 1)
                );
            re[3] = Math.Round(IL[1], 2);  //1550

            IL = Calculate.MaxMinPoint(
                refpass,
                y,
                ReturnAxis(1310 - 40),
                ReturnAxis(1310 + 40)
                );
            re[4] =  Math.Round(IL[1], 2);  //1310
            re[5] =  Math.Round(IL[0], 2);

            IL = Calculate.MaxMinPoint(
                refpass,
                y,
                ReturnAxis(1550 - 40),
                ReturnAxis(1550 + 40)
                );
            re[6] = Math.Round(IL[1], 2);  //1310
            re[7] = Math.Round(IL[0], 2);

            return re;
        }

        public double[] CalcReflect(double[] r, double[] y)
        {
            return new double[2] { 0, 0 };
        }

    }
}
