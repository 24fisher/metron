using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Metron
{
    
    public class ConcreteTimerWin32 : TimerAbstract
    {
        private MmTimer _timer; // clr timer
        public override event EventHandler TimerTick;


        public ConcreteTimerWin32()
        {
            _timer = new MmTimer(); //creating timer with max priority
            _timer.Elapsed += new EventHandler(Metronome_Tick);
        }
        public override void Start()
        {
            _timer.Start();
        }

        public override void Stop()
        {
            if(_timer.IsRunning)
                _timer.Stop();
        }

        public override TimeSpan Interval
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

