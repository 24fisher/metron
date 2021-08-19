using System;
using System.Timers;
using Metron.Core.Interfaces;

namespace MetronWPF.Models.Timers
{
    public class TimerNetFrameworkTimer : ITimer
    {
        private Timer _timer;

        

        public event EventHandler TimerTick;

        void ITimer.StartTimer()
        {
            _timer = new Timer(); 
            _timer.Elapsed += Timer_Elapsed; ;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ITimer.StopTimer()
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
