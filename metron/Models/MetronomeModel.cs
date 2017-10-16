using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Threading;
using System.Drawing;



namespace Metron
{
    /// <summary>
    /// Metronome model class. Receives timer and beep implementor objects in constructor. 
    /// Contains tick event. Supports WPF binding.
    /// </summary>
    class MetronomeModel : MetronomeModelAbstraction, INotifyPropertyChanged 
    { 

        #region Fields
        private readonly TimerAbstract _timer;
        private readonly IMetromomeBeep _beep;
        private Pattern _metronomePattern;
        private int _tempo;
        private string _tickVisualization;
        private string _tempoDescription;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="timerImplementor">abstract timer object</param>
        /// /// <param name="beepImplementor">abstract Beep object</param>
        public MetronomeModel(TimerAbstract timerImplementor, IMetromomeBeep beepImplementor) : base(timerImplementor, beepImplementor)
        {
            _timer = timerImplementor; // setting timer type from abstract class
            _beep = beepImplementor; // setting beep type from abstract class

            _timer.TimerTick += new EventHandler(Metronome_Tick);

            _metronomePattern = new Pattern();
            
            Tempo = 100;
            TickVisualization = Color.White.ToKnownColor().ToString();

            

        }
        #endregion

        #region Events
        private void Metronome_Tick(object sender, EventArgs e)
        {

            if ((TickTack)(int)Char.GetNumericValue(_metronomePattern.CurrentTick) == TickTack.MetronomeTick)
            {
               
                _beep.PlayHighBeep();
                TickVisualization = Color.Red.ToKnownColor().ToString();
            }
            if ((TickTack)(int)Char.GetNumericValue(_metronomePattern.CurrentTick) == TickTack.MetronomeTack)
            {
                _beep.PlayLowBeep();
                TickVisualization = Color.Green.ToKnownColor().ToString();
            }


            if (_metronomePattern.CurrentTickIndex % 2 == 0)
                TickVisualization = Color.Red.ToKnownColor().ToString();
            else TickVisualization = Color.Green.ToKnownColor().ToString();

            _metronomePattern.NextTick();

        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Public members
        public override void StartTimer()
        {
            
            _timer.Stop();
            _timer.Interval = TimeSpan.FromMilliseconds(60000 / Tempo);
            base.StartTimer();
            IsRunning = true;
            
        }
        public override void StopTimer()
        {
        
            base.StopTimer();
            IsRunning = false;
        }
        #endregion

        #region Private members
        private void SetTempoDescription()
        {
            TempoDescription = "";
            XDocument xdoc;
            try
            {
                xdoc = XDocument.Load("tempos_edited.xml");
            }
            catch (Exception)
            { return; }

            var items = from xe in xdoc.Element("tempos").Elements("tempo")
                        where ((Convert.ToInt32(xe.Element("lower_limit").Value) <= Tempo)  && (Convert.ToInt32(xe.Element("higher_limit").Value) >= Tempo))
                        select new TempoXml
                        {
                            Name = xe.Element("tempo_name").Value,
                            LowerLimit = Convert.ToInt32(xe.Element("lower_limit").Value),
                            HigherLimit = Convert.ToInt32(xe.Element("higher_limit").Value)
                        };

            foreach (var item in items)
            {
                if ((Tempo >= item.LowerLimit) && (Tempo <= item.HigherLimit))
                {
                    TempoDescription +=  item.Name + " | ";
                }   
            }
        }
        #endregion

        #region Properties

        public string TempoDescription
        {
            get => _tempoDescription;

            set
            {
                _tempoDescription = value;
                OnPropertyChanged(nameof(TempoDescription));

            }
        }
        public string Measure
        {
            get => _metronomePattern.Measure; 
            
        } 
        public int Tempo
        {
            get => _tempo;
            set
            {
                _tempo = value;
                this.SetTempoDescription();
                
                OnPropertyChanged(nameof(Tempo));
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
            get => _tickVisualization;
            set
            {
                _tickVisualization = value;
                OnPropertyChanged(nameof(TickVisualization));
            }
        }
        #endregion
    }
}
