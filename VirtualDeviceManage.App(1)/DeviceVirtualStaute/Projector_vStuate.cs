/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.DeviceVirtualStaute
*文件名称   ：Projector_vStuate.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/16 星期三 11:11:36 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/16 星期三 11:11:36 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDeviceManage.App.DeviceVirtualStaute
{
    public class Projector_vStuate:GalaSoft.MvvmLight.ViewModelBase
    {

        public Projector_vStuate()
        {
            ErrCode = new Dictionary<int, string>();
            ErrCode.Add(0, "0：未检测到错误");
            ErrCode.Add(1, "1：警告");
            ErrCode.Add(2, "2：错误");

        }
        /// <summary>
        /// 投影机开关
        /// </summary>
 
        private bool _Power=false;

        public bool Power
        {
            get { return _Power ; }
            set
            {
                _Power = value;
                this.RaisePropertyChanged("Power");
            }
        }


        private bool _IsOpen;

        public bool IsOpen
        {
            get { return IsOpen; }
            set
            {
                IsOpen = value;
                this.RaisePropertyChanged("IsOpen");
            }
        }

        private bool _isClose;

        public bool IsClose
        {
            get { return _isClose; }
            set
            {
                _isClose = value;
                this.RaisePropertyChanged("IsClose");
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

        private Dictionary<int,string> _errCode;

        public Dictionary<int,string> ErrCode
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

       
 


    }

    
}
