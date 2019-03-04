using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using FormUI;

namespace FormUI
{
    public partial class BtnCommand : Button
    {
        ICommand Command;
        public BtnCommand(ICommand cmd)
        {
            Command = cmd;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Command.Run();
        }
    }
}
