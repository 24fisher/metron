using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Metron;
using Android.Media;
using MetronXamarin.Droid;


[assembly: Xamarin.Forms.Dependency(typeof(AndroidResourcesAudioReader))]

namespace MetronXamarin.Droid
{
    class AndroidResourcesAudioReader: IPlatformSpecificSoundPlayer
    {
        MediaPlayer highFreqFilePlayer;
        MediaPlayer lowFreqFilePlayer;

        public AndroidResourcesAudioReader()
        {

            lowFreqFilePlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.sticks_low);
            highFreqFilePlayer = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.sticks_high);
        }

        void IPlatformSpecificSoundPlayer.PlayHighFreqSound()
        {
            highFreqFilePlayer.Start();
        }


        void IPlatformSpecificSoundPlayer.PlayLowFreqSound()
        {
            lowFreqFilePlayer.Start();

        }
    }
}