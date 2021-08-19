using Metron.Core.Data;

namespace Metron.Core.Models
{
    public class SpeedTrainer
    {
        private readonly int _bpmIncreaseStep;
        private readonly int _taktsToEncreaseTempo;

        private int _currentTick = Constants.InitialTick;

        public SpeedTrainer(int bpmIncreaseStepStep, int taktsToEncreaseTempo)
        {
            _bpmIncreaseStep = bpmIncreaseStepStep;
            _taktsToEncreaseTempo = taktsToEncreaseTempo;
        }

        public int NextTick(int currentTempo)
        {
            if (_currentTick == _taktsToEncreaseTempo)
            {
                _currentTick = Constants.InitialTick;
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
    

