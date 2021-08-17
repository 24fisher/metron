using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public interface ITimer
    {
        event EventHandler TimerTick;
        void StartTimer();
        void StopTimer();
        TimeSpan Interval { get; set; }

    }
}
