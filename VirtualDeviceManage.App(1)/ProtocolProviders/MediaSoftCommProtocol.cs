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


using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel;

namespace VirtualDeviceManage.App.ProtocolProviders
{
    [Export(typeof(ICommProtocol))]
    public class MediaSoftCommProtocol : MediaSoftDeviceViewModel, ICommProtocol
    {
        public MediaSoftCommProtocol()
        {

        }

        private DotnetSocketServer socketServer { get; set; }
        public string Name { get; private set; } = "MediaSoft";

        public MediaSoftCommProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }

        public void RevMsg(object obj)
        {
            var cus_msg = obj as CustomMsg;

            string RevStr = $"[{cus_msg.remoteEndPoint}] : {cus_msg.msg}";

            if (socketServer == null)
                return;

        
            switch (cus_msg.msg)
            {
                case "#mode=1#": this.SelectedIndex = 1; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=2#": this.SelectedIndex = 2; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=3#": this.SelectedIndex = 3; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=4#": this.SelectedIndex = 4; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=5#": this.SelectedIndex = 5; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=6#": this.SelectedIndex = 6; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=7#": this.SelectedIndex = 7; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "#mode=8#": this.SelectedIndex = 8; socketServer.Send(cus_msg.remoteEndPoint, "#mode=ok#"); break;
                case "?mode#": socketServer.Send(cus_msg.remoteEndPoint, "#mode="+this.SelectedIndex.ToString()+"#"); break;
                default:break;
            }
            return;
        }

        public void doSomeWork()
        {
            throw new NotImplementedException();
        }
    }
}
