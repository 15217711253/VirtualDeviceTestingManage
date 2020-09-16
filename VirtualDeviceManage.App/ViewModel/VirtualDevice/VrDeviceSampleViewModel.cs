/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ViewModel
*文件名称   ：VirtualDeviceViewModel.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 18:55:10 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 18:55:10 
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
using VirtualDeviceTestingManage.Domain;

namespace VirtualDeviceManage.App.ViewModel
{
    public class VrDeviceSampleViewModel : VrDeviceDbugViewModel
    {


        /*----------------------------------- Constructors -------------------------------------*/

        public VrDeviceSampleViewModel(
            VirtualNetworkDevice networkDevice) 
            : base(networkDevice) {
        }

        /*---------------------------------- Public Methods ------------------------------------*/


        /*---------------------------------- Private Method ------------------------------------*/
    }
}
