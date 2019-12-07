using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Metron
{
    /// <summary>
    /// Metronome view model class.
    /// Adds WPF and Xamarin data binding support to the model; Adapts metronome model interface.
    /// </summary>
    public class MetronomeViewModel : MetronomeModel, INotifyPropertyChanged
    {
        

       // private readonly MetronomeModel metronomeModel;
        private ITempoDescription tempoDescriptionService;
        private readonly int metronomeLowLimit;
        private readonly int metronomeHighLimit;


        public MetronomeViewModel(IAppBuilder appBuilder): base(appBuilder)
        {
         

            metronomeLowLimit = appBuilder.metronomeLowLimit;
            metronomeHighLimit = appBuilder.metronomeHighLimit;

            base.Timer.TimerTick += MetronomeViewModel_MetronomeTick;
            base.OnSpeedTrainerTempoChangedEventHandler += MetronomeViewModel_SpeedTrainerTempoChanged;

            tempoDescriptionService = new TempoDescritionXMLService(appBuilder.XmlDocImplementor);
        }

        public void Run()
        {
            base.StartTimer();

        }
        public void Stop()
        {
            base.StopTimer();
        }
        public void ChangePattern()
        {
            if (base.IsRunning)
            {
                base.RestartTimer();
            }
        }
        public void TempoSliderMoved()
        {
            if (base.IsRunning)
            {
                base.RestartTimer();
            }
        }

   
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void MetronomeViewModel_MetronomeTick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TickVisualization));
        }
        private void MetronomeViewModel_SpeedTrainerTempoChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Tempo));
        }

 
        public string TempoDescription
        {
            get => base.TempoDescription;
            set
            {
                base.TempoDescription = value;
                OnPropertyChanged(nameof(TempoDescription));
            }
        }
        public int Tempo
        {
            get => base.Tempo;
            set
            {
                if ((value >= metronomeLowLimit) && (value <= metronomeHighLimit))
                {
                    base.Tempo = value;
                    OnPropertyChanged(nameof(Tempo));


                    //Calling async method from service 
                    Task<string> task = tempoDescriptionService.GetTempoDescriptionAsync(Tempo);
                    task.ContinueWith(t =>
                    {
                        TempoDescription = t.Result;
                        OnPropertyChanged(nameof(TempoDescription));

                    });
                }
                else if(value < metronomeLowLimit)
                {
                    base.Tempo = metronomeLowLimit;
                }
                else if (value > metronomeHighLimit)
                {
                    base.Tempo = metronomeHighLimit;
                }
            }
        }
        public string Pattern
        {
            get => base.Pattern;
            set
            {
                base.Pattern = value;
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(Measure));
            }
        }
        public string TickVisualization
        {
            get => base.TickVisualization;
            set
            {
                base.TickVisualization = value;
                OnPropertyChanged(nameof(TickVisualization));
            }
        }

    
    }
}
