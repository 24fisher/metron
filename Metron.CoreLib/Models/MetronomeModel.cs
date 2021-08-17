using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Metron.CoreLib.Structs;

namespace Metron
{
    public class MetronomeModel : INotifyPropertyChanged
    {
        private const int _initialTempo = Constants.initialTempo;
        private readonly IMetromomeSound _beep;
        private readonly IColor _color;
        private readonly int _metronomeHighLimit;
        private readonly int _metronomeLowLimit;

        private readonly Pattern _metronomePattern;
        private readonly SpeedTrainer _speedTrainer;


        private readonly ITempoDescription tempoDescriptionService;
        public EventHandler OnSpeedTrainerTempoChangedEventHandler;
        public int tempo;


        private string tempoDescription;

        public MetronomeModel(IAppBuilder appBuilder)
        {
            

            Timer = appBuilder?.TimerImplementor;
            _beep = appBuilder.SoundImplementor;
            _color = appBuilder.ColorImplementor;

           
            OnSpeedTrainerTempoChangedEventHandler += MetronomeViewModel_SpeedTrainerTempoChanged;

            _metronomeHighLimit = appBuilder.metronomeHighLimit;

            _metronomeLowLimit = appBuilder.metronomeLowLimit;



            tempoDescriptionService = new TempoDescriptionXMLService(appBuilder.XmlDocImplementor);

            Timer.TimerTick += MetronomeViewModel_MetronomeTick;
            Timer.TimerTick += Metronome_Tick;
            _metronomePattern = new Pattern();
            _metronomePattern.OnNextTaktHandler += Metronome_OnNextTakt;
            Tempo = _initialTempo;
            TickVisualization = _color.GetColor("White");
            _speedTrainer = new SpeedTrainer(1, 8);
        }

        public string Measure => _metronomePattern.Measure;

        public bool IsRunning { get; internal set; }

        public string TempoDescription
        {
            get => tempoDescription;
            set
            {
                tempoDescription = value;
                OnPropertyChanged(nameof(TempoDescription));
            }
        }


        public string Pattern
        {
            get => _metronomePattern.PatternString;
            set
            {
                _metronomePattern.PatternString = value;
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(Measure));
            }
        }


        public string TickVisualization
        {
            get => TickVisualization;
            set
            {
                TickVisualization = value;
                OnPropertyChanged(nameof(TickVisualization));
            }
        }

        public ITimer Timer { get; }

        public bool IsSpeedTrainerActivated
        {
            get => _speedTrainer.IsActivated;
            set => _speedTrainer.IsActivated = value;
        }


        public int Tempo
        {
            get => tempo;
            set
            {
                if (value >= _metronomeLowLimit && value <= _metronomeHighLimit)
                {
                    Tempo = value;
                    OnPropertyChanged(nameof(Tempo));


                    //Calling async method from service 
                    var task = tempoDescriptionService.GetTempoDescriptionAsync(Tempo);
                    task.ContinueWith(t =>
                    {
                        TempoDescription = t.Result;
                        OnPropertyChanged(nameof(TempoDescription));
                    });
                }
                else if (value < _metronomeLowLimit)
                {
                    tempo = _metronomeLowLimit;
                }
                else if (value > _metronomeHighLimit)
                {
                    tempo = _metronomeHighLimit;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public void ChangePattern()
        {
            if (IsRunning) RestartTimer();
        }

        public void TempoSliderMoved()
        {
            if (IsRunning) RestartTimer();
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
            return (TickTack)(int)char.GetNumericValue(_metronomePattern.CurrentTick);
        }

        private void Metronome_OnNextTakt(object sender, EventArgs e)
        {
            if (_speedTrainer.IsActivated)
            {
                var temp = Tempo;
                var newTempo = _speedTrainer.NextTick(Tempo);

                if (newTempo <= _metronomeHighLimit)
                {
                    Tempo = newTempo;

                    if (temp != Tempo)
                    {
                        // speed changed!
                        RestartTimer();

                        OnSpeedTrainerTempoChangedEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public void RestartTimer()
        {
            StopTimer();
            StartTimer();
        }

        public void StartTimer()
        {
            if (!IsRunning)
            {
                Timer.StopTimer();
                Timer.Interval = TimeSpan.FromMilliseconds(Constants.milliSecondsInOneMinute / Tempo);

                try
                {
                    Timer.StartTimer();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Timer has not been started.");
                }

                IsRunning = true;
            }
        }

        public void StopTimer()
        {
            Timer.StopTimer();
            IsRunning = false;
        }

        private void MetronomeViewModel_MetronomeTick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TickVisualization));
        }

        private void MetronomeViewModel_SpeedTrainerTempoChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Tempo));
        }
    }
}