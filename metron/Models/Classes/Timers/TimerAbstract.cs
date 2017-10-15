using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public abstract class TimerAbstract
    {


        public abstract event EventHandler TimerTick;
        public abstract void Start();
        public abstract void Stop();
        

        #region Properties
        public abstract TimeSpan Interval { get; set; }
        #endregion
    }
}
