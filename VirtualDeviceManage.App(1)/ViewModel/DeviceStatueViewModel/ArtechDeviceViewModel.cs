using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VirtualDeviceManage.App.ViewModel
{
 
    public class ArtechDeviceViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public ArtechDeviceViewModel()
        {
            Statue_Oiler = new List<bool>() { true, true, true, true };
            Statue_Motor = new List<bool>() {true,true,true,true,true,true,true,true, true, true, true, true, true, true, true, true };
            Warm = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        }

        private string _DeviceId;

        public string DeviceId
        {
            get { return _DeviceId; }
            set
            {
                _DeviceId = value;
                this.RaisePropertyChanged("DeviceId");
            }
        }


        ///// <summary>
        ///// 电源
        ///// </summary>

        //private bool _Power = true;

        //public bool Power
        //{
        //    get { return _Power; }
        //    set
        //    {
        //        _Power = value;
        //        this.RaisePropertyChanged("Power");
        //    }
        //}



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

        private int _Volume;

        public int Volume
        {
            get { return _Volume; }
            set
            {
                _Volume = value;
                this.RaisePropertyChanged("Volume");
            }
        }

        private bool _LiveControl;

        public bool LiveControl
        {
            get { return _LiveControl; }
            set
            {
                _LiveControl = value;
                this.RaisePropertyChanged("LiveControl");
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

     

        private List<bool> _Statue_Oiler;

        public List<bool> Statue_Oiler
        {
            get { return _Statue_Oiler; }
            set
            {
                _Statue_Oiler = value;
                this.RaisePropertyChanged("Statue_Oiler");
            }
        }


        private List<bool> _Statue_Motor;
        /// <summary>
        /// 每一位动雕的开关
        /// </summary>
        public List<bool> Statue_Motor
        {
            get { return _Statue_Motor; }
            set
            {
                _Statue_Motor = value;
                this.RaisePropertyChanged("Statue_Motor");
            }
        }

        private List<bool> _Warm;

        public List<bool> Warm
        {
            get { return _Warm; }
            set
            {
                _Warm = value;
                this.RaisePropertyChanged("Warm");
            }
        }


        //public override string ToString()
        //{
        //    return $"{}";
        //}
    }
}

