using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Metron
{
    public class XamarinAudioFileBeep : IMetromomeSound
    {

        public XamarinAudioFileBeep()

        {
            
        }

        void IMetromomeSound.PlayHighBeep()
        {
            DependencyService.Get<IPlatformSpecificSoundPlayer>().PlayHighFreqSound();
        }

        void IMetromomeSound.PlayLowBeep()
        {
            DependencyService.Get<IPlatformSpecificSoundPlayer>().PlayLowFreqSound();
            
        }
    }
}
