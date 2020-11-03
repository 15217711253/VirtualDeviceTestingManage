using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel
{
    public class HTXDeviceViewModel : ViewModelBase
    {
        public HTXDeviceViewModel()
        {
            DeviceWarm = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            InterStatues = new List<bool> { true,false, false, false, false, false, true, true };
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

        private bool modeWrong;

        public bool ModeWrong
        {
            get { return modeWrong; }
            set
            {
                modeWrong = value;
                this.RaisePropertyChanged("ModeWrong");
            }
        }


        private List<bool> interStatues;

        public List<bool> InterStatues
        {
            get { return interStatues; }
            set
            {
                interStatues = value;
                this.RaisePropertyChanged("InterStatues");
            }
        }



        private int showId;

        public int ShowId
        {
            get { return showId; }
            set
            {
                showId = value;
                this.RaisePropertyChanged("ShowId");
            }
        }

        private List<bool> deivceWarm;

        public List<bool> DeviceWarm
        {
            get { return deivceWarm; }
            set
            {
                deivceWarm = value;
                this.RaisePropertyChanged("DeviceWarm");
            }
        }




    }
}
