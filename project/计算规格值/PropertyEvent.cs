using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{

    public class SweepWaveEventArgs : EventArgs
    {
        public string StartWave { get; set; }
        public string StopWave { get; set; }
    }

    public class SignleEventArgs : EventArgs
    {
        protected double _cw = 0;
        protected double _pb = 0;

        protected double[] _ISOBand1;
        protected double[] _ISOBand2;

        public double CW1 {
            get { return _cw; }
            set { _cw = value; }
        }
        public double PB1 {
            get { return _pb; }
            set { _pb = value; }
        }

        public SignleEventArgs()
        {}

        public SignleEventArgs(double cw,double pb)
        {
            _cw = cw;
            _pb = pb;
        }
    }

    //public class DualEventArgs : SignleEventArgs
    //{
    //    public double CW2 { get; set; }
    //    public double PB2 { get; set; }
    //}

    public class DwdmEventArgs : SignleEventArgs
    {
        private bool _is100G = true;
        private double _Region = 0.05;
        private string _isoBand = "";

        public string IsoBand
        {
            get { return _isoBand; }
            set { _isoBand = value; }
        }
        /// <summary>
        /// 是否是100G产品类型
        /// </summary>
        public bool Is100G {
            get { return _is100G; }
            set {
                _is100G = value;
            }
        }

        public double Region
        {
            get { return _Region; }
            set { _Region = value; }
        }

        public DwdmEventArgs() { }

        public DwdmEventArgs(double cw,double pb,bool Is100G,double re,string iso):base(cw,pb)
        {
            _is100G = Is100G;
            _Region = re;
            _isoBand = iso;
        }
    }

    public class DWDM_R_Args : EventArgs
    {
        private double _cw = 0;

        public double Cw
        {
            get { return _cw; }
            set { _cw = value; }
        }
        private double _pb = 0;

        public double Pb
        {
            get { return _pb; }
            set { _pb = value; }
        }
        private bool _is100G = true;

        /// <summary>
        /// 是否是100G产品类型
        /// </summary>
        public bool Is100G
        {
            get { return _is100G; }
            set
            {
                _is100G = value;
            }
        }

        private int _jianju = 0;
        public int Jianju
        {
            get { return _jianju; }
            set { _jianju = value; }
        }

        public DWDM_R_Args()
        { }
        
    }

    public class FwdmEventArgs : EventArgs
    {
        public string PassBand { get; set; }
        public string ReflectBand { get; set; }
 
        public FwdmEventArgs()
        {

        }
    }
}
