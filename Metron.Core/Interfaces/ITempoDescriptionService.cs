using System.Threading.Tasks;

namespace Metron.Core.Interfaces
{
    public interface ITempoDescriptionService
    {

        string GetTempoDescription(object tempo);
        Task<string> GetTempoDescriptionAsync(int tempo);
        IPlatformSpecificXMLDoc PlatformSpecificXmlDoc { get; set; }
    }
}
