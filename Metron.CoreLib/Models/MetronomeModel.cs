using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Metron
{
    /// <summary>
    /// Metronome model class. Receives timer and beep implementor objects in constructor. 
    /// Implements metronome-tick logic
    /// 
    /// 
    /// </summary>
    public class MetronomeModel : MetronomeModelAbstraction
    {

        #region Fields
        private readonly ITimer timer;
        private readonly IMetromomeSound beep;
        private Pattern metronomePattern;
        private int tempo;
        private string tickVisualization;
        private string tempoDescription;
        private IColor color;
        private const int initialTempo = 100;
        

        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="timerImplementor">abstract timer object</param>
        ///  <param name="beepImplementor">abstract Beep object</param>
        ///  ///  <param name="colorImplementor">abstract color object</param>
        public MetronomeModel(ITimer timerImplementor, IMetromomeSound beepImplementor, IColor colorImplementor) : base(timerImplementor, beepImplementor, colorImplementor)
        {
            timer = timerImplementor; // setting timer type from abstract class
            beep = beepImplementor; // setting beep type from abstract class
            color = colorImplementor; // setting color type from abstract class
            

            timer.TimerTick += new EventHandler(Metronome_Tick);

            metronomePattern = new Pattern();
            
            Tempo = initialTempo;
            
            TickVisualization = color.GetColor("White");




        }
        #endregion

        #region Events
        private void Metronome_Tick(object sender, EventArgs e)
        {

            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.MetronomeTick)
            {
               
                beep.PlayHighBeep();
                TickVisualization = color.GetColor("Red");
            }
            if ((TickTack)(int)Char.GetNumericValue(metronomePattern.CurrentTick) == TickTack.MetronomeTack)
            {
                beep.PlayLowBeep();
                TickVisualization = color.GetColor("Green");
            }


            if (metronomePattern.CurrentTickIndex % 2 == 0)
                TickVisualization = color.GetColor("Red");
            else TickVisualization = color.GetColor("Green");

            metronomePattern.NextTick();

        }
        #endregion

        #region Public members
        public override void StartTimer()
        {
            
            Timer.Stop();
            Timer.Interval = TimeSpan.FromMilliseconds(60000 / Tempo);
            base.StartTimer();
            IsRunning = true;
            
        }
        public override void StopTimer()
        {
        
            base.StopTimer();
            IsRunning = false;
        }
        #endregion


        #region Properties
        public override string TempoDescription
        {
            get => tempoDescription;

            set
            {
                tempoDescription = value;
            }
        }
        public override string Measure
        {
            get => metronomePattern.Measure; 
            
        } 
        public override int Tempo
        {
            get => tempo;
            set
            {
                tempo = value;

                

                //tempoDescription =  tempoDescriptionService.GetTempoDescription(tempo);
            }
        }
        public override string Pattern
        {
            get => metronomePattern.PatternString;
            set
            {
                metronomePattern.PatternString = value;

            }
            
        }
        public override string TickVisualization
        {
            get => tickVisualization;
            set
            {
                tickVisualization = value;

            }
        }
        public override ITimer Timer => timer;

        #endregion
    }
}
