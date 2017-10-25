using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using System.Xml.Linq;


namespace Metron
{
    class XamarinDocPlatformSpecificXml : IPlatformSpecificXMLDoc
    {
        XDocument IPlatformSpecificXMLDoc.GetXMLDoc()
        {
            XDocument doc;
            doc = DependencyService.Get<IPlatformXamarinSpecificXMLDoc>().GetXMLDoc();
            return doc;
        }
    }
}
