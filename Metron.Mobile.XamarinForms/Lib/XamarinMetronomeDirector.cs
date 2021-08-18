using System;
using System.Collections.Generic;
using System.Text;

namespace Metron.Mobile.XamarinForms.Lib
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
                .withLowLimit(10)
                .withHighLimit(300)
                .Build();
        }
    }
}
