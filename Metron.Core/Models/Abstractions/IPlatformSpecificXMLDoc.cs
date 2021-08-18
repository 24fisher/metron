using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Metron
{
    public interface IPlatformSpecificXMLDoc
    {
        XDocument GetXMLDoc();
    }
}
