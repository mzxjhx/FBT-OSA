using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    /*
     * DWDM芯检测，只算反射
     */
    public class CoreTest: ICalc
    {

        public double[] CalcPass(double[] r, double[] y)
        {
            double[] par = new double[4];
            return par;
        }

        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[5];

            int axisleftitu = Core.GetInstance().axisleftitu, axisrightitu = Core.GetInstance().axisrightitu;

            double[] point1 = Calculate.MaxMinPoint(refpass, y, Core.GetInstance().AxisStart, axisleftitu);
            double[] point2 = Calculate.MaxMinPoint(refpass, y, axisrightitu, Core.GetInstance().AxisStop);

            par[0] = point1[1] > point2[1] ? point1[1] : point2[1]; //IL,最大值
            par[1] = point1[0] < point2[0] ? point1[0] : point2[0];
            par[1] = par[0] - par[1];                                  //Ripple

            double minpoint = refpass[axisleftitu] - y[axisleftitu];
            int Maxxais = 0;    //ITU带宽内IL的最大值对应数组点
            for (int i = axisleftitu; i < axisrightitu; i++)
            {
                if (minpoint < refpass[i] - y[i])
                {
                    minpoint = refpass[i] - y[i];
                    Maxxais = i;
                }
            }

            //在反射曲线中，反射带宽内插损的最大值和PB通道透射带宽内插损的最小值的差值，即为反射隔离度（ISO-r）
            par[2] = Calculate.MinPoint(refpass, y, Core.GetInstance().AxisLeftPB1, Core.GetInstance().AxisRightPB1) - par[0];    //ISO

            double waveleft = 0;
            double waveright = 0;

            double stardlever = 5 + par[0];              //反射IL下降5db，以此值为准

            double ff = 0;
            double min = Math.Abs(stardlever - refpass[axisleftitu] + y[axisleftitu]);
            for (int i = axisleftitu; i < Maxxais; i++)
            {
                ff = Math.Abs(stardlever - refpass[i] + y[i]);
                if (min > ff)
                {
                    min = ff;
                    waveleft = Calculate.ReturnWave(i);
                }
            }
            min = Math.Abs(stardlever - refpass[Maxxais] + y[Maxxais]);
            for (int i = Maxxais; i < axisrightitu; i++)
            {
                ff = Math.Abs(stardlever - refpass[i] + y[i]);
                if (min > ff)
                {
                    min = ff;
                    waveright = Calculate.ReturnWave(i);
                }
            }

            par[0] = Calculate.Float2(par[0]);    //il
            par[1] = Calculate.Float2(par[1]);    //ripple
            par[2] = Calculate.Float2(par[2]);    //iso
            par[3] = (waveleft + waveright) / 2;
            par[4] = ((waveleft + waveright) / 2 - Core.GetInstance().CW) / 0.006 + Core.GetInstance().Jianju;
            return par;

        }
    }

    public class Core : BaseWDM
    {
        static Core core = new Core();
        private Core() { }

        public static Core GetInstance()
        {
            if (core == null)
            {
                core = new Core();
            }
            return core;
        }

        private bool _is100G = true;

        private double _Region = 0.05;
        /// <summary>
        /// 密波分复用器件的ITU带宽，100G-0.8。200G-1.6
        /// </summary>
        double DWDMPB = 0.8;

        int _jianju = 100;

        public int AxisLeftPB1 = 0;
        public int AxisRightPB1 = 0;

        public int axisleftitu , axisrightitu ;

        public int AxisStart = 1525;
        public int AxisStop = 1610;

        double leftitu = 0;
        double rightitu = 0;

        public double CW
        {
            get { return base._centerWave; }
            set { base._centerWave = value; }
        }

        public double PB
        {
            get { return base._pb; }
            set { base._pb = value; }
        }

        public int Jianju
        {
            get { return _jianju; }
            set { _jianju = value; }
        }

        public bool Is100G
        {
            get { return _is100G; }
            set {
                _is100G = value;
                DWDMPB = _is100G == true ? 0.8 : 1.6;
            }
        }

        public void CalcAxisFormWaveLength()
        {

            AxisLeftPB1 = ReturnAxis(_centerWave - _pb, SweepModel.DWDM);
            AxisRightPB1 = ReturnAxis(_centerWave + _pb, SweepModel.DWDM);
            AxisStart = ReturnAxis(SweepWave.StartWave, SweepModel.DWDM);
            AxisStop = ReturnAxis(SweepWave.StopWave, SweepModel.DWDM);
            if (_is100G == true)
                DWDMPB = 0.8;
            else
                DWDMPB = 1.6;

            leftitu = _centerWave - (DWDMPB - _pb);
            rightitu = _centerWave + (DWDMPB - _pb);

            axisleftitu = ReturnAxis(leftitu, SweepModel.DWDM);
            axisrightitu = ReturnAxis(rightitu, SweepModel.DWDM);

        }
    }
}
