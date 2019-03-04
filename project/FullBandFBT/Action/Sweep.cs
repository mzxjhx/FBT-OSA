using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormUI;
//using SweepStatus;
using CalcSpec;
namespace CWDM1To4
{
    /// <summary>
    /// 记录扫描类型和扫描阶段，包括规格计算接口
    /// </summary>
    public class Sweep : ICommand
    {
        SweepStep _step = SweepStep.None;
        SweepModel _testModel = SweepModel.CWDM;

        static Sweep _sweep;

        public static Sweep GetInstance()
        {
            if (_sweep == null)
                _sweep = new Sweep();
            return _sweep;
        }

        public SweepStep Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public SweepModel TestModel
        {
            get { return _testModel; }
            set { _testModel = value; }
        }

        /// <summary>
        /// 计算透射和反射接口
        /// </summary>
        public ICalc CalcPassRef;

        public void Run()
        { 
            
        }
    }
}
