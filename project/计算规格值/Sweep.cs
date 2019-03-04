using System;
using System.Collections.Generic;
using System.Text;
using OptionForm;

namespace CalcSpec
{
    public class SweepWave
    {
        static double _startWave = 1260;
        static double _stopWave = 1620;

        SweepModel _type = SweepModel.CWDM;

        public const int Cpoints = 10001;
        public const int Dpoints = 5001;

        /// <summary>
        /// 扫描器件类型
        /// </summary>
        public SweepModel Type
        {
            get { return _type; }
            set { 
                _type = value;

            }
        }

        /// <summary>
        /// 终止波长
        /// </summary>
        public double StopWave
        {
            get { return _stopWave; }
            set { _stopWave = value; }
        }
        /// <summary>
        /// 起始波长
        /// </summary>
        public double StartWave
        {
            get { return _startWave; }
            set { _startWave = value; }
        }

        public SweepWave()
        {

        }

        public static void Load(string file)
        {
            IniFile inifile = new IniFile();
            inifile.FileName = file;
            
            _startWave =  double.Parse( inifile.ReadString("Sweep", "StartWave", "1"));
            _stopWave = double.Parse( inifile.ReadString("Sweep", "StopWave", "1"));
            
        }
    }
}
