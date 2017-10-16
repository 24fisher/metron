using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace Metron
{
    class ConcreteTimerClr : TimerAbstract
    {
        private DispatcherTimer _timer; // clr timer
        public override event EventHandler TimerTick;


        public ConcreteTimerClr()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Send); //creating timer with max priority
            
            _timer.Tick += new EventHandler(Metronome_Tick);
        }
        public override void Start()
        {
            _timer.Start();
        }

        public override void Stop()
        {
            _timer.Stop();
        }


        public override TimeSpan Interval { get { return _timer.Interval; }  set { _timer.Interval = value; } }
        

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {


            TimerTick?.Invoke(this, e);

        }
        #endregion
    }
}
