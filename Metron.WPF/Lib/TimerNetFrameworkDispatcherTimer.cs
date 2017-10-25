using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace Metron
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
        void ITimer.Start()
        {
            _timer.Start();
        }

        void ITimer.Stop()
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
