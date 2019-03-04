using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OptionForm;
//using SharpPlus.Ini;
using System.Windows.Forms;
using CWDM1To4.Class;
using CalcSpec;
using FormUI;

namespace CWDM1To4.Option
{
    public class BaseOptionForm : AbstractOptionPanel
    {
        protected IniFile inifile = null;
        //protected PropServer property = null;
        public PropertyServer Server = new PropertyServer();
        public BaseOptionForm()
        {
            inifile = new IniFile();
            inifile.FileName = Application.StartupPath + @"\config.ini";
        }
    }


}
