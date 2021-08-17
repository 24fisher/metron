using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Metron
{
    public interface IPlatformSpecificSoundPlayer
    {

        void PlayHighFreqSound();
        void PlayLowFreqSound();

    }
}