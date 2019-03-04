using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWDM1To4
{
    /// <summary>
    /// 多线程更新UI
    /// </summary>
    delegate void UpdateUI();
    delegate void UpdateChar(double[] x, double[] y);
}
