using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metron
{
    /// <summary>
    /// Metronome abstract model class. Receives timer implementor abstract object in constructor. 
    /// Provides interaction with abstract timer object
    /// </summary>
    public abstract class MetronomeModelAbstraction
    {

        protected TimerAbstract timerImplementor = null;

        public MetronomeModelAbstraction(TimerAbstract timerImplementor)
        {
            this.timerImplementor = timerImplementor;
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
