using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel
{
    public class MITSUBISHIDeviceViewModel : ViewModelBase
    {
        public MITSUBISHIDeviceViewModel()
        {
            
        }

        private bool power;

        public bool Power
        {
            get { return power; }
            set
            {
                power = value;
                this.RaisePropertyChanged("Power");
            }
        }

        private object showMode;
                
        public object ShowMode
        {
            get { return showMode; }
            set
            {
                showMode = value;
                this.RaisePropertyChanged("ShowMode");
            }
        }

        private object statue;

        public object Statue
        {
            get { return statue; }
            set
            {
                statue = value;
                this.RaisePropertyChanged("Statue");
            }
        }

        private List<bool> devices;

        public List<bool> Devices
        {
            get { return devices; }
            set
            {
                devices = value;
                this.RaisePropertyChanged("Devices");
            }
        }




    }
}
