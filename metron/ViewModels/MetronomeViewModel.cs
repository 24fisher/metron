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
    /// Provides interaction with abstract timer object
    /// </summary>
    public class MetronomeViewModel : INotifyPropertyChanged
    {

        #region Fields
        private MetronomeModelAbstraction metronome;


        #endregion
        #region Constructor
        public MetronomeViewModel(TimerAbstract implementorTimer, MetromomeBeep beepImplementor)
        {
             
            metronome = new MetronomeModel(implementorTimer, beepImplementor);
            OnPropertyChanged("MetronomeViewModel");

        }


        #endregion
        #region Public members
        public void Run()
        {
            try
            {

                Metronome.StartTimer();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Timer has not been started.");

            }
        }
        public void Stop()
        {
            Metronome.StopTimer();
        }
        public void ChangePattern()
        {
            if (Metronome.IsRunning)
            {
                this.Stop();
                this.Run();
            }
        }
        public void TempoSliderMoved()
        {
            if (Metronome.IsRunning)
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
        #endregion

        #region Properties
        public MetronomeModelAbstraction Metronome
        {
            get { return metronome; }
            set
            {
                metronome = value;
                OnPropertyChanged("Metronome");
            }
        }
        #endregion
    }
}
