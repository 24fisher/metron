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

        protected TimerAbstract TimerImplementor = null;
        protected IMetromomeBeep BeepImplementor = null;

        protected MetronomeModelAbstraction(TimerAbstract timerImplementor, IMetromomeBeep beepImplementor)
        {
            this.TimerImplementor = timerImplementor;
            this.BeepImplementor = beepImplementor;
        }

        public bool IsRunning { get; internal set; }

        public virtual void StartTimer()
        {
            if (!IsRunning)
            {
                TimerImplementor.Start();
                IsRunning = true;
            }
            
        }

        public virtual void StopTimer()
        {
            TimerImplementor.Stop();
            IsRunning = false;
        }

    }
}
