using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FormUI;

namespace FormUI
{
    public class CheckBoxCommand : CheckBox
    {
        ICommand Command;
        public CheckBoxCommand(ICommand cmd)
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
