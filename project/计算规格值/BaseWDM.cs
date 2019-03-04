using System;
using System.Collections.Generic;
using System.Text;
using OptionForm;
using System.Windows.Forms;

namespace CalcSpec
{
    public class BaseWDM //: SweepWave
    {
        SweepWave _sweepWave = new SweepWave();
        protected IniFile inifile = new IniFile();
        public string inifilename = "";
        /// <summary>
        /// 带宽左波长
        /// </summary>
        protected double _leftPB = 0;
        /// <summary>
        /// 带宽右波长
        /// </summary>
        protected double _rightPB = 0;

        protected double _centerWave = 0;
        protected double _pb = 0;

        /// <summary>
        /// 隔离度计算波段
        /// </summary>
        protected string[] _ISOBand ;

        private DataGridView _dataGridView = null;

        public DataGridView DataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        public SweepWave SweepWave
        {
            get { return _sweepWave; }
            set { _sweepWave = value; }
        }

        public BaseWDM()
        {
            
        }
        /// <summary>
        /// 读取规格指标
        /// </summary>
        public virtual void Load()
        { 
            
        }
        /// <summary>
        /// 更新波段表格显示
        /// </summary>
        public virtual void UpdateDGV()
        { 
            
        }
        /// <summary>
        /// 根据波长从光谱曲线中找x轴数组坐标点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public int ReturnAxis(double wave)
        {
            int re = (int)((wave - 1260) * (SweepWave.Cpoints - 1) / (1650 - 1260));
            if (re < 0) re = 0;
            return re;
        }
        /// <summary>
        /// 按1260～1650，C系列扫描10001点。DWDM扫描5001点
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public int ReturnAxis(double wave, SweepModel ty)
        {
            int re = 0;

            if (ty == SweepModel.DWDM)
            {
                re = (int)((wave - 1525) * (SweepWave.Dpoints - 1) / (1610 - 1525));
                //if (re >= 21500) re = 5000;
            }
            else
            {
                re = (int)((wave - 1260) * (SweepWave.Cpoints - 1) / (1650 - 1260));
                //if (re >= 10000) re = 10000;
            }
            if (re < 0) re = 0;
            return re;
        }
    }
    /// <summary>
    /// 扫描器件类型
    /// </summary>
    public enum SweepModel
    {
        CWDM,
        Dual,
        FWDM,
        DWDM,
    }

}
