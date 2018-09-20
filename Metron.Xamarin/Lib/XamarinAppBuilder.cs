using System;
using System.Collections.Generic;
using System.Text;


namespace Metron
{
    public class XamarinAppBuilder : IAppBuilder
    {

        public XamarinAppBuilder()
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
        public int metronomeLowLimit { get; set; }
        public int metronomeHighLimit { get; set; }
    }
}
