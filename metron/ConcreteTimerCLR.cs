using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace metron
{
    class ConcreteTimerCLR : TimerAbstract
    {
        private DispatcherTimer timer; // clr timer
        public event EventHandler Tick;

        public ConcreteTimerCLR()
        {
            timer = new DispatcherTimer(DispatcherPriority.Send); //creating timer with max priority
            timer.Tick += new EventHandler(Metronome_Tick);
        }
        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }


        public override TimeSpan Interval { get { return timer.Interval; }  set { timer.Interval = value; } }
        

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            this.Tick.Invoke(this,e);


        }
        #endregion
    }
}
