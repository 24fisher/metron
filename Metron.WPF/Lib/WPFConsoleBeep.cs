using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public class WPFConsoleBeep : IMetromomeSound
    {
        void IMetromomeSound.PlayHighBeep()
        {
            Console.Beep(5000, 100);
        }

        void IMetromomeSound.PlayLowBeep()
        {
            Console.Beep(4000, 100);
        }
    }
}
