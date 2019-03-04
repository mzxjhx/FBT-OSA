using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FormUI;

namespace FormUI
{
    public class ComboBoxCommand : ComboBox
    {
        ISelectChange Command;
        public ComboBoxCommand(ISelectChange cmd)
        {
            Command = cmd;
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            base.OnSelectedItemChanged(e);
            Command.Change(this.SelectedItem.ToString());
        }
    }
}
