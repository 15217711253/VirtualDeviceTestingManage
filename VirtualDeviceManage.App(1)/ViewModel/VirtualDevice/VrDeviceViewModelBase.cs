/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ViewModel.VirtualDevice
*文件名称   ：VirtualDeviceBase.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/10 星期四 14:37:22 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/10 星期四 14:37:22 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceTestingManage.Common;
using VirtualDeviceTestingManage.Domain;

namespace VirtualDeviceManage.App.ViewModel
{
    public class VrDeviceViewModelBase : ViewModelBase
    { /*-------------------------------------- Fields ----------------------------------------*/

        private VirtualNetworkDevice _Source;
        private bool _isSelected;


        /*-------------------------------------Properties --------------------------------------*/

        public VirtualNetworkDevice Source
        {
            get { return _Source; }
            set { Set(ref _Source, value); }
        }
        public bool isSelected
        {
            get { return _isSelected; }
            set { Set(ref _isSelected, value); }
        }

        private string _SelectedProtocol = string.Empty;

        /*----------------------------------- Constructors -------------------------------------*/
        public VrDeviceViewModelBase(VirtualNetworkDevice networkDevice)
        {
            Source = networkDevice;
            
            //NetWorkProvider.UpdateNetWorkIpAddress(new string[3], new string[3], new string[3]);
        }

        public override string ToString()
        {
            return base.ToString();
        }
        /*---------------------------------- Public Methods ------------------------------------*/

        /*---------------------------------- Private Method ------------------------------------*/
    }
}
