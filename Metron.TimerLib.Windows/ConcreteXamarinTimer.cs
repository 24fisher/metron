using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;




using Metron;

namespace MetronAndroid
{
    class ConcreteXamarinTimer : TimerAbstract
    {
        private Timer timer;

        

        public override event EventHandler TimerTick;

        public override void Start()
        {
            timer = new Timer(); //creating timer with max priority
            timer.Elapsed += Timer_Elapsed; ;
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
            get { return TimeSpan.FromMilliseconds(timer.Interval); }
            set { timer.Interval = (int)value.TotalMilliseconds; }
        }

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {


            TimerTick?.Invoke(this, e);

        }
        #endregion
        
    }
}
