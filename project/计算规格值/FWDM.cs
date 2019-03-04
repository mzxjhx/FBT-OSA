using System;
using System.Collections.Generic;
using System.Text;
using OptionForm;

namespace CalcSpec
{
    public class CalcFWDM: BaseWDM, ICalcAxisFormWaveLength, ICalc
    {
        string[] PassBand;
        string[] ReflectBand;
        /// <summary>
        /// 透射，单点算ISO还是带宽内计算ISO,默认按带宽计算
        /// </summary>
        public static bool IsPassBandISO = true;

        /// <summary>
        /// 反射，单点算ISO还是带宽内计算ISO,默认按带宽计算
        /// </summary>
        public static bool IsRefBandISO = true;

        public static string[] PassISOPoints;
        public static string[] RefISOPoints;

        public CalcFWDM()
        {

        }

        public void CalcAxisFormWaveLength()
        {
            
        }

        public override void UpdateDGV()
        {
            //base.UpdateDGV();
            string pa = PassBand[0];
            if (PassBand.Length == 2)
                pa += " & " + PassBand[1];
            else if(PassBand.Length ==3)
                pa += " & " + PassBand[1] + " & " + PassBand[2];
            DataGridView.Rows[0].Cells[1].Value = pa;
            
            pa = ReflectBand[0];
            if (ReflectBand.Length == 2)
                pa += " & " + ReflectBand[1];
            else if (ReflectBand.Length == 3)
                pa += " & " + ReflectBand[1] + " & " + ReflectBand[2];
            DataGridView.Rows[0].Cells[2].Value = pa;
        }

        public void PropertyChanged(object r, FwdmEventArgs e)
        {
            //base.PropertyChanged(r, e);
            PassBand = e.PassBand.Split('&');
            ReflectBand = e.ReflectBand.Split('&');
            UpdateDGV();
        }

        public double[] CalcPass(double[] refpass, double[] y)
        {
            double[] re = new double[4];
            double Iso = 0;
            double[] max = new double[PassBand.Length];
            double[] min = new double[PassBand.Length];
            for (int i = 0; i < PassBand.Length; i++)
            {
                string[] band = PassBand[i].Split('~');
                double[] tt = Calculate.MaxMinPoint(refpass, 
                    y,
                    ReturnAxis(double.Parse(band[0])),
                    ReturnAxis(double.Parse(band[1]))
                    );
                max[i] = tt[1];
                min[i] = tt[0];
            }

            for (int i = 1; i < PassBand.Length; i++)
            {
                if (max[0] < max[i]) max[0] = max[i];
                if (min[0] > min[i]) min[0] = min[i];
            }
            re[0] = Calculate.Float2(max[0]);
            re[1] = Calculate.Float2(max[0] - min[0]);
            
            //ISO
            //min = new double[ReflectBand.Length];
            //for (int i = 0; i < ReflectBand.Length; i++)
            //{
            //    string[] band = ReflectBand[i].Split('~');
            //    min[i] = MinPoint(refpass,
            //        y,
            //        ReturnAxis(double.Parse(band[0]), _type),
            //        ReturnAxis(double.Parse(band[1]), _type));
            //}

            //for (int i = 0; i < ReflectBand.Length; i++)
            //{
            //    if (min[0] > min[i]) min[0] = min[i];
            //}
            if (IsPassBandISO)  //计算带宽内ISO
            {
                min = new double[ReflectBand.Length];
                for (int i = 0; i < ReflectBand.Length; i++)
                {
                    string[] band = ReflectBand[i].Split('~');
                    min[i] = Calculate.MinPoint(refpass,
                        y,
                        ReturnAxis(double.Parse(band[0])),
                        ReturnAxis(double.Parse(band[1])));
                }

                for (int i = 0; i < ReflectBand.Length; i++)
                {
                    if (min[0] > min[i]) min[0] = min[i];
                }
                Iso = Math.Abs(min[0] - re[0]);

            }
            else
            {
                Iso = Math.Min(refpass[ReturnAxis(double.Parse(PassISOPoints[0]))] - y[ReturnAxis(double.Parse(PassISOPoints[0]))],                               refpass[ReturnAxis(double.Parse(PassISOPoints[1]))] - y[ReturnAxis(double.Parse(PassISOPoints[1]))]);
            }

            re[2] = Calculate.Float2(Iso);
            re[3] = 0;
            return re;
        }

        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] re = new double[3];
            double Iso = 0;
            double[] max = new double[ReflectBand.Length];
            double[] min = new double[ReflectBand.Length];
            for (int i = 0; i < ReflectBand.Length; i++)
            {
                string[] band = ReflectBand[i].Split('~');
                double[] tt = Calculate.MaxMinPoint(refpass,
                    y,
                    ReturnAxis(double.Parse(band[0])),
                    ReturnAxis(double.Parse(band[1]))
                    );
                max[i] = tt[1];
                min[i] = tt[0];
            }

            for (int i = 1; i < ReflectBand.Length; i++)
            {
                if (max[0] < max[i]) max[0] = max[i];
                if (min[0] > min[i]) min[0] = min[i];
            }
            re[0] = Calculate.Float2(max[0]);
            re[1] = Calculate.Float2(max[0] - min[0]);

            //ISO
            if (IsRefBandISO)   //计算带宽内ISO
            {
                min = new double[PassBand.Length];
                for (int i = 0; i < PassBand.Length; i++)
                {
                    string[] band = PassBand[i].Split('~');
                    min[i] = Calculate.MinPoint(refpass,
                        y,
                        ReturnAxis(double.Parse(band[0])),
                        ReturnAxis(double.Parse(band[1])));
                }

                for (int i = 0; i < PassBand.Length; i++)
                {
                    if (min[0] > min[i]) min[0] = min[i];
                }
                Iso = Math.Abs(min[0] - re[0]);

            }
            else
            {
                Iso = Math.Min(refpass[ReturnAxis(double.Parse(RefISOPoints[0]))] - y[ReturnAxis(double.Parse(RefISOPoints[0]))],                                 refpass[ReturnAxis(double.Parse(RefISOPoints[1]))] - y[ReturnAxis(double.Parse(RefISOPoints[1]))]);

            }
            re[2] = Calculate.Float2(Iso);
            //re[3] = 0;
            return re;
        }

        public override void Load()
        {
            base.Load();
            inifile.FileName = inifilename;

        }
    }
}
