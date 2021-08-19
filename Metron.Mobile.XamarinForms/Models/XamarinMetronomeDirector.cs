using Metron.Core.Interfaces;

namespace Metron.Mobile.XamarinForms.Models
{
    class XamarinMetronomeDirector: IMetronomeDirector
    {
        private readonly IMetronomeBuilder _builder;

        public XamarinMetronomeDirector(IMetronomeBuilder builder)
        {
            _builder = builder;
        }

        public IMetronomeModel ConstructDefaultMetronomeModel()
        {
            return _builder
                .withColor(new ColorXamarin())
                .withPlatformSpecificXMLDoc(new XamarinDocPlatformSpecificXml())
                .withSound(new XamarinAudioFileBeep())
                .withTimer(new TimerXamarin())
                .Build();
        }
    }
}
