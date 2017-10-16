using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;




using Metron;

namespace Metron
{
    class ConcreteXamarinTimer : TimerAbstract
    {
        private Timer _timer;

        

        public override event EventHandler TimerTick;

        public override void Start()
        {
            _timer = new Timer(); //creating timer with max priority
            _timer.Elapsed += Timer_Elapsed; ;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(_timer.Interval); }
            set { _timer.Interval = (int)value.TotalMilliseconds; }
        }

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {


            TimerTick?.Invoke(this, e);

        }
        #endregion
        
    }
}
