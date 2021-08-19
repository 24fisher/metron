using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Metron.Core.Interfaces;

namespace Metron.Core.Services
{
    public class TempoDescriptionServiceService : ITempoDescriptionService
    {
        public TempoDescriptionServiceService(IPlatformSpecificXMLDoc doc)
        {
            PlatformSpecificXmlDoc = doc;
        }

         
        public IPlatformSpecificXMLDoc PlatformSpecificXmlDoc { get ; set; }

        public string GetTempoDescription(object tempo)
        {
            string tempoDescription = string.Empty;
            XDocument xdoc;
            try
            {
                xdoc = PlatformSpecificXmlDoc.GetXMLDoc();
            }
            catch (Exception)
            { return "error on opening xml stream"; }

            var items = from xe in xdoc.Element("tempos").Elements("tempo")
                where (Convert.ToInt32(xe.Element("lower_limit").Value) <= (int)tempo) && (Convert.ToInt32(xe.Element("higher_limit").Value) >= (int)tempo)
                select new TempoXml
                {
                    Name = xe.Element("tempo_name").Value,
                    LowerLimit = Convert.ToInt32(xe.Element("lower_limit").Value),
                    HigherLimit = Convert.ToInt32(xe.Element("higher_limit").Value)
                };

            foreach (var item in items)
            {
                if (((int)tempo >= item.LowerLimit) && ((int)tempo <= item.HigherLimit))
                {
                    tempoDescription += item.Name + " | ";
                }
            }
            return tempoDescription;
        }

        async Task<string> ITempoDescriptionService.GetTempoDescriptionAsync(int tempo)
        {

            return await Task.Run(() => GetTempoDescription(tempo)).ConfigureAwait(false);
        }
    }
}

