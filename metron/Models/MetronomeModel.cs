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
        private TimerAbstract timer;
        private MetromomeBeep beep;
        private Pattern metronomePattern;
        private int tempo;
        private string tickVisualization;
        private string tempoDescription;
        private string measure;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="timerImplementor">abstract timer object</param>
        public MetronomeModel(TimerAbstract timerImplementor, MetromomeBeep beepImplementor) : base(timerImplementor, beepImplementor)
        {
            timer = timerImplementor; // setting timer type from abstract class
            beep = beepImplementor; // setting beep type from abstract class

            timer.TimerTick += new EventHandler(Metronome_Tick);

            metronomePattern = new Pattern();
            this.SetMeasure();
            Tempo = 100;
            TickVisualization = Color.White.ToKnownColor().ToString();

            

        }
        #endregion

        #region Events
        private void Metronome_Tick(object sender, EventArgs e)
        {

            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.metronomeTick)
            {
               
                beep.DoHighBeep();
                TickVisualization = Color.Red.ToKnownColor().ToString();
            }
            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.metronomeTack)
            {
                beep.DoLowBeep();
                TickVisualization = Color.Green.ToKnownColor().ToString();
            }


            if (metronomePattern.CurrentTickIndex % 2 == 0)
                TickVisualization = Color.Red.ToKnownColor().ToString();
            else TickVisualization = Color.Green.ToKnownColor().ToString();

            metronomePattern += 1;

        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Public members
        public override void StartTimer()
        {
            
            timer.Stop();
            timer.Interval = TimeSpan.FromMilliseconds(60000 / Tempo);
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
                        select new TempoXML
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
        private void SetMeasure()
        {
            Measure = Pattern.Length.ToString() + " / " + "4";
        }
        #endregion

        #region Properties
        public string MetronomePattern
        {
            get => metronomePattern.PatternString;

            set
            {
                metronomePattern.PatternString = value;
                this.StopTimer();
                this.StartTimer();
            }
        }
        public string TempoDescription
        {
            get => tempoDescription;

            set
            {
                tempoDescription = value;
                OnPropertyChanged(nameof(TempoDescription));

            }
        }
        public string Measure
        {
            get => measure;

            set
            {
                measure = value;
                OnPropertyChanged(nameof(Measure));

            }
        }
        public int Tempo
        {
            get => tempo;
            set
            {
                tempo = value;
                this.SetTempoDescription();
                
                OnPropertyChanged(nameof(Tempo));
            }
        }
        public string Pattern
        {
            get => metronomePattern.PatternString;
            set
            {
                metronomePattern.PatternString = value;
                this.SetMeasure();
                OnPropertyChanged(nameof(Pattern));
            }
            
        }
        public string TickVisualization
        {
            get => tickVisualization;
            set
            {
                tickVisualization = value;
                OnPropertyChanged(nameof(TickVisualization));
            }
        }
        #endregion
    }
}
