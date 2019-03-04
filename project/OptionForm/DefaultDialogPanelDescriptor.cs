using System;
using System.Collections.Generic;

using System.Text;
using OptionForm;
namespace OptionForm
{
    public class DefaultDialogPanelDescriptor : IDialogPanelDescriptor
    {
        string _id = "";
        string _label;
        IDialogPanel _dialogpanel;

        public string ID
        {
            get { return _id; }
        }

        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }

        public IDialogPanel DialogPanel
        {
            set { _dialogpanel = value; }
            get { return _dialogpanel; }
        }
    }
}
