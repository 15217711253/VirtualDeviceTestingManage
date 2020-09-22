using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel.DeviceStatueViewModel
{
    public class PJLinkDeviceStatueViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public PJLinkDeviceStatueViewModel()
        {
            ErrCode = new Dictionary<int, string>();
            ErrCode.Add(0, "0：未检测到错误");
            ErrCode.Add(1, "1：警告");
            ErrCode.Add(2, "2：错误");

        }
        /// <summary>
        /// 投影机开关
        /// </summary>

        private bool _Power = false;

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
        /// 灯泡时间
        /// </summary>

        private int _LampTime = 6000;

        public int LampTime
        {
            get { return _LampTime; }
            set
            {
                _LampTime = value;
                this.RaisePropertyChanged("LampTime");
            }
        }

        private Dictionary<int, string> _errCode;

        public Dictionary<int, string> ErrCode
        {
            get { return _errCode; }
            set
            {
                _errCode = value;
                this.RaisePropertyChanged("ErrCode");
            }
        }

        private object _fanError;

        public object FanError
        {
            get { return _fanError; }
            set
            {
                _fanError = value;
                this.RaisePropertyChanged("FanError");
            }
        }

        private object _LightError;

        public object LightError
        {
            get { return _LightError; }
            set
            {
                _LightError = value;
                this.RaisePropertyChanged("LightError");
            }
        }

        private object _TempError;

        public object TempError
        {
            get { return _TempError; }
            set
            {
                _TempError = value;
                this.RaisePropertyChanged("TempError");
            }
        }
        private object _OpenCoverError;

        public object OpenCoverError
        {
            get { return _OpenCoverError; }
            set
            {
                _OpenCoverError = value;
                this.RaisePropertyChanged("OpenCoverError");
            }
        }

        private object _FilterError;

        public object FilterError
        {
            get { return _FilterError; }
            set
            {
                _FilterError = value;
                this.RaisePropertyChanged("FilterError");
            }
        }

        private object _OtherError;

        public object OtherError
        {
            get { return _OtherError; }
            set
            {
                _OtherError = value;
                this.RaisePropertyChanged("OtherError");
            }
        }

    }
}
