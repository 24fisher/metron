using Xamarin.Forms;

namespace Metron.Mobile.XamarinForms.Models
{
    public class XamarinAudioFileBeep : IMetromomeSound
    {

        void IMetromomeSound.PlayHighFreqSound()
        {
            DependencyService.Get<IMetromomeSound>().PlayHighFreqSound();
        }

        void IMetromomeSound.PlayLowFreqSound()
        {
            DependencyService.Get<IMetromomeSound>().PlayLowFreqSound();
            
        }
    }
}
