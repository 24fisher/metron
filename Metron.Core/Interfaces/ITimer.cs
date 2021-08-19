using System;

namespace Metron.Core.Interfaces
{
    public interface ITimer
    {
        event EventHandler TimerTick;
        void StartTimer();
        void StopTimer();
        TimeSpan Interval { get; set; }

    }
}
