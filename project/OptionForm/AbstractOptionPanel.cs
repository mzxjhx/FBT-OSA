using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OptionForm
{
    public class AbstractOptionPanel : UserControl,IDialogPanel
    {
        bool wasActivated = false;
        bool isFinished = true;
        object customizationObject;
        public Control Control
        {
            get
            {
                return this;
            }
        }

        public AbstractOptionPanel()
        {
        }

        public virtual bool ReceiveDialogMessage(DialogMessage message)
        {
            switch (message)
            {
                case DialogMessage.Activated:
                    if (!wasActivated)
                    {
                        LoadPanelContents();
                        wasActivated = true;
                    }
                    break;
                case DialogMessage.OK:
                    if (wasActivated)
                    {
                        return StorePanelContents();
                    }
                    break;
            }

            return true;
        }

        public virtual void LoadPanelContents()
        {

        }

        public virtual bool StorePanelContents()
        {
            return true;
        }

        #region IDialogPanel 成员

        public object CustomizationObject
        {
            get
            {
                return customizationObject;
            }
            set
            {
                customizationObject = value;
                OnCustomizationObjectChanged();
            }
        }

        public bool EnableFinish
        {
            get
            {
                return isFinished;
            }
            set
            {
                if (isFinished != value)
                {
                    isFinished = value;
                    OnEnableFinishChanged();
                }
            }
        }

        protected virtual void OnEnableFinishChanged()
        {
            if (EnableFinishChanged != null)
            {
                EnableFinishChanged(this, null);
            }
        }
        protected virtual void OnCustomizationObjectChanged()
        {
            if (CustomizationObjectChanged != null)
            {
                CustomizationObjectChanged(this, null);
            }
        }

        public event EventHandler EnableFinishChanged;
        public event EventHandler CustomizationObjectChanged;
        #endregion

    }
}
