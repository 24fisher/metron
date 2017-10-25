using MetronWPF.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Metron
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

        void IMetromomeSound.PlayHighBeep()
        {
            highFreqFilePlayer.Play();
        }
        void IMetromomeSound.PlayLowBeep()
        {
            lowFreqFilePlayer.Play();
        }

 
    }
}
