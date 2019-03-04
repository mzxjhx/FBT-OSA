using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormUI;
namespace CWDM1To4
{
    public class About :ICommand
    {
        public void Run()
        {
            new FormAbout().ShowDialog();
        }
    }
}
