using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public class MetronomeConsoleBeep : IMetromomeBeep
    {
        void IMetromomeBeep.PlayHighBeep()
        {
            Console.Beep(5000, 100);
        }

        void IMetromomeBeep.PlayLowBeep()
        {
            Console.Beep(4000, 100);
        }
    }
}
