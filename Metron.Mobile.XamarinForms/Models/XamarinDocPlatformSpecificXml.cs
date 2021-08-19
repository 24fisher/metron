using System.Xml.Linq;
using Metron.Core.Interfaces;
using Xamarin.Forms;

namespace Metron.Mobile.XamarinForms.Models
{
    class XamarinDocPlatformSpecificXml : IPlatformSpecificXMLDoc
    {
        XDocument IPlatformSpecificXMLDoc.GetXMLDoc()
        {
            XDocument doc;
            doc = DependencyService.Get<IPlatformSpecificXMLDoc>().GetXMLDoc();
            return doc;
        }
    }
}
