using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using Metron.Core.Structs;

namespace Metron
{
    public class MetronomeModel : IMetronomeModel, INotifyPropertyChanged
    {
        private readonly int _metronomeHighLimit;
        private readonly int _metronomeLowLimit;
        private int _tempo;
        private readonly IMetromomeSound _beep;
        private readonly IColor _color;
        private readonly ITempoDescription _tempoDescriptionService;
        private readonly Pattern _metronomePattern;
        private readonly SpeedTrainer _speedTrainer;
        private readonly ITimer _timer;
        private string _tickVisualization;


        public MetronomeModel(IAppBuilder appBuilder)
        {
            _timer = appBuilder?.TimerImplementor;
            _beep = appBuilder?.SoundImplementor;
            _color = appBuilder?.ColorImplementor;
            _metronomeHighLimit = appBuilder.metronomeHighLimit;
            _metronomeLowLimit = appBuilder.metronomeLowLimit;
            _tempoDescriptionService = new TempoDescriptionService(appBuilder.XmlDocImplementor);


            _timer.TimerTick += Metronome_Tick;
            _metronomePattern = new Pattern();
            _metronomePattern.OnNextTaktHandler += Metronome_OnNextTakt;
            Tempo = Constants.initialTempo;
            _tickVisualization = _color?.GetColor("White");
            _speedTrainer = new SpeedTrainer(1, 8);
        }

        public string Measure => _metronomePattern.Measure;

        public bool IsRunning { get; internal set; }


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

        public bool IsSpeedTrainerActivated
        {
            get => _speedTrainer.IsActivated;
            set => _speedTrainer.IsActivated = value;
        }

        public string TempoDescription
        {
            get;
            set;
        }
      

        public string TickVisualization
        {
            get { return _tickVisualization; }
            set { _tickVisualization = value; }
        }



        public int Tempo
        {
            get => _tempo;
            set
            {
                if (value >= _metronomeLowLimit && value <= _metronomeHighLimit)
                {
                    _tempo = value;
                    OnPropertyChanged(nameof(Tempo));
                    GetTempoDescription();
                    OnPropertyChanged(nameof(TempoDescription));

                }
                else if (value < _metronomeLowLimit)
                {
                    _tempo = _metronomeLowLimit;
                }
                else if (value > _metronomeHighLimit)
                {
                    _tempo = _metronomeHighLimit;
                }
            }
        }

        private async void GetTempoDescription()
        {
            TempoDescription = await _tempoDescriptionService.GetTempoDescriptionAsync(_tempo);
        }

        public void RestartTimer()
        {
            if (IsRunning) ForceRestartTimer();
        }


        private void Metronome_Tick(object sender, EventArgs e)
        {
            if (GetCurrentTackOrTick() == TickTack.MetronomeTick)
            {
                _beep.PlayHighBeep();
                _tickVisualization = _color.GetColor("Red");
            }

            if (GetCurrentTackOrTick() == TickTack.MetronomeTack)
            {
                _beep.PlayLowBeep();
                _tickVisualization = _color.GetColor("Green");
            }


            _tickVisualization = _color.GetColor(_metronomePattern.CurrentTickIndex % 2 == 0 ? "Red" : "Green");

            OnPropertyChanged(nameof(TickVisualization));
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
                        ForceRestartTimer();

                        OnPropertyChanged(nameof(Tempo));
                    }
                }
            }
        }

        private void ForceRestartTimer()
        {
            Stop();
            Run();
        }

        public void Run()
        {
            if (!IsRunning)
            {
                _timer.StopTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(Constants.milliSecondsInOneMinute / Tempo);

                try
                {
                    _timer.StartTimer();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("_timer has not been started.");
                }

                IsRunning = true;
            }
        }

        public void Stop()
        {
            _timer.StopTimer();
            IsRunning = false;
        }


        #region PropertyChanged_BoilerPlate


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}