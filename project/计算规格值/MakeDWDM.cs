using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    public class MakeDWDM : BaseWDM, ICalcAxisFormWaveLength, ICalc
    {

        private bool _is100G = true;
        private double _Region = 0.05;
        int _jianju = 0;

        int AxisLeftPB1 = 0;
        int AxisRightPB1 = 0;

        int AxisLeftITU = 0;
        int AxisRightITU = 0;
        int AxisStart = 0;
        int AxisStop = 0;
        double DWDMPB = 0;

        public double CenterWave
        {
            get { return base._centerWave; }
        }
        public bool Is100G
        {
            get { return _is100G; }
            set {
                _is100G = value;
                DWDMPB = 0.8;
                if (_is100G == false) DWDMPB = 1.6;
            }
        }
        public MakeDWDM()
        {
            _type = SweepModel.DWDM;
        }

        public void CalcAxisFormWaveLength()
        {
            AxisLeftPB1 = ReturnAxis(_centerWave - _pb, _type);
            AxisRightPB1 = ReturnAxis(_centerWave + _pb, _type);

            AxisLeftITU = ReturnAxis(_centerWave - (DWDMPB - _pb), _type);
            AxisRightITU = ReturnAxis(_centerWave + (DWDMPB - _pb), _type);

            AxisStart = ReturnAxis(StartWave, _type);
            AxisStop = ReturnAxis(StopWave, _type);
        }

        public override void UpdateDGV()
        {
            //base.UpdateDGV();
            DataGridView.Rows[0].Cells[1].Value = (_centerWave - _pb) + " ~ " + (_centerWave + _pb);
            DataGridView.Rows[0].Cells[2].Value = StartWave + " ~ " + (_centerWave - _pb) + " & " + (_centerWave + _pb) + " ~ " + StopWave;
        }

        public void PropertyChanged(object r, DWDM_R_Args e)
        {
            _centerWave = e.Cw;
            _pb = e.Pb;
            Is100G = e.Is100G;
            _jianju = e.Jianju;
            CalcAxisFormWaveLength();
            UpdateDGV();
        }

        public double[] CalcPass(double[] r, double[] y)
        {
            double[] re = new double[4];
            return re;
        }

        public double[] CalcReflect(double[] refpass, double[] y)
        {
            double[] par = new double[5];

            double[] point1 = MaxMinPoint(refpass, y, AxisStart, AxisLeftITU);   //最小值+最大值
            double[] point2 = MaxMinPoint(refpass, y, AxisRightITU, AxisStop);

            par[0] = point1[1] > point2[1] ? point1[1] : point2[1]; //IL,最大值
            par[1] = point1[0] < point2[0] ? point1[0] : point2[0];
            par[1] = par[0] - par[1];                               //Ripple

            double minpoint = refpass[AxisLeftITU] - y[AxisLeftITU];
            int Maxxais = 0;    //ITU带宽内IL的最大值对应数组点
            for (int i = AxisLeftITU; i < AxisRightITU; i++)
            {
                if (minpoint < refpass[i] - y[i])
                {
                    minpoint = refpass[i] - y[i];
                    Maxxais = i;
                }
            }

            //在反射曲线中，反射带宽内插损的最大值和PB通道透射带宽内插损的最小值的差值，即为反射隔离度（ISO-r）
            par[2] = MinPoint(refpass, y, AxisLeftPB1, AxisRightPB1) - par[0];    //ISO

            double waveleft = 0;
            double waveright = 0;

            double stardlever = 5 + par[0];              //反射IL下降5db，以此值为准

            double ff = 0;
            double min = Math.Abs(stardlever - refpass[AxisLeftITU] + y[AxisRightITU]);
            for (int i = AxisLeftITU; i < Maxxais; i++)
            {
                ff = Math.Abs(stardlever - refpass[i] + y[i]);
                if (min > ff)
                {
                    min = ff;
                    waveleft = ReturnWave(i);
                }
            }
            min = Math.Abs(stardlever - refpass[Maxxais] + y[Maxxais]);
            for (int i = Maxxais; i < AxisRightITU; i++)
            {
                ff = Math.Abs(stardlever - refpass[i] + y[i]);
                if (min > ff)
                {
                    min = ff;
                    waveright = ReturnWave(i);
                }
            }

            par[0] = Float2(par[0]);    //il
            par[1] = Float2(par[1]);    //ripple
            par[2] = Float2(par[2]);    //iso
            par[3] = (waveleft + waveright) / 2;
            par[4] = (par[3] - _centerWave) / 0.006 + _jianju;

            return par;
        }

    }
}
