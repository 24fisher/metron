using System;
using System.Collections.Generic;
using System.Text;

namespace Metron
{
    public interface IAppBuilder
    {


        ITimer TimerImplementor { get; set; }
        IMetromomeSound SoundImplementor { get; set; }
        IColor ColorImplementor { get; set; }
        IPlatformSpecificXMLDoc XmlDocImplementor { get; set; }
        int metronomeLowLimit { get; set; }
        int metronomeHighLimit { get; set; }


    }
}
