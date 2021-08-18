using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metron;
using Metron.Mobile.XamarinForms.Lib;
using Xamarin.Forms;



namespace MetronXamarin
{
    public partial class MainPage : ContentPage
    {
         IMetronomeModel metronomeModel;
        

        public MainPage()
        {
            InitializeComponent();
            buttonMetronomeStart.Clicked += ButtonMetronomeStart_Clicked;
            sliderTempo.ValueChanged += Slider_ValueChanged;

            IMetronomeBuilder builder = new MetronomeBuilder(new MetronomeModel());
            XamarinMetronomeDirector director = new XamarinMetronomeDirector(builder);

            metronomeModel = director.ConstructDefaultMetronomeModel();

            this.BindingContext = metronomeModel;
        }

        private void ButtonMetronomeStart_Clicked(object sender, EventArgs e)
        {

            metronomeModel.Run(); 
        }
        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            ((IMetronomeModel)BindingContext)?.RestartTimer();
        }
    }
}
