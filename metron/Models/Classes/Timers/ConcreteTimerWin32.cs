using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Metron
{
    
    public class ConcreteTimerWin32 : TimerAbstract
    {
        private MMTimer timer; // clr timer
        public override event EventHandler TimerTick;


        public ConcreteTimerWin32()
        {
            timer = new MMTimer(); //creating timer with max priority
            timer.Elapsed += new EventHandler(Metronome_Tick);
        }
        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            if(timer.IsRunning)
                timer.Stop();
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(timer.Interval); }
            set { timer.Interval = (int)value.TotalMilliseconds; }
        }
        
 


        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            this.TimerTick.Invoke(this, e);


        }
        #endregion
        
    }
}

