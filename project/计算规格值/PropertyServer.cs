using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSpec
{
    public class PropertyServer
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public static event EventHandler<SignleEventArgs> SignlePropertyChange;
        public static event EventHandler<DwdmEventArgs> DWDMPropertyChange;
        public static event EventHandler<FwdmEventArgs> FWDMPropertyChange;
        public static event EventHandler<DWDM_R_Args> DWDM_R_Change;
        
        public void OnSignleChanged(SignleEventArgs e)
        {
            if (SignlePropertyChange != null)
                SignlePropertyChange(null, e);
        }

        public void OnDWDMChanged(DwdmEventArgs e)
        {
            if (DWDMPropertyChange != null)
                DWDMPropertyChange(null, e);
        }

        public void OnFWDMChanged(FwdmEventArgs e)
        {
            if (FWDMPropertyChange != null)
                FWDMPropertyChange(null, e);
        }

        public void OnDWDM_R_Changed(DWDM_R_Args e)
        {
            if (DWDM_R_Change != null)
                DWDM_R_Change(null, e);
        }
    }
}
