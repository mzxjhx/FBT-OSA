using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsaServer
{
    //枚举，测试阶段
    public enum Step
    {
        pass_normal,
        pass_low,
        pass_high,
        reflect_normal,
        reflect_low,
        reflect_high,
        pass_reference,
        reflect_reference,
        retest,             //用于重测当前通道
        loginout
    }
}
