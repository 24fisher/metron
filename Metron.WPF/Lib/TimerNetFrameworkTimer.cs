using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;




using Metron;

namespace Metron
{
    public class TimerNetFrameworkTimer : ITimer
    {
        private Timer _timer;

        

        public event EventHandler TimerTick;

        void ITimer.Start()
        {
            _timer = new Timer(); 
            _timer.Elapsed += Timer_Elapsed; ;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ITimer.Stop()
        {
            throw new NotImplementedException();
        }

        TimeSpan ITimer.Interval
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
