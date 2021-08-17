using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Metron
{
    public interface ITempoDescription
    {

        string GetTempoDescription(object tempo);
        Task<string> GetTempoDescriptionAsync(int tempo);
        IPlatformSpecificXMLDoc PlatformSpecificXmlDoc { get; set; }
    }
}
