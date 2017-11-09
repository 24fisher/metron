using System;
using System.Collections.Generic;
using System.Text;

namespace Metron
{
    class SpeedTrainer
    {
        private readonly int _bpmIncrease;
        private readonly int _taktsToEncreaseTempo;

        private int _currentTick = 1;

        public SpeedTrainer(int bpmIncrease, int taktsToEncreaseTempo)
        {
            _bpmIncrease = bpmIncrease;
            _taktsToEncreaseTempo = taktsToEncreaseTempo;
        }

        public int NextTick(int currentTempo)
        {
            if (_currentTick == _taktsToEncreaseTempo)
            {
                _currentTick = 1;
                return currentTempo += _bpmIncrease;
            }
            else
            {
                _currentTick++;
                return currentTempo;
            }
        }

        public bool IsActivated { get; set; }

    }
}
    

