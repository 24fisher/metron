using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Metron
{

    public class MetronomeModel
    {
        private readonly IMetromomeSound _beep;
        private readonly Pattern _metronomePattern;
        private readonly IColor _color;
        private const int _initialTempo = 100;
        private readonly SpeedTrainer _speedTrainer;
        private readonly int _metronomeHighLimit;
        public EventHandler OnSpeedTrainerTempoChangedEventHandler;
        public bool IsRunning { get; internal set; }
        public string TempoDescription { get; set; }
        public string Measure
        {
            get => _metronomePattern.Measure;

        }
        public int Tempo { get; set; }
        public string Pattern
        {
            get => _metronomePattern.PatternString;
            set
            {
                _metronomePattern.PatternString = value;

            }

        }
        public string TickVisualization { get; set; }
        public ITimer Timer { get; }
        public bool IsSpeedTrainerActivated
        {
            get => _speedTrainer.IsActivated;
            set => _speedTrainer.IsActivated = value;
        }


        public MetronomeModel(ITimer timerImplementor, IMetromomeSound beepImplementor, IColor colorImplementor, int metronomeHighLimit)
        {
            Timer = timerImplementor;
            _beep = beepImplementor;
            _color = colorImplementor;

         
            _metronomeHighLimit = metronomeHighLimit;


            Timer.TimerTick += new EventHandler(Metronome_Tick);
            _metronomePattern = new Pattern();
            _metronomePattern.OnNextTaktHandler += new EventHandler(Metronome_OnNextTakt);
            Tempo = _initialTempo;
            TickVisualization = _color.GetColor("White");
            _speedTrainer = new SpeedTrainer(bpmIncrease: 1, taktsToEncreaseTempo: 8);

        }

        private void Metronome_Tick(object sender, EventArgs e)
        {

            if (GetCurrentTackOrTick() == TickTack.MetronomeTick)
            {

                _beep.PlayHighBeep();
                TickVisualization = _color.GetColor("Red");
            }
            if (GetCurrentTackOrTick() == TickTack.MetronomeTack)
            {
                _beep.PlayLowBeep();
                TickVisualization = _color.GetColor("Green");
            }


            TickVisualization = _color.GetColor(_metronomePattern.CurrentTickIndex % 2 == 0 ? "Red" : "Green");

            _metronomePattern.NextTick();

        }
        private TickTack GetCurrentTackOrTick()
        {
            return (TickTack)(int)Char.GetNumericValue(_metronomePattern.CurrentTick);
        }

        private void Metronome_OnNextTakt(object sender, EventArgs e)
        {
            if (_speedTrainer.IsActivated)
            {

                int temp = Tempo;
                int newTempo = _speedTrainer.NextTick(Tempo);

                if (newTempo <= _metronomeHighLimit)
                {
                    Tempo = newTempo;

                    if (temp != Tempo)
                    {
                        // speed up!
                        StopTimer();
                        StartTimer();

                        OnSpeedTrainerTempoChangedEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public void StartTimer()
        {

            if (!IsRunning)
            {
                Timer.StopTimer();
                Timer.Interval = TimeSpan.FromMilliseconds(60000 / Tempo);

                Timer.StartTimer();
                IsRunning = true;
            }

        }
        public void StopTimer()
        {

            Timer.StopTimer();
            IsRunning = false;

        }

    }
}
