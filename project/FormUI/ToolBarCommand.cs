using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

namespace FormUI
{
    public partial class ToolBarCommand : ToolStripButton
    {
        ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public ToolBarCommand() { }

        public ToolBarCommand(ICommand cmd)
        {
            _command = cmd;
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_command != null)
                _command.Run();
        }
    }
}
