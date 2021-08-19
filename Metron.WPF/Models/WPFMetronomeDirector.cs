using Metron;
using Metron.Core.Interfaces;
using MetronWPF.Models.Timers;

namespace MetronWPF.Models
{
    public class WPFMetronomeDirector : IMetronomeDirector
    {
        private readonly IMetronomeBuilder _builder;

        public WPFMetronomeDirector(IMetronomeBuilder builder)
        {
            _builder = builder;
        }

        public IMetronomeModel ConstructDefaultMetronomeModel()
        {
            return _builder
                .withColor(new ColorWPF())
                .withPlatformSpecificXMLDoc(new WpfPlatformSpecificXmlDoc())
                .withSound(new WPFAudioFileBeep())
                .withTimer(new TimerNetFrameworkDispatcherTimer())
                .withLowLimit(10)
                .withHighLimit(300)
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
                .withTimer(new TimerNetFrameworkDispatcherTimer())
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