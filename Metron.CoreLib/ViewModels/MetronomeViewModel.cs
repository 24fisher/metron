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
        
        #region Fields
        private MetronomeModelAbstraction metronomeModel;
        private ITempoDescription tempoDescriptionService;
    


        #endregion
        #region Constructor
        public MetronomeViewModel(ITimer implementorTimer, IMetromomeSound beepImplementor, IColor colorImplementor, IPlatformSpecificXMLDoc doc)
        {         
            metronomeModel = new MetronomeModel(implementorTimer, beepImplementor, colorImplementor);
            metronomeModel.Timer.TimerTick += MetronomeViewModel_MetronomeTick;

            tempoDescriptionService = new TempoDescritionXMLService(doc);
        }

        

        #endregion
        #region Public members
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

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void MetronomeViewModel_MetronomeTick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TickVisualization));
        }

        #endregion

        #region Properties
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
                metronomeModel.Tempo = value;
                OnPropertyChanged(nameof(Tempo));
                

                //Calling async method from servise 
                Task<string> task = tempoDescriptionService.GetTempoDescriptionAsync(Tempo);
                task.ContinueWith(t =>
                {
                    TempoDescription = t.Result;
                    OnPropertyChanged(nameof(TempoDescription));
                    
                });
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
        #endregion
    }
}
