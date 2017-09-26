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
    public class MetronomeViewModel : INotifyPropertyChanged
    {

        #region Fields
        private MetronomeModelAbstraction metronome;
        private TimerAbstract implementorTimer;


        #endregion
        #region Constructor
        public MetronomeViewModel(TimerAbstract implementorTimer)
        {
             /*ConcreteTimerCLR();*/ //Here we choose timer class to use
            metronome = new MetronomeModel(implementorTimer);
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
                //MessageBox.Show("Error!");
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
