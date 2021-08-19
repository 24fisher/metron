using System.Xml.Linq;
using Metron.Core.Interfaces;
using MetronWPF.Properties;

namespace MetronWPF.Models
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
