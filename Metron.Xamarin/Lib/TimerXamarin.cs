using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Metron;
using Xamarin.Forms;

namespace Metron
{
    public class TimerXamarin : ITimer
    {
        
        private TimeSpan interval;

        public delegate void MetronomeTickDelegate(object sender, EventArgs e);
        MetronomeTickDelegate tickDelegate;

        private XamarinDeviceTimerWrapper timer;

        public event EventHandler TimerTick;
        private bool isRuning;

        public TimerXamarin()
        {
            tickDelegate = new MetronomeTickDelegate(Metronome_Tick);
            isRuning = false;


        }

        void ITimer.StartTimer()
        {
            if (!isRuning)
            {
                timer = new XamarinDeviceTimerWrapper(Timer_Elapsed, interval, isRecurring:true);
                timer.Start();
                isRuning = true;
            }

            //Device.StartTimer(interval, Timer_Elapsed);
        }

        private void Timer_Elapsed()
        {
            tickDelegate(this, new EventArgs());
            
        }

        void ITimer.StopTimer()
        {
            if (isRuning)
            {
                timer.Stop();
                isRuning = false;
            }
        }

        void Restart()
        {
            if (isRuning)
            {
                timer.Stop();
                isRuning = false;
            }
            timer = new XamarinDeviceTimerWrapper(Timer_Elapsed, interval, isRecurring: true);
            timer.Start();
            isRuning = true;

        }

        TimeSpan ITimer.Interval
        {
            get { return interval; }
            set
            {
                interval = value;
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
