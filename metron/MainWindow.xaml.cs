using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


namespace metron
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MetronomeViewModel();

            /*
            Slider_BPM.Value = 100;
            this.TextBox_BPM.Text = "100";

            Metronome = MetronomeModel.GetInstance("1000");
            this.TextBox_Pattern.Text = Metronome.MetronomePattern;
            */
            

        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((MetronomeViewModel)DataContext)?.TempoSliderMoved();
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            
            ((MetronomeViewModel)DataContext).Stop();
        }

        private void Button_Click_Pattern(object sender, RoutedEventArgs e)
        {
            ((MetronomeViewModel)DataContext).ChangePattern();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {

            ((MetronomeViewModel)DataContext).Run();

            
        }


    }
}
