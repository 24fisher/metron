using System;
using Metron.Core.Interfaces;

namespace Metron.Mobile.XamarinForms.Models
{
    public class TimerXamarin : ITimer
    {
        
        private TimeSpan _interval;

        public delegate void MetronomeTickDelegate(object sender, EventArgs e);

        readonly MetronomeTickDelegate _tickDelegate;

        private XamarinDeviceTimerWrapper _timer;

        public event EventHandler TimerTick;

        private bool _isRunning;

        public TimerXamarin()
        {
            _tickDelegate = new MetronomeTickDelegate(Metronome_Tick);
            _isRunning = false;


        }

        void ITimer.StartTimer()
        {
            if (!_isRunning)
            {
                _timer = new XamarinDeviceTimerWrapper(Timer_Elapsed, _interval, isRecurring:true);
                _timer.Start();
                _isRunning = true;
            }

        }

        private void Timer_Elapsed()
        {
            _tickDelegate(this, new EventArgs());
            
        }

        void ITimer.StopTimer()
        {
            if (_isRunning)
            {
                _timer.Stop();
                _isRunning = false;
            }
        }

        void Restart()
        {
            if (_isRunning)
            {
                _timer.Stop();
                _isRunning = false;
            }
            _timer = new XamarinDeviceTimerWrapper(Timer_Elapsed, _interval, isRecurring: true);
            _timer.Start();
            _isRunning = true;

        }

        TimeSpan ITimer.Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                this.Restart();
            }
        }

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            TimerTick?.Invoke(this, e);
        }
        #endregion
        
    }
}
