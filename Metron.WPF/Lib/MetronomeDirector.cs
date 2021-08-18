using System;
using System.Collections.Generic;
using System.Text;
using Metron;
using Metron.Core;

namespace Metron
{
    public class WPFMetronomeDirector : IMetronomeDirector
    {
        private readonly IMetronomeBuilder _builder;

        public WPFMetronomeDirector()
        {
            _builder = new MetronomeBuilder(new MetronomeModel());


        }
        public IMetronomeModel ConstructDefaultMetronomeModel()
        {
            return _builder
                .withColor(new ColorWPF())
                .withLowLimit(10)
                .withHighLimit(300)
                .withPlatformSpecificXMLDoc(new WpfPlatformSpecificXmlDoc())
                .withSound(new WPFAudioFileBeep())
                .withTimer(new TimerNetFrameworkDispatcherTimer())
                .Build();


        }

    public IMetronomeModel ConstructWFPMetronomeModelWithExtendedLimits()
    {
        return _builder
            .withColor(new ColorWPF())
            .withLowLimit(-100)
            .withHighLimit(700)
            .withPlatformSpecificXMLDoc(new WpfPlatformSpecificXmlDoc())
            .withSound(new WPFAudioFileBeep())
            .withTimer(new TimerNetFrameworkTimer())
            .Build();

    }

    public IMetronomeModel ConstructWFPMetronomeModelWithWinApiTimer()
    {
        return _builder
            .withColor(new ColorWPF())
            .withLowLimit(10)
            .withHighLimit(300)
            .withPlatformSpecificXMLDoc(new WpfPlatformSpecificXmlDoc())
            .withSound(new WPFAudioFileBeep())
            .withTimer(new TimerWin32Adapted())
            .Build();
    }

   

    }
}
