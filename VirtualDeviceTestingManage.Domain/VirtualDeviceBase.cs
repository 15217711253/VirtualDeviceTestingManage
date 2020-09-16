/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceTestingManage.Domain
*文件名称   ：VirtualDeviceBase.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 14:55:13 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 14:55:13 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualDeviceTestingManage.Domain
{
    public abstract class VirtualDeviceBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CommProtocol { get; set; }
        public DeviceModel BrandModel { get; set; }
        public DeviceType DeviceType { get; set; }
        public string IpAddrees { get; set; }
        public int Port { get; set; }

        public DateTime? AddDateTime { get; set; }



    }
}
