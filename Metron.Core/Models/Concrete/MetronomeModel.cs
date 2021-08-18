using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Metron.Core.Structs;

namespace Metron
{
    public class MetronomeModel : IMetronomeModel, INotifyPropertyChanged
    {
        public ITempoDescription _tempoDescriptionService;
        private int _tempo;


        public IMetromomeSound Beep { get; set; }
        public IColor Color { get; set; }
        public int MetronomeHighLimit { get; set; }
        public int MetronomeLowLimit { get; set; }
        public BeatPattern MetronomeBeatPattern { get; set; }
        public SpeedTrainer Trainer { get; set; }
        public ITimer Timer { get; set; }
        public IPlatformSpecificXMLDoc XmlDocImplementor { get; set; }
        private string _measure;

        public string Measure
        {

            get
            {
                if (_measure != null)
                {
                    return _measure;
                }
                else return "";
            }
            set
            {
                _measure = value;
            }
        }


        public string Pattern
        {
            get
            {
                if (MetronomeBeatPattern != null)
                {
                    return MetronomeBeatPattern?.PatternString;
                }
                else return "";
            }
            set
            {
                MetronomeBeatPattern.PatternString = value;
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(Measure));
            }
        }


        public string TempoDescription { get; set; }


        public string TickVisualization { get; set; }

        public bool IsRunning { get; internal set; }

        public bool IsSpeedTrainerActivated
        {
            get => Trainer.IsActivated;
            set => Trainer.IsActivated = value;
        }


        public int Tempo
        {
            get => _tempo;
            set
            {
                if (value >= MetronomeLowLimit && value <= MetronomeHighLimit)
                {
                    _tempo = value;
                    OnPropertyChanged(nameof(Tempo));
                    GetTempoDescription();
                    OnPropertyChanged(nameof(TempoDescription));
                }
                else if (value < MetronomeLowLimit)
                {
                    _tempo = MetronomeLowLimit;
                }
                else if (value > MetronomeHighLimit)
                {
                    _tempo = MetronomeHighLimit;
                }
            }
        }

        public void RestartTimer()
        {
            if (IsRunning) ForceRestartTimer();
        }

        public void Run()
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
                    throw new InvalidOperationException("_timer has not been started.");
                }

                IsRunning = true;
            }
        }

        public void Stop()
        {
            Timer.StopTimer();
            IsRunning = false;
        }

        private async void GetTempoDescription()
        {
            TempoDescription = await _tempoDescriptionService.GetTempoDescriptionAsync(_tempo);
        }


        public void Metronome_Tick(object sender, EventArgs e)
        {
            if (GetCurrentTackOrTick() == TickTack.MetronomeTick)
            {
                Beep.PlayHighBeep();
                TickVisualization = Color.GetColor("Red");
            }

            if (GetCurrentTackOrTick() == TickTack.MetronomeTack)
            {
                Beep.PlayLowBeep();
                TickVisualization = Color.GetColor("Green");
            }


            TickVisualization = Color.GetColor(MetronomeBeatPattern.CurrentTickIndex % 2 == 0 ? "Red" : "Green");

            OnPropertyChanged(nameof(TickVisualization));
            MetronomeBeatPattern.NextTick();
        }

        private TickTack GetCurrentTackOrTick()
        {
            return (TickTack)(int)char.GetNumericValue(MetronomeBeatPattern.CurrentTick);
        }

        public void Metronome_OnNextTakt(object sender, EventArgs e)
        {
            if (Trainer.IsActivated)
            {
                var temp = Tempo;
                var newTempo = Trainer.NextTick(Tempo);

                if (newTempo <= MetronomeHighLimit)
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


        #region PropertyChanged_BoilerPlate

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}