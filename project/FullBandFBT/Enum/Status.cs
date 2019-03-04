using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWDM1To4
{
    //枚举，测试阶段
    public enum Status
    {
        Use,
        Idle,
        Wait,
        Finish,
        Off
    }

    /// <summary>
    /// 枚举，测试阶段
    /// </summary>
    public enum SweepStep
    {
        None,
        PassNormal,
        PassLow,
        PassHigh,
        ReflectNormal,
        ReflectLow,
        ReflectHigh,
        RassRef,
        ReflectRef,
    }

}
