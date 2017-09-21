using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metron
{
    abstract class TimerAbstract
    {

        public event EventHandler Tick;

        public abstract void Start();
        public abstract void Stop();

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {

            this.Tick.Invoke(this, e);


        }
        #endregion
        

        #region Properties
        public abstract TimeSpan Interval { get; set; }
        #endregion
    }
}
