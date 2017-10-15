using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace Metron
{
    class ConcreteTimerCLR : TimerAbstract
    {
        private DispatcherTimer timer; // clr timer
        public override event EventHandler TimerTick;


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


            TimerTick?.Invoke(this, e);

        }
        #endregion
    }
}
