using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metron;
using Xamarin.Forms;



namespace MetronXamarin
{
    public partial class MainPage : ContentPage
    {
         MetronomeViewModel metronomeViewModel;
        

        public MainPage()
        {
            InitializeComponent();
            buttonMetronomeStart.Clicked += ButtonMetronomeStart_Clicked;
            sliderTempo.ValueChanged += Slider_ValueChanged;
            metronomeViewModel = new Metron.MetronomeViewModel(new TimerXamarin(), new XamarinAudioFileBeep(), new ColorXamarin(), new XamarinDocPlatformSpecificXml());
            this.BindingContext = metronomeViewModel;
        }

        private void ButtonMetronomeStart_Clicked(object sender, EventArgs e)
        {

            metronomeViewModel.Run(); 
        }
        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            ((MetronomeViewModel)BindingContext)?.TempoSliderMoved();
        }
    }
}
