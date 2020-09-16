/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceTestingManage.Domain
*文件名称   ：NetwrokCommPort.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 15:21:52 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 15:21:52 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualDeviceTestingManage.Domain
{
    public class NetwrokCommPort : CommunicationPort
    {
        /// <summary>
        /// 通讯类型：1：tcp client,2:tcp server,3:udp client ,4 udp server
        /// </summary>
        public int NetworkType { get; set; }
    }
}
