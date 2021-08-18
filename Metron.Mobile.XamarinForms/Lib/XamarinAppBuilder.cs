using System;
using System.Collections.Generic;
using System.Text;


namespace Metron
{
    public class XamarinMetronomeBuilder : IMetronomeBuilder
    {

        public XamarinMetronomeBuilder()
        {
            TimerImplementor = new TimerXamarin();
            SoundImplementor = new XamarinAudioFileBeep();
            ColorImplementor = new ColorXamarin();
            XmlDocImplementor = new XamarinDocPlatformSpecificXml();
        }

        public ITimer TimerImplementor { get; set; }
        public IMetromomeSound SoundImplementor { get; set; }
        public IColor ColorImplementor { get; set; }
        public IPlatformSpecificXMLDoc XmlDocImplementor { get; set; }
        public int MetronomeLowLimit { get; set; }
        public int MetronomeHighLimit { get; set; }

        public IMetronomeBuilder Create()
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withColor(IColor colorImplementor)
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withHighLimit(int metronomeHighLimit)
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withLowLimit(int metronomeLowLimit)
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withPlatformSpecificXMLDoc(IPlatformSpecificXMLDoc xmlDocImplementor)
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withSound(IMetromomeSound soundImplementor)
        {
            throw new NotImplementedException();
        }

        public IMetronomeBuilder withTimer(ITimer timerImplementor)
        {
            throw new NotImplementedException();
        }
    }
}
