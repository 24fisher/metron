using Metron.Core.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metron
{
    public class SpeedTrainer
    {
        private readonly int _bpmIncreaseStep;
        private readonly int _taktsToEncreaseTempo;

        private int _currentTick = Constants.initialTick;

        public SpeedTrainer(int bpmIncreaseStepStep, int taktsToEncreaseTempo)
        {
            _bpmIncreaseStep = bpmIncreaseStepStep;
            _taktsToEncreaseTempo = taktsToEncreaseTempo;
        }

        public int NextTick(int currentTempo)
        {
            if (_currentTick == _taktsToEncreaseTempo)
            {
                _currentTick = Constants.initialTick;
                return currentTempo += _bpmIncreaseStep;
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
    

