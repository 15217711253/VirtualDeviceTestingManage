using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel
{
    public class ComputerDeviceViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private bool _SoftWareOnline = true;

        public bool SoftWareOnline
        {
            get { return _SoftWareOnline; }
            set
            {

                _SoftWareOnline = value;
                this.RaisePropertyChanged("SoftWareOnline");
            }
        }

        private string _Cpu = "23.0%";

        public string Cpu
        {
            get { return _Cpu; }
            set
            {
                _Cpu = value;
                this.RaisePropertyChanged("Cpu");
            }
        }

        private string _CpuTemp = "60.0";

        public string CpuTemp
        {
            get { return _CpuTemp; }
            set
            {
                _CpuTemp = value;
                this.RaisePropertyChanged("CpuTemp");
            }
        }

        private string _Memory = "47.7%";

        public string Memory
        {
            get { return _Memory; }
            set
            {
                _Memory = value;
                this.RaisePropertyChanged("Memory");
            }
        }

        private string _HardDisk = "53.5%";

        public string HardDisk
        {
            get { return _HardDisk; }
            set
            {
                _HardDisk = value;
                this.RaisePropertyChanged("HardDisk");
            }
        }

        private string _Ip ="192.168.106.74";

        public string Ip
        {
            get { return _Ip; }
            set
            {
                _Ip = value;
                this.RaisePropertyChanged("Ip");
            }
        }

        private string _ComputerName = "Pc1";

        public string ComputerNmae
        {
            get { return _ComputerName; }
            set
            {
                _ComputerName = value;
                this.RaisePropertyChanged("ComputerNmae");
            }
        }

       



    }
}
