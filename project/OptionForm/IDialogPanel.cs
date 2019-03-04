using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace OptionForm
{
    public enum DialogMessage
    {
        OK,
        Cancel,
        Help,
        Next,
        Prev,
        Finish,
        Activated
    }

    public interface IDialogPanel
    {
        /// <summary>
        /// Some panels do get an object which they can customize, like
        /// Wizard Dialogs. Check the dialog description for more details
        /// about this.
        /// </summary>
        //object CustomizationObject
        //{
        //    get;
        //    set;
        //}

        Control Control
        {
            get;
        }

        /// <returns>
        /// true, if the DialogMessage could be executed.
        /// </returns>
        bool ReceiveDialogMessage(DialogMessage message);

        bool StorePanelContents();

        event EventHandler EnableFinishChanged;
    }

    public interface IDialogPanelDescriptor
    {
        /// <value>
        /// Returns the ID of the dialog panel codon
        /// </value>
        string ID
        {
            get;
        }

        /// <value>
        /// Returns the label of the dialog panel
        /// </value>
        string Label
        {
            get;
            set;
        }

        /// <summary>
        /// The child dialog panels (e.g. for treeviews)
        /// </summary>
        //IEnumerable<IDialogPanelDescriptor> ChildDialogPanelDescriptors
        //{
        //    get;
        //}

        /// <value>
        /// Returns the dialog panel object
        /// </value>
        IDialogPanel DialogPanel
        {
            get;
            set;
        }
    }
}
