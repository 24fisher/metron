using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Metron.Core.Interfaces;

namespace Metron
{
    public class TempoDescriptionService : ITempoDescription
    {
        public TempoDescriptionService(IPlatformSpecificXMLDoc doc)
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

        async Task<string> ITempoDescription.GetTempoDescriptionAsync(int tempo)
        {

            return await Task<string>.Factory.StartNew(GetTempoDescription, tempo).ConfigureAwait(false);
        }
    }
}

