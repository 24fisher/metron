using System;
using Metron;
using Metron.Core.Interfaces;

namespace MetronWPF.Models.Timers
{
    
    public class TimerWin32Adapted : ITimer
    {
        private TimerWin32Wrapper _timer; 
        public event EventHandler TimerTick;


        public TimerWin32Adapted()
        {
            _timer = new TimerWin32Wrapper(); 
            _timer.Elapsed += new EventHandler(Metronome_Tick);
        }
        void ITimer.StartTimer()
        {
            _timer.Start();
        }

        void ITimer.StopTimer()
        {
            if(_timer.IsRunning)
                _timer.Stop();
        }

        TimeSpan ITimer.Interval
        {
            get { return TimeSpan.FromMilliseconds(_timer.Interval); }
            set { _timer.Interval = (int)value.TotalMilliseconds; }
        }
        
 


        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            this.TimerTick.Invoke(this, e);


        }
        #endregion
        
    }
}

