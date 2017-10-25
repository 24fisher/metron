using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using MetronWPF.Properties;
using System.Xml.Linq;

namespace Metron
{
    public class WpfPlatformSpecificXmlDoc : IPlatformSpecificXMLDoc
    {
        XDocument IPlatformSpecificXMLDoc.GetXMLDoc()
        {
            var xdoc = XDocument.Parse(Resources.tempos_edited);

            return xdoc;

        }
    }
}
