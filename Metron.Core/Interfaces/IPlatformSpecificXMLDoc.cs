using System.Xml.Linq;

namespace Metron.Core.Interfaces
{
    public interface IPlatformSpecificXMLDoc
    {
        XDocument GetXMLDoc();
    }
}
