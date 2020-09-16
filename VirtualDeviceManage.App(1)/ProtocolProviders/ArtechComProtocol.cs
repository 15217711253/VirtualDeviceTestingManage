﻿using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceManage.App.DeviceVirtualStaute;

namespace VirtualDeviceManage.App.ProtocolProviders
{
 
    [Export(typeof(ICommProtocol))]
    public class ArtechComProtocol : Artech_vStuate, ICommProtocol
    {
        private DotnetSocketServer socketServer { get; set; }
        public string Name { get; private set; } = "Artech";

        public ArtechComProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }
        public ArtechComProtocol()
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
                case "!A1ESS000#":      //动雕1开启
                    base.Power = true;
                    socketServer.Send(cus_msg.remoteEndPoint, "!A1OK#");
                    break;
                case "!A1ESO000#":      //动雕1关闭
                    base.Power = false;
                    socketServer.Send(cus_msg.remoteEndPoint, "!A1OK#");
                    break; 
                case "!A1RSN01#":        //动雕1 表演 段落  01    
                    socketServer.Send(cus_msg.remoteEndPoint, "!A1OK#");
                    break;
         



                default:
                    break;
            }

        }

        public void doSomeWork()
        {
            base.Power=!base.Power ;
            Console.WriteLine(" Artech doSomeWork");
        }
    }
}
