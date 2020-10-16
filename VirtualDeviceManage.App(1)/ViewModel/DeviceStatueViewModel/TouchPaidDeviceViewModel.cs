using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel
{
    public class TouchPaidDeviceViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private bool _power;

        public bool Power
        {
            get { return _power; }
            set
            {

                _power = value;
                this.RaisePropertyChanged("Power");
            }
        }

    }
}
