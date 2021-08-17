﻿using System.ComponentModel;

namespace Metron
{
    public interface IMetronomeModel
    {
        bool IsRunning { get; }
        bool IsSpeedTrainerActivated { get; set; }
        int Tempo { get; set; }
        void RestartTimer();
        void Run();
        void Stop();

    }
}