using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using SharpPlus.Ini;
using System.Drawing;
namespace CWDM1To4.Class
{
    /// <summary>
    /// 每输出端一堆参数
    /// </summary>
    public class Par
    {
        bool _isOK;

        public bool IsOK
        {
            get { return _isOK; }
            set { _isOK = value; }
        }

        double il1310;

        public double Il1310
        {
            get { return il1310; }
            set { il1310 = value; }
        }
        double il1490;

        public double Il1490
        {
            get { return il1490; }
            set { il1490 = value; }
        }
        double il1550;

        public double Il1550
        {
            get { return il1550; }
            set { il1550 = value; }
        }
        double il1625;

        public double Il1625
        {
            get { return il1625; }
            set { il1625 = value; }
        }
        double ilMax1310;

        public double IlMax1310
        {
            get { return ilMax1310; }
            set { ilMax1310 = value; }
        }

        double ilMin1310;

        public double IlMin1310
        {
            get { return ilMin1310; }
            set { ilMin1310 = value; }
        }

        double ilMax1550;

        public double IlMax1550
        {
            get { return ilMax1550; }
            set { ilMax1550 = value; }
        }
        double ilMin1550;

        public double IlMin1550
        {
            get { return ilMin1550; }
            set { ilMin1550 = value; }
        }

        public Par()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color CompareIL(double src, double des)
        {
            if (src > des) return Color.Black;
            else return Color.Red;
        }
    }

    public class FBT
    {

        public double[] EL;

        public double[] UL;

        public double[,] CR;
        public FBT()
        {
            EL = new double[4];
            UL = new double[4];
        }
    }

    public static class ParCompare
    {
        static Par par = new Par();
        //static double[,] ToCompare = new double[4,16];
        static BandPar[,] ToCompare = new BandPar[4, 8];
        public static void Load(string path)
        {

            IniFile inifile = new IniFile();
            inifile.FileName = path;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ToCompare[i, j] = new BandPar();
                    ToCompare[i, j].MinValue = double.Parse(inifile.ReadString("CH" + (i + 1), "Par" + (j + 1) + "min", "1"));
                    ToCompare[i, j].MaxValue = double.Parse(inifile.ReadString("CH" + (i + 1), "Par" + (j + 1) + "max", "1"));
                }
            }

        }
        /// <summary>
        /// 与设定规格IL值比较
        /// 2016-8-6更改
        /// </summary>
        /// <param name="ch">指定输出端</param>
        /// <param name="des">指定IL值</param>
        /// <returns></returns>
        //public static Color CompareIL(int ch, int i, double il)
        //{
        //    if (ToCompare[ch, i] > il) return Color.Black;
        //    else return Color.Red;
        //}
        /// <summary>
        /// 与设定规格IL值比较
        /// 2016-8-6更改
        /// </summary>
        /// <param name="ch">指定输出端</param>
        /// <param name="des">指定IL值</param>
        /// <returns></returns>
        public static Color CompareIL(int ch, int i, BandPar par)
        {
            if (ToCompare[ch, i].MaxValue > par.MaxValue && ToCompare[ch,i].MinValue < par.MinValue) return Color.Black;
            else return Color.Red;
        }


        /// <summary>
        /// 是否超规格值
        /// 2016-8-17更改
        /// </summary>
        /// <param name="ch">指定输出端</param>
        /// <param name="des">指定IL值</param>
        /// <returns></returns>
        //public static bool CmpIL(int ch, int i, double il)
        //{
        //    return ToCompare[ch, i] < il ? true : false;
        //}
        /// <summary>
        /// 是否超规格值
        /// 2016-8-17更改
        /// </summary>
        /// <param name="ch">指定输出端</param>
        /// <param name="des">指定IL值</param>
        /// <returns></returns>
        public static bool CmpIL(int ch, int i, BandPar par)
        {
            return (ToCompare[ch, i].MaxValue > par.MaxValue && ToCompare[ch, i].MinValue < par.MinValue) ? true : false;
        }

        public static double[] GetEL(Par[] par)
        {
            double pow = 0;
            double[] el = new double[4];
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (par[i].Il1310 != 0)
                {
                    pow += Math.Pow(10, -par[i].Il1310 / 10);
                }
            }
            el[0] = -10 * Math.Log10(pow);

            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (par[i].Il1490 != 0)
                {
                    pow += Math.Pow(10, -par[i].Il1490 / 10);
                }
            }
            el[1] = -10 * Math.Log10(pow);

            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (par[i].Il1550 != 0)
                {
                    pow += Math.Pow(10, -par[i].Il1550 / 10);
                }
            }
            el[2] = -10 * Math.Log10(pow);

            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (par[i].Il1625 != 0)
                {
                    pow += Math.Pow(10, -par[i].Il1625 / 10);
                }
            }
            el[3] = -10 * Math.Log10(pow);
            return el;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IL"></param>
        /// <param name="Channel">测试通道数</param>
        /// <returns></returns>
        public static double[] GetUL(Par[] par)
        {
            double max = 0;
            double min = 0;
            double[] ul = new double[4];

            max = min = par[0].Il1310;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (max < par[i].Il1310) max = par[i].Il1310;
                if (min > par[i].Il1310) min = par[i].Il1310;
            }
            ul[0] = max - min;

            max = min = par[0].Il1490;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (max < par[i].Il1490) max = par[i].Il1490;
                if (min > par[i].Il1490) min = par[i].Il1490;
            }
            ul[1] = max - min;

            max = min = par[0].Il1550;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (max < par[i].Il1550) max = par[i].Il1550;
                if (min > par[i].Il1550) min = par[i].Il1550;
            }
            ul[2] = max - min;

            max = min = par[0].Il1625;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                if (max < par[i].Il1625) max = par[i].Il1625;
                if (min > par[i].Il1625) min = par[i].Il1625;
            }
            ul[3] = max - min;

            return ul;
        }

        public static double[,] GetCR(Par[] par)
        {
            double pow = 0;
            double[,] el = new double[par.Length, 4];
            double[,] cr = new double[par.Length, 4];

            for (int i = 0, len = par.Length; i < len; i++)
            {
                el[i, 0] = Math.Pow(10, -par[i].Il1310 / 10);
                pow += el[i, 0];
            }
            for (int i = 0,len = par.Length; i < len; i++)
            {
                cr[i, 0] = el[i, 0] * 100 / pow;  
            }
            //1490
            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                el[i, 1] = Math.Pow(10, -par[i].Il1490 / 10);
                pow += el[i, 1];
            }
            for (int i = 0, len = par.Length; i < len; i++)
            {
                cr[i, 1] = el[i, 1] * 100 / pow;
            }
            //1550
            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                el[i, 2] = Math.Pow(10, -par[i].Il1550 / 10);
                pow += el[i, 2];
            }
            for (int i = 0, len = par.Length; i < len; i++)
            {
                cr[i, 2] = el[i, 2] * 100 / pow;
            }
            //1625
            pow = 0;
            for (int i = 0, len = par.Length; i < len; i++)
            {
                el[i, 3] = Math.Pow(10, -par[i].Il1625 / 10);
                pow += el[i, 3];
            }
            for (int i = 0, len = par.Length; i < len; i++)
            {
                cr[i, 3] = el[i, 3] * 100 / pow;
            }
            return cr;
        }

    }

    public class SaveInfo
    {
        public string ID;
        public string SN;
        public string Spec;
        public string ProductNo;
        //是否合格标志
        public bool IsPass;
        //测试完成标志
        public bool IsFinish;
        /// <summary>
        /// 等级
        /// </summary>
        public string Grade;

        public string CR;

        public double[] TraceX;
        public double[] PassRef;
        public double[,] TraceYPN;

    }
}
