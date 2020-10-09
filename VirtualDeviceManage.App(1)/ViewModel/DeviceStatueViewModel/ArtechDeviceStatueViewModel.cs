using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VirtualDeviceManage.App.ViewModel.DeviceStatueViewModel
{
 
    public class ArtechDeviceStatueViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public ArtechDeviceStatueViewModel()
        {
            MoverPower = new List<bool>() {true,true,true,true,true,true,true,true,true };
        }

        private object _DeviceId;

        public object DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                this.RaisePropertyChanged("DeviceId");
            }
        }


        /// <summary>
        /// 投影机开关
        /// </summary>

        private bool _Power = true;

        public bool Power
        {
            get { return _Power; }
            set
            {
                _Power = value;
                this.RaisePropertyChanged("Power");
            }
        }



        /// <summary>
        /// 表演段落
        /// </summary>

        private int _ShowId = 0;

        public int ShowId
        {
            get { return _ShowId; }
            set
            {
                _ShowId = value;
                this.RaisePropertyChanged("ShowId");
            }
        }

        private object _Statue_Mode;

        public object Statue_Mode
        {
            get { return _Statue_Mode; }
            set
            {
                _Statue_Mode = value;
                this.RaisePropertyChanged("Statue_Mode");
            }
        }

        private object _Statue_EStop;

        public object Statue_EStop
        {
            get { return _Statue_EStop; }
            set
            {
                _Statue_EStop = value;
                this.RaisePropertyChanged("Statue_EStop");
            }
        }

        private object _Statue_Status;

        public object Statue_Status
        {
            get { return _Statue_Status; }
            set
            {
                _Statue_Status = value;
                this.RaisePropertyChanged("Statue_Status");
            }
        }

        private object _Statue_APU;

        public object Statue_APU
        {
            get { return _Statue_APU; }
            set
            {
                _Statue_APU = value;
                this.RaisePropertyChanged("Statue_APU");
            }
        }

        private object _Statue_SERVO;

        public object Statue_SERVO
        {
            get { return _Statue_SERVO; }
            set
            {
                _Statue_SERVO = value;
                this.RaisePropertyChanged("Statue_SERVO");
            }
        }


        private List<bool> _Mover_Power;
        /// <summary>
        /// 每一位动雕的开关
        /// </summary>
        public List<bool> MoverPower
        {
            get { return _Mover_Power; }
            set
            {
                _Mover_Power = value;
                this.RaisePropertyChanged("MoverPower");
            }
        }

       
    }
}

