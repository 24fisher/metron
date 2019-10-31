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
    /// Metronome view model class. Receives timer implementor abstract object in constructor. 
    /// Provides interaction with abstract timer object. Supports WPF and Xamarin data binding.
    /// </summary>
    public class MetronomeViewModel : INotifyPropertyChanged
    {
        

        private readonly MetronomeModel metronomeModel;
        private ITempoDescription tempoDescriptionService;
        private readonly int metronomeLowLimit;
        private readonly int metronomeHighLimit;




        public MetronomeViewModel(IAppBuilder appBuilder)
        {
            metronomeLowLimit = appBuilder.metronomeLowLimit;
            metronomeHighLimit = appBuilder.metronomeHighLimit;

            metronomeModel = new MetronomeModel(
                appBuilder.TimerImplementor, 
                appBuilder.SoundImplementor, 
                appBuilder.ColorImplementor,
                appBuilder.metronomeHighLimit);
            

            metronomeModel.Timer.TimerTick += MetronomeViewModel_MetronomeTick;
            metronomeModel.OnSpeedTrainerTempoChangedEventHandler += MetronomeViewModel_SpeedTrainerTempoChanged;

            tempoDescriptionService = new TempoDescritionXMLService(appBuilder.XmlDocImplementor);
        }

        public void Run()
        {
            try
            {

                metronomeModel.StartTimer();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Timer has not been started.");

            }
        }
        public void Stop()
        {
            metronomeModel.StopTimer();
        }
        public void ChangePattern()
        {
            if (metronomeModel.IsRunning)
            {
                this.Stop();
                this.Run();
            }
        }
        public void TempoSliderMoved()
        {
            if (metronomeModel.IsRunning)
            {
                this.Stop();
                this.Run();
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
            get => metronomeModel.TempoDescription;
            set
            {
                metronomeModel.TempoDescription = value;
                OnPropertyChanged(nameof(TempoDescription));
            }
        }
        public string Measure { get => metronomeModel.Measure; }
        public int Tempo
        {
            get => metronomeModel.Tempo;
            set
            {
                if ((value >= metronomeLowLimit) && (value <= metronomeHighLimit))
                {
                    metronomeModel.Tempo = value;
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
                    metronomeModel.Tempo = metronomeLowLimit;
                }
                else if (value > metronomeHighLimit)
                {
                    metronomeModel.Tempo = metronomeHighLimit;
                }
            }
        }
        public string Pattern
        {
            get => metronomeModel.Pattern;
            set
            {
                metronomeModel.Pattern = value;
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(Measure));
            }
        }
        public string TickVisualization
        {
            get => metronomeModel.TickVisualization;
            set
            {
                metronomeModel.TickVisualization = value;
                OnPropertyChanged(nameof(TickVisualization));
            }
        }
        public bool IsRunning
        {
            get => metronomeModel.IsRunning;
            set
            {
                metronomeModel.IsRunning = value;
            }
        }
        public bool IsSpeedTrainerActivated
        {
            get => metronomeModel.IsSpeedTrainerActivated;
            set => metronomeModel.IsSpeedTrainerActivated = value;
        }

    
    }
}
