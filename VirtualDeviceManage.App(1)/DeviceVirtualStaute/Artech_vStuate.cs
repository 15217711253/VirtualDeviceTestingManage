/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.DeviceVirtualStaute
*文件名称   ：SampleA_vStuate.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/16 星期三 11:10:08 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/16 星期三 11:10:08 
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
    /// <summary>
    /// 动雕设备
    /// </summary>
    public class Artech_vStuate : GalaSoft.MvvmLight.ViewModelBase
    {
        /// <summary>
        /// 动雕开关
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
        /// 动雕当前表演Show
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


    }
}
