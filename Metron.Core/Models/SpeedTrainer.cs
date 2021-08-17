using Metron.Core.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metron
{
    class SpeedTrainer
    {
        private readonly int _bpmIncrease;
        private readonly int _taktsToEncreaseTempo;

        private int _currentTick = Constants.initialTick;

        public SpeedTrainer(int bpmIncrease, int taktsToEncreaseTempo)
        {
            _bpmIncrease = bpmIncrease;
            _taktsToEncreaseTempo = taktsToEncreaseTempo;
        }

        public int NextTick(int currentTempo)
        {
            if (_currentTick == _taktsToEncreaseTempo)
            {
                _currentTick = Constants.initialTick;
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
    

