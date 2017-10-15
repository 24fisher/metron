using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public class MetronomeConsoleBeep : MetromomeBeep
    {
        public override void DoHighBeep()
        {
            Console.Beep(5000, 100);
        }

        public override void DoLowBeep()
        {
            Console.Beep(4000, 100);
        }
    }
}
