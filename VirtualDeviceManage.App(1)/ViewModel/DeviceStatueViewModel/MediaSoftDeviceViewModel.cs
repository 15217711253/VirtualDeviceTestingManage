using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.ViewModel
{
    public class MediaSoftDeviceViewModel:ViewModelBase
    {
        public MediaSoftDeviceViewModel()
        {
            MediaSoft.Add(1, "平日模式");
            MediaSoft.Add(2, "假日模式");
            MediaSoft.Add(3, "黄金周模式");
            MediaSoft.Add(4, "高峰模式");
            MediaSoft.Add(5, "离峰模式");
            MediaSoft.Add(6, "维修模式");
            MediaSoft.Add(7, "归位模式");
            MediaSoft.Add(8, "演艺表演点模式"); 
        }


        private Dictionary<int,string> _MediaSoft=new Dictionary<int, string>() ;
                
        public Dictionary<int,string> MediaSoft
        {
            get { return _MediaSoft; }
            set
            {
                _MediaSoft = value;
                this.RaisePropertyChanged("MediaSoft");
            }
        }

        private object _SelectedMode;

        public object SelectedMode
        {
            get { return _SelectedMode; }
            set
            {
                _SelectedMode = value;
                this.RaisePropertyChanged("SelectedMode");
            }
        }

        private int _SelectedIndex;

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                this.RaisePropertyChanged("SelectedIndex");
            }
        }


    }
}
