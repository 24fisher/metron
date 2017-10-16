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
    class MetronomeAudioFileBeep : IMetromomeBeep
    {
        //string filename = "metronome-tick.wav";
        //SoundPlayer soundPlayer; 

        public MetronomeAudioFileBeep()
        {
            try
            {
               // FileStream fileStream;
               // Stream stream;
                
                //ResourceManager resourceManager = new ResourceManager(
                
                
                //soundPlayer = new SoundPlayer(fileStream);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        

        void IMetromomeBeep.PlayHighBeep()
        {
            throw new NotImplementedException();
        }

        void IMetromomeBeep.PlayLowBeep()
        {
            throw new NotImplementedException();
        }
    }
}
