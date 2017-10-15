using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metron
{
    /// <summary>
    /// Metronome abstract model class. Receives timer implementor abstract objects in constructor. 
    /// Provides interaction with abstract timer object
    /// </summary>
    public abstract class MetronomeModelAbstraction
    {

        protected TimerAbstract timerImplementor = null;
        protected MetromomeBeep beepImplementor = null;

        public MetronomeModelAbstraction(TimerAbstract timerImplementor, MetromomeBeep beepImplementor)
        {
            this.timerImplementor = timerImplementor;
            this.beepImplementor = beepImplementor;
        }

        public bool IsRunning { get; internal set; }

        public virtual void StartTimer()
        {
            timerImplementor.Start();
        }

        public virtual void StopTimer()
        {
            timerImplementor.Stop();
        }

    }
}
