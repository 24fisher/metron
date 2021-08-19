using System.Threading.Tasks;

namespace Metron.Core.Interfaces
{
    public interface ITempoDescription
    {

        string GetTempoDescription(object tempo);
        Task<string> GetTempoDescriptionAsync(int tempo);
        IPlatformSpecificXMLDoc PlatformSpecificXmlDoc { get; set; }
    }
}
