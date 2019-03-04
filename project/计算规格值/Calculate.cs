using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    public class Calculate
    {

        /// <summary>
        /// 保留两位小数，返回浮点型数据
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static double Float2(double f)
        {
            double ff = 0;
            int i = (int)(f * 100 + 0.5);
            ff = (double)i / 100;
            return ff;
        }

        /// <summary>
        /// 按1260～1650，扫描10000个点来计算波长对应在数组中的坐标点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public static int ReturnAxis(double wave)
        {
            int re = (int)((wave - 1260) * 10000 / (1650 - 1260));
            if (re < 0) re = 0;
            return re;
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
            if (re < 0) re = 0;
            return re;
        }

        /// <summary>
        /// 根据数组坐标确定波长值
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double ReturnWave(double point)
        {
            return (point - 1) * (1610-1525) / SweepWave.Dpoints + 1525;

        }

        /// <summary>
        /// 取指定区域内最大值
        /// 两个输入参数均为负值，相减等正值
        /// </summary>
        /// <param name="reference">测试参考值</param>
        /// <param name="y">实测曲线y轴坐标点值</param>
        /// <param name="xAxis1">计算起始点</param>
        /// <param name="xAxis2">计算终止点</param>
        /// <returns></returns>
        public double MaxPoint(double[] reference, double[] y, int xAxis1, int xAxis2)
        {
            double max = reference[xAxis1] - y[xAxis1];
            for (int i = xAxis1; i <= xAxis2; i++)
            {
                if (max < reference[i] - y[i])
                {
                    max = reference[i] - y[i];
                }
            }
            return max;
        }

        /// <summary>
        /// 取指定区域内最小值
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="y"></param>
        /// <param name="xAxis1"></param>
        /// <param name="xAxis2"></param>
        /// <returns></returns>
        public static double MinPoint(double[] reference, double[] y, int xAxis1, int xAxis2)
        {
            double min = reference[xAxis1] - y[xAxis1];
            for (int i = xAxis1; i <= xAxis2; i++)
            {
                if (min > reference[i] - y[i])
                {
                    min = reference[i] - y[i];
                }
            }
            return min;
        }

        /// <summary>
        /// 返回最小值和最大值
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="y"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static double[] MaxMinPoint(double[] reference, double[] y, int start, int stop)
        {
            double[] point = new double[2];
            point[0] = reference[start] - y[start];
            point[1] = reference[start] - y[start];
            for (int i = start; i <= stop; i++)
            {
                if (point[0] > reference[i] - y[i])
                {
                    point[0] = reference[i] - y[i];
                }
                if (point[1] < reference[i] - y[i])
                {
                    point[1] = reference[i] - y[i];
                }
            }
            return point;   //point[0]是最小值，point[1]是最大值
        }

        /// <summary>
        /// 计算指定透射范围的Ripple、IL
        /// 输出数据只保留两位小数
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="y"></param>
        /// <param name="xstart"></param>
        /// <param name="xstop"></param>
        /// <returns></returns>
        public static double[] CalcILRipple(double[] reference, double[] y, int xstart, int xstop)
        {
            double min = reference[xstart] - y[xstart];
            double max = reference[xstart] - y[xstart];
            double[] par = new double[2];
            for (int i = xstart; i <= xstop; i++)
            {
                if (min > reference[i] - y[i])
                {
                    min = reference[i] - y[i];
                }
                if (max < reference[i] - y[i])
                {
                    max = reference[i] - y[i];
                }
            }
            min = max - min;
            par[0] = max;
            par[1] = min;
            return par;
        }
    }
}
