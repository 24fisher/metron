using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace metron
{
    class MetronomeModelAbstraction
    {

        protected TimerAbstract timerImplementor = null;

        public MetronomeModelAbstraction(TimerAbstract timerImplementor)
        {
            this.timerImplementor = timerImplementor;
        }

        public bool IsRunning { get; internal set; }

        public void StartTimer()
        {
            timerImplementor.Start();
        }
        public void StopTimer()
        {
            timerImplementor.Stop();
        }
    }
}
