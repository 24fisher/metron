using System;
using Metron.Core.Data;
using Metron.Core.Interfaces;

namespace Metron.Core.Models
{
    public class MetronomeBuilder: IMetronomeBuilder
    {

        private MetronomeModel _model;

        public MetronomeBuilder(MetronomeModel model)
        {
            _model = model;
        }
        public IMetronomeBuilder withTimer(ITimer timerImplementor)
        {
            _model.Timer = timerImplementor ?? throw new ArgumentNullException(nameof(timerImplementor));
            _model.Timer.TimerTick += _model.Metronome_Tick;

            return this;
        }

        public IMetronomeBuilder withSound(IMetromomeSound soundImplementor)
        {
            _model.Beep = soundImplementor ?? throw new ArgumentNullException(nameof(soundImplementor));
            return this;
        }

        public IMetronomeBuilder withColor(IColor colorImplementor)
        {
            _model.Color = colorImplementor ?? throw new ArgumentNullException(nameof(colorImplementor));
            return this;
        }

        public IMetronomeBuilder withPlatformSpecificXMLDoc(IPlatformSpecificXMLDoc xmlDocImplementor)
        {
            _model.XmlDocImplementor = xmlDocImplementor ?? throw new ArgumentNullException(nameof(xmlDocImplementor));
            _model.TempoDescriptionService = new TempoDescriptionService(xmlDocImplementor);

            return this;
        }


        public IMetronomeBuilder withHighLimit(int metronomeHighLimit)
        {
            _model.MetronomeHighLimit = metronomeHighLimit;
            return this;
        }

        public IMetronomeBuilder withLowLimit(int metronomeLowLimit)
        {
            _model.MetronomeHighLimit = metronomeLowLimit;
            return this;
        }




        public IMetronomeBuilder withTempo(int tempo = Constants.InitialTempo)
        {
            _model.Tempo = tempo;
            

            return this;
        }


        public IMetronomeBuilder withBeatPattern(string patternString = "1000")
        {
            _model.MetronomeBeatPattern = new BeatPattern(0, patternString);
            _model.MetronomeBeatPattern.OnNextTaktHandler += _model.Metronome_OnNextTakt;
            return this;
        }

        

        public IMetronomeBuilder withTickVisualizationDefaultColor(string color = "White")
        {
            _model.TickVisualization = _model.Color.GetColor(color);
            return this;
        }

        public IMetronomeBuilder withSpeedTrainer(int bpmIncreaseStep = 1 , int taktsToIncreaseTempo = 8)
        {
            _model.Trainer = new SpeedTrainer(bpmIncreaseStep, taktsToIncreaseTempo);
            return this;
        }
        


        public IMetronomeModel Build()
        {
            if (_model.Tempo == 0)
            {
                withTempo();
            }

            if (string.IsNullOrEmpty(_model.Pattern))
            {
                withBeatPattern();
            }

            if (_model.TickVisualization == null)
            {
                withTickVisualizationDefaultColor();
            }

            if (_model.Trainer == null)
            {
                withSpeedTrainer();
            }

            return _model;
        }


    }
}
