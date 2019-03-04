using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormUI
{
    public partial class ToolStripItemCommand : ToolStripItem
    {
        IExternalCommand _command;
        public ToolStripItemCommand()
        {

        }

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
            if (_command != null)
                _command.Run();
        }
    }
}
