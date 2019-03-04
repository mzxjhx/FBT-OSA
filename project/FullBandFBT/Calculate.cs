using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 计算坐标点;
using CalcSpec;
namespace CWDM1To4
{
    /*
     * 透射和反射计算主体类，继承自接口，实现动态改变计算方式
     */
    /// <summary>
    /// 单透透射
    /// </summary>
    public class CalcSignlePass:ICalcPass
    {
        CalcAxis xAxis;
        public CalcSignlePass(CalcAxis x)
        {
            this.xAxis = x;
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
            double Iso;
            double[] par = AbstractCwdm.CalcILRipple(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1);
            if (xAxis.CenterWave1 > 1470 || xAxis.CenterWave1 <= 1480 && xAxis.StartWave == 1260 && xAxis.CenterWave1 > 1280)         //中心波长》1470的，透射ISO算两边
            {
                Iso = refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] < refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1] ? refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] : refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1];
            }
            else                            //中心波长小于1470，只算右边ITU的ISO;
            {
                Iso = refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1];
            }

            re[0] = Convert.Float2(par[0]);
            re[1] = Convert.Float2(par[1]);
            re[2] = Convert.Float2(Iso);
            re[3] = 0;
            return re;
        }
    }

    /// <summary>
    /// 双透透射
    /// </summary>
    public class CalcDualPass : ICalcPass
    {
        CalcAxis xAxis;
        public CalcDualPass(CalcAxis x)
        {
            this.xAxis = x;
        }
        /// <summary>
        /// 双波长计算规格参数
        /// IL、D_IL、Ripple、ISO
        /// </summary>
        /// <param name="refpass"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double[] CalcPass(double[] refpass, double[] y)
        {
            //return base.CalcParDual(refpass, y);
            double[] par = new double[4];
            double[] par1 = AbstractCwdm.CalcILRipple(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1);
            double[] par2 = AbstractCwdm.CalcILRipple(refpass, y, xAxis.XleftPB2, xAxis.XrightPB2);

            double il = par1[0] > par2[0] ? par1[0] : par2[0];
            double D_il = par1[0] < par2[0] ? par1[0] : par2[0];
            double ripple = par1[1] > par2[1] ? par1[1] : par2[1];

            //隔离度取两点中较小值
            double Iso1 = refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] < refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1] ? refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] : refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1];
            
            if (xAxis.CenterWave2 != 1610)
            {
                double iso2 = refpass[xAxis.XleftITU2] - y[xAxis.XleftITU2] < refpass[xAxis.XrightITU2] - y[xAxis.XrightITU2] ? refpass[xAxis.XleftITU2] - y[xAxis.XleftITU2] : refpass[xAxis.XrightITU2] - y[xAxis.XrightITU2];
                Iso1 = Iso1 < iso2 ? Iso1 : iso2;
            }

            par[0] = Convert.Float2(il);
            par[1] = Convert.Float2(ripple);
            par[2] = Convert.Float2(Iso1);
            par[3] = Convert.Float2(D_il);
            return par;
        }
    }

    /// <summary>
    /// FWDM透射
    /// </summary>
    public class CalcFwdmPass : ICalcPass
    {
        CalcAxis xAxis;
        public CalcFwdmPass(CalcAxis x)
        {
            this.xAxis = x;
        }
        
        #region ICalcPass 成员

        /// <summary>
        /// 计算FWDM器件的透射规格
        /// 中心波长及带宽单独定义一个CW3和PB3
        /// </summary>
        /// <param name="r">参考值</param>
        /// <param name="y">器件测试值</param>
        /// <returns>规格指标数组</returns>
        public double[] CalcPass(double[] refpass, double[] y)
        {
            double[] re = new double[4];
            double Iso;
            double[] par = AbstractCwdm.CalcILRipple(refpass, y, xAxis.XleftPB3, xAxis.XrightPB3);
            //if (xAxis.CenterWave1 > 1470 || xAxis.CenterWave1 == 1470 && xAxis.StartWave == 1260)         //中心波长》1470的，透射ISO算两边
            //{
            //    Iso = refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] < refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1] ? refpass[xAxis.XleftITU1] - y[xAxis.XleftITU1] : refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1];
            //}
            //else                            //中心波长小于1470，只算右边ITU的ISO;
            //{
            //    Iso = refpass[xAxis.XrightITU1] - y[xAxis.XrightITU1];
            //}
            Iso = refpass[xAxis.XleftITU3] - y[xAxis.XleftITU3] < refpass[xAxis.XrightITU3] - y[xAxis.XrightITU3] ? refpass[xAxis.XleftITU3] - y[xAxis.XleftITU3] : refpass[xAxis.XrightITU3] - y[xAxis.XrightITU3];

            re[0] = Convert.Float2(par[0]);
            re[1] = Convert.Float2(par[1]);
            re[2] = Convert.Float2(Iso);
            re[3] = 0;
            return re;
            
            //throw new NotImplementedException();
        }

        #endregion
    }


    /// <summary>
    /// 计算单波长反射
    /// </summary>
    public class CalcSignleReflect : ICalcReflect
    {
        CalcAxis xAxis;
        public CalcSignleReflect(CalcAxis x)
        {
            this.xAxis = x;
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
            if (xAxis.CenterWave1 > 1470 || xAxis.CenterWave1 < 1470 && xAxis.StartWave == 1260 && xAxis.CenterWave1 > 1280)  //可以算两边带宽的插损和隔离度
            {
                double[] point1 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.Xstart, xAxis.XleftITU1);   //最小值+最大值
                double[] point2 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XrightITU1, xAxis.Xstop);

                par[0] = point1[1] > point2[1] ? point1[1] : point2[1]; //IL,最大值
                par[1] = point1[0] < point2[0] ? point1[0] : point2[0]; 
                par[1] = par[0] - par[1];                               //Ripple

                //在反射曲线中，反射带宽内插损的最大值和PB通道透射带宽内插损的最小值的差值，即为反射隔离度（ISO-r）
                par[2] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1) - par[0];    //ISO
            }
            else //if (xAxis.CenterWave1 < 1470 && xAxis.StartWave == 1460)                   //只算右边反射带宽
            {
                double[] point1 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XrightITU1, xAxis.Xstop);
                par[1] = point1[1] - point1[0]; //ripple
                par[0] = point1[1];             //il
                point1[0] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1);
                par[2] = point1[0] - par[0];    //iso
            }
            par[0] = Convert.Float2(par[0]);    //il
            par[1] = Convert.Float2(par[1]);    //ripple
            par[2] = Convert.Float2(par[2]);    //iso
            return par;
        }
    }
    /// <summary>
    /// 计算双波长反射
    /// </summary>
    public class CalcDualReflect:ICalcReflect
    {
        CalcAxis xAxis;
        public CalcDualReflect(CalcAxis x)
        {
            this.xAxis = x;
        }
        /// <summary>
        /// 双中心波长器件只有全波
        /// 双透中，1610器件整个SP及右边PB带宽都不计算
        /// </summary>
        /// <param name="refpass"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[3];
            //三个PB带宽的最值
            double[] point1 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.Xstart, xAxis.XleftITU1);   //point[0]最小值、point[1]最大值
            double[] point2 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XrightITU1, xAxis.XleftITU2);
            double[] point3;
            if (xAxis.CenterWave2 == 1610 )
            {
                point3 = new double[2] { 10, 0 };   
            }
            else
            {
                point3 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XrightITU2, xAxis.Xstop);
            }
            
            point1[1] = point1[1] > point2[1] ? point1[1] : point2[1];
            if (xAxis.CenterWave2 != 1610)
            {
                point1[1] = point1[1] > point3[1] ? point1[1] : point3[1];  //point1[1]==il
            }

            point1[0] = point1[0] < point2[0] ? point1[0] : point2[0];
            if (xAxis.CenterWave2 != 1610)
            {
                point1[0] = point1[0] < point3[0] ? point1[0] : point3[0];  
            }

            point1[0] = point1[1] - point1[0];                          //point1[0]==ripple
            
            //隔离度
            point2[0] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1);     //对于反射，取透射带宽内的最小值 
            if (xAxis.CenterWave2 != 1610)
            {
                point2[1] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB2, xAxis.XrightPB2);     //曲线最高点，两条曲线求差后就是最小值
                point2[0] = point2[0] < point2[1] ? point2[0] : point2[1];      //取两者最小值
            }

            point2[0] -= point1[1];         //两个反射透射带宽内最大值，即曲线最高点

            par[0] = Convert.Float2(point1[1]);
            par[1] = Convert.Float2(point1[0]);
            //par[2] = point2[0] - point1[0] < point2[1] - point1[0] ? point2[0] - point1[0] : point2[1] - point1[0];
            par[2] = Convert.Float2(point2[0]);
            return par;
        }
    }

    /// <summary>
    /// 计算FWDM的反射，反射有两个波段，在两个波段上计算
    /// 中心波长按照WDM原定义双透波长计算
    /// </summary>
    public class CalcFWDMReflect : ICalcReflect
    {
        CalcAxis xAxis;
        public CalcFWDMReflect(CalcAxis x)
        {
            this.xAxis = x;
        }
        #region ICalcReflect 成员

        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[3];
            //二个PB带宽的最值
            double[] point1 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XleftPB1, xAxis.XrightPB1);   //point[0]最小值、point[1]最大值
            double[] point2 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XleftPB2, xAxis.XrightPB2);
            //double[] point3;
            //if (xAxis.CenterWave2 == 1610)
            //{
            //    point3 = new double[2] { 10, 0 };
            //}
            //else
            //{
            //    point3 = AbstractCwdm.MaxMinPoint(refpass, y, xAxis.XrightITU2, xAxis.Xstop);
            //}

            point1[1] = point1[1] > point2[1] ? point1[1] : point2[1];//两个波段里面IL较大者
            //if (xAxis.CenterWave2 != 1610)
            //{
            //    point1[1] = point1[1] > point3[1] ? point1[1] : point3[1];  //point1[1]==il
            //}

            point1[0] = point1[0] < point2[0] ? point1[0] : point2[0];//两个波段里IL较小者
            //if (xAxis.CenterWave2 != 1610)
            //{
            //    point1[0] = point1[0] < point3[0] ? point1[0] : point3[0];
            //}

            point1[0] = point1[1] - point1[0];                          //point1[0]==ripple

            //隔离度
            point2[0] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB3, xAxis.XrightPB3);     //对于反射，取透射带宽内的最小值 
            //if (xAxis.CenterWave2 != 1610)
            //{
            //    point2[1] = AbstractCwdm.MinPoint(refpass, y, xAxis.XleftPB2, xAxis.XrightPB2);     //曲线最高点，两条曲线求差后就是最小值
            //    point2[0] = point2[0] < point2[1] ? point2[0] : point2[1];      //取两者最小值
            //}

            point2[0] -= point1[1];         //两个反射透射带宽内最大值，即曲线最高点

            par[0] = Convert.Float2(point1[1]);
            par[1] = Convert.Float2(point1[0]);
            //par[2] = point2[0] - point1[0] < point2[1] - point1[0] ? point2[0] - point1[0] : point2[1] - point1[0];
            par[2] = Convert.Float2(point2[0]);
            return par;

        }

        #endregion
    }


    /// <summary>
    /// 保留两位小数函数
    /// </summary>
    public class Convert
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
        public static int ReturnPoint(double wave)
        {
            return (int)((wave - 1260) * 10000 / (1650 - 1260) + 1);
        }
    }
}
