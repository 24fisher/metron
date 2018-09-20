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

        protected ITimer TimerImplementor = null;
        protected IMetromomeSound BeepImplementor = null;
        protected IColor colorImplementor = null;
        public EventHandler OnSpeedTrainerTempoChangedEventHandler;

        protected MetronomeModelAbstraction(ITimer timerImplementor, IMetromomeSound beepImplementor, IColor colorImplementor)
        {
            TimerImplementor = timerImplementor;
            BeepImplementor = beepImplementor;
            colorImplementor = colorImplementor;
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



        public abstract string TempoDescription { get; set; }
        public abstract string Measure { get; }
        public abstract int Tempo { get; set; }
        public abstract string Pattern { get; set; }
        public abstract string TickVisualization { get; set; }
        public abstract ITimer Timer { get; }
        public abstract bool IsSpeedTrainerActivated { get; set; }


    }
}
