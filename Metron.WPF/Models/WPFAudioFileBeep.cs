using System.Media;
using Metron;
using MetronWPF.Properties;

namespace MetronWPF.Models
{
    public class WPFAudioFileBeep : IMetromomeSound
    {


        private SoundPlayer lowFreqFilePlayer;
        private SoundPlayer highFreqFilePlayer;

        public WPFAudioFileBeep()
        {
            System.IO.Stream str = Resources.sticks_high;
            highFreqFilePlayer = new SoundPlayer(str);

            System.IO.Stream str1 = Resources.sticks_low;
            lowFreqFilePlayer = new SoundPlayer(str1);
        }

        void IMetromomeSound.PlayHighFreqSound()
        {
            highFreqFilePlayer.Play();
        }
        void IMetromomeSound.PlayLowFreqSound()
        {
            lowFreqFilePlayer.Play();
        }

 
    }
}
