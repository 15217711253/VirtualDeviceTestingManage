/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ProtocolProviders
*文件名称   ：SampleACommProtocol.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/16 星期三 9:19:55 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/16 星期三 9:19:55 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceManage.App.DeviceVirtualStaute;
using VirtualDeviceManage.App.Interface;

namespace VirtualDeviceManage.App.ProtocolProviders
{
    [Export(typeof(ICommProtocol))]
    public class SampleBCommProtocol :SampleB_vStuate,ICommProtocol
    {
        private DotnetSocketServer socketServer { get; set; }
        public SampleBCommProtocol()
        {

        }

        public SampleBCommProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }

        public string Name => "SampleB";

        public void RevMsg(object obj)
        {
            // Todo : 写Sample B 接收后的方法
            return;
        }

        public void doSomeWork()
        {
            throw new NotImplementedException();
        }
    }
}
