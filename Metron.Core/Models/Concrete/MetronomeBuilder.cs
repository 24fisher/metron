using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metron;
using Metron.Core.Structs;


namespace Metron
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
            _model.Timer = timerImplementor;
            _model.Timer.TimerTick += _model.Metronome_Tick;

            return this;
        }

        public IMetronomeBuilder withSound(IMetromomeSound soundImplementor)
        {
            _model.Beep = soundImplementor;
            return this;
        }

        public IMetronomeBuilder withColor(IColor colorImplementor)
        {
            _model.Color = colorImplementor;
            return this;
        }

        public IMetronomeBuilder withPlatformSpecificXMLDoc(IPlatformSpecificXMLDoc xmlDocImplementor)
        {
            _model.XmlDocImplementor = xmlDocImplementor;
            _model._tempoDescriptionService = new TempoDescriptionService(xmlDocImplementor);

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




        public IMetronomeBuilder withTempo(int tempo = Constants.initialTempo)
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

        public IMetronomeBuilder withSpeadTrainer(int bpmIncreaseStep = 1 , int taktsToIncreaseTempo = 8)
        {
            _model.Trainer = new SpeedTrainer(bpmIncreaseStep, taktsToIncreaseTempo);
            return this;
        }
        


        public IMetronomeModel Build()
        {
            withTempo();
            withBeatPattern();
            withTickVisualizationDefaultColor();
            withSpeadTrainer();

            return _model;
        }


    }
}
