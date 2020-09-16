﻿/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.CommProviders
*文件名称   ：PJLinkProtocol.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/14 星期一 11:21:21 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/14 星期一 11:21:21 
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

namespace VirtualDeviceManage.App.CommProviders
{
    [Export(typeof(ICommProtocol))]
    public class PJLinkCommProtocol : ICommProtocol
    {
        private DotnetSocketServer socketServer { get; set; }
        public string Name { get; private set; } = "PJLink";

        public PJLinkCommProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }

        public PJLinkCommProtocol()
        {
        }
        public void RevMsg(object obj)
        {
            var cus_msg = obj as CustomMsg;

            string RevStr = $"[{cus_msg.remoteEndPoint}] : {cus_msg.msg}";

            if (socketServer == null)
                return;

            switch (cus_msg.msg)
            {
                case "aa":
                    socketServer.Send(cus_msg.remoteEndPoint, "aabb");

                    break;



                default:
                    break;
            }

        }
    }
}
