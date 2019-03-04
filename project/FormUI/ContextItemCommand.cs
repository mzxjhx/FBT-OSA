using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace FormUI
{
    public class ContextItemCommand:ToolStripMenuItem
    {
        ICommand Command;

        public ContextItemCommand() { }

        public ContextItemCommand(ICommand cmd)
        {
            this.Command = cmd;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Command != null)
                Command.Run();
        }
    }

}
