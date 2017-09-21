using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metron
{
    public class Notifier : INotifyPropertyChanged
    {

        public String DisplayName { get; set; }

        #region INotifyPropertyChanged Members

        protected void RaisePropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
