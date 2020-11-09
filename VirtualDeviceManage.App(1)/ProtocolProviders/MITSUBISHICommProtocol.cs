using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel;

namespace VirtualDeviceManage.App.ProtocolProviders
{
    [Export(typeof(ICommProtocol))]
    class MITSUBISHICommProtocol : MITSUBISHIDeviceViewModel, ICommProtocol
    {

        private DotnetSocketServer socketServer { get; set; }
        public string Name { get; private set; } = "MITSUBISHI";

        public MITSUBISHICommProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }
        public MITSUBISHICommProtocol()
        {
        }
        public void RevMsg(object obj)
        {

            var cus_msg = obj as CustomMsg;

            string RevStr = $"[{cus_msg.remoteEndPoint}] : {cus_msg.msg}";

            if (socketServer == null)
                return;
            var cmd = cus_msg.msg.Substring(1, 4);
            switch (cmd)
            {
                case "PWON":
                    this.Power = true;

                    break;
                case "PWOF":
                    this.Power = false;
                    break;
              
                default: break;
            }
            return;
        }

        private string GetStatues()
        {

            return "a";
        }


        public void doSomeWork()
        {
            //base.Power=!base.Power ;
            Console.WriteLine(" HTX doSomeWork");
        }
    }
}