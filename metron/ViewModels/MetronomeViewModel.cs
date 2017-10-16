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
        private MetronomeModelAbstraction _metronome;


        #endregion
        #region Constructor
        public MetronomeViewModel(TimerAbstract implementorTimer, IMetromomeBeep beepImplementor)
        {         
            _metronome = new MetronomeModel(implementorTimer, beepImplementor);
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
            get { return _metronome; }
            set
            {
                _metronome = value;
                OnPropertyChanged(nameof(Metronome));
            }
        }
        #endregion
    }
}
