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


namespace Metron
{
    /// <summary>
    /// Metronome model class. Receives timer implementor object in constructor. 
    /// Contains tick event. Supports WPF binding.
    /// </summary>
    class MetronomeModel : MetronomeModelAbstraction, INotifyPropertyChanged 
    {

        #region Fields

        private TimerAbstract timer; // timer
        /// <summary>
        /// pattern char: '1';
        /// </summary>
        //private SoundPlayer metronomeTick;
        /// <summary>
        /// pattern char: '0';
        /// </summary>
        //private SoundPlayer metronomeTack; 
        private Pattern metronomePattern;
        private int tempo;
        private string tickVisualization;
        private string tempoDescription;
        private string measure;

        #endregion

        #region Constructor
        /// <summary>
        /// Adding click pattern 
        /// </summary>



        public MetronomeModel(TimerAbstract timerImplementor) : base(timerImplementor)
        {
            timer = timerImplementor; // setting timer type from abstract class

            timer.TimerTick += new EventHandler(Metronome_Tick);
            metronomePattern = new Pattern(0, "1000");
            this.SetMeasure();
            Tempo = 100;
            TickVisualization = "FF26D456";
            try
            {

                //metronomeTack = new SoundPlayer("metronome-tick2.wav"); //Metronome ticking s
                //metronomeTack.Load();
                //metronomeTick = new SoundPlayer("sticks.wav"); //Metronome ticking s
               //metronomeTick.Load();
            }
            catch (System.IO.FileNotFoundException )
            {
                //TODO: Imlplement universal message box
                //MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region Events
        void Metronome_Tick(object sender, EventArgs e)
        {
            //Console.Beep(5000, 70);
            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.metronomeTick)
            {
                Console.Beep(5000, 100);
                TickVisualization = "Red";
            }
            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.metronomeTack)
            {
                Console.Beep(4000, 100);
                TickVisualization = "Green";
            }


            if (metronomePattern.CurrentTickIndex % 2 == 0)
                TickVisualization = "Red";
            else TickVisualization = "Green";

            metronomePattern += 1;

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Public members

        /// <summary>
        /// Running timer; calculating bpm
        /// </summary>
        /// <param name="bpm">beats per minute input</param>
        /// <returns></returns>
        public override void StartTimer()
        {
            
            timer.Stop();
            timer.Interval = TimeSpan.FromMilliseconds(60000 / Tempo);
            base.StartTimer();
            IsRunning = true;
            
        }
        /// <summary>
        /// Timer stop
        /// </summary>
        /// <returns></returns>
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
