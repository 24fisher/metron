using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metron
{
    class ConcreteTimerWin32 : TimerAbstract
    {
        private MMTimer timer; // clr timer
        public override event EventHandler TimerTick;


        /*public ConcreteTimerWin32()
        {
            timer = new DispatcherTimer(DispatcherPriority.Send); //creating timer with max priority
            timer.Tick += new EventHandler(Metronome_Tick);
        }*/
        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }

        public override TimeSpan Interval { get { return TimeSpan.Parse(timer.Interval.ToString()); } set { timer.Interval = Convert.ToInt32(value.ToString()); } }
        /*
        public TimeSpan Interval { get { return timer.Interval; } internal set { timer.Interval = value; } }


        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            this.Tick.Invoke(this, e);


        }
        #endregion
        */
    }
}

