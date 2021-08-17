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



namespace Metron
{

    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MetronomeModel(new WpfAppBuilder());

            
            //Setting window position
            Left = MetronWPF.Properties.Settings.Default.WindowPosition.Left;
            Top = MetronWPF.Properties.Settings.Default.WindowPosition.Top;

           
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((MetronomeModel)DataContext)?.RestartTimer();
        }


        private void Button_Click_Pattern(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).RestartTimer();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            if (!((MetronomeModel) DataContext).IsRunning)
            {
                ButtonStart.Content = "Stop";
                ((MetronomeModel) DataContext).Run();
            }
            else
            {
                ButtonStart.Content = "Start";
                ((MetronomeModel)DataContext).Stop();
            }


        }
        private void Button_Click_Minus_10_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel) DataContext).Tempo -= 10;
        }
        private void Button_Click_Plus_10_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).Tempo += 10;
        }
        private void Button_Click_Minus_2_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).Tempo -= 2;
        }

        private void Button_Click_Plus_2_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).Tempo += 2;
        }

        private void Button_Click_x2_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).Tempo *= 2;
        }

        private void Button_Click_dev2_BPM(object sender, RoutedEventArgs e)
        {
            ((MetronomeModel)DataContext).Tempo /= 2;
        }

        private void Button_SpeedTrainer_Start(object sender, RoutedEventArgs e)
        {
            if (!((MetronomeModel)DataContext).IsSpeedTrainerActivated)
            {
                ButtonSpeedTrainer.Content = "Deactivate speed trainer";
                ((MetronomeModel)DataContext).IsSpeedTrainerActivated = true;
            }
            else
            {
                ButtonSpeedTrainer.Content = "Activate speed trainer";
                ((MetronomeModel)DataContext).IsSpeedTrainerActivated = false;
            }
        }



        private void TextBox_Pattern_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            


            if (!Int32.TryParse(e.Text, out int value))
            {
                e.Handled = true;
            }
            else
            {
                if(!(e.Text.Equals("0") || e.Text.Equals("1")))
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBox_Pattern_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MetronWPF.Properties.Settings.Default.WindowPosition = this.RestoreBounds;
            MetronWPF.Properties.Settings.Default.Save();
        }



        
    }
}
