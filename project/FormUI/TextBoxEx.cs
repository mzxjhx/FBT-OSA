using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FormUI
{
    public class TextBoxEx : TextBox
    {
         protected override void OnKeyPress(KeyPressEventArgs e)
         {
             base.OnKeyPress(e);
             if (Text.Length == 12 && e.KeyChar != 8)
             {
                 e.Handled = true;
             }
         }
    }
}
