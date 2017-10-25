using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    public interface IMetromomeSound
    {
        void PlayHighBeep();
        void PlayLowBeep();
    }
}
