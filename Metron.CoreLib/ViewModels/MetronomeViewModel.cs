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
    /// Metronome view model class.
    /// Adds WPF and Xamarin data binding support to the model; Adapts metronome model interface.
    /// </summary>
    public class MetronomeViewModel 
    {
        

        private readonly MetronomeModel _metronomeModel;

        
        

        public MetronomeViewModel(IAppBuilder appBuilder)
        {

            _metronomeModel = new MetronomeModel(appBuilder);


        }


        
        
   
        
        
 
        
       
       

    
    }
}
