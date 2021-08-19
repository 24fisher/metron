using System;
using System.Windows.Threading;
using Metron.Core.Interfaces;

//using Windows.UI.Xaml;

namespace MetronWPF.Models.Timers
{
    public class TimerNetFrameworkDispatcherTimer : ITimer
    {
        private DispatcherTimer _timer; // clr timer
        public event EventHandler TimerTick;


        public TimerNetFrameworkDispatcherTimer()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Send); //creating timer with max priority

            _timer.Tick += new EventHandler(Metronome_Tick);
        }
        void ITimer.StartTimer()
        {
            _timer.Start();
        }

        void ITimer.StopTimer()
        {
            _timer.Stop();
        }


        TimeSpan ITimer.Interval { get { return _timer.Interval; } set { _timer.Interval = value; } }


        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {


            TimerTick?.Invoke(this, e);

        }
        #endregion
    }
}
