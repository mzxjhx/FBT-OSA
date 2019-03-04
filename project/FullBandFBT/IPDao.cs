using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace CWDM1To4
{
    public class IPDao
    {
        public static List<IPCorespond> Read()
        {
            List<IPCorespond> list = new List<IPCorespond>();
            XmlReader xr = XmlReader.Create(Application.StartupPath.ToString() + "\\Config.xml");
            
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.LocalName)
                    {

                        case "Terminal":
                            IPCorespond ip = new IPCorespond();
                            for (int i = 0; i < xr.AttributeCount; i++)
                            {
                                xr.MoveToAttribute(i);
                                if (xr.Name == "server")
                                {
                                    ip.Server = xr.Value;
                                }
                                else if (xr.Name == "IP")
                                {
                                    ip.Terminal = xr.Value;
                                }
                                
                            }
                            list.Add(ip);
                            break;

                        default:
                            break;
                    }

                }
            }
            return list;
        }
    }

    public class IPCorespond
    {
        public string Server { get; set; }
        public string Terminal { get; set; }
    }
}
