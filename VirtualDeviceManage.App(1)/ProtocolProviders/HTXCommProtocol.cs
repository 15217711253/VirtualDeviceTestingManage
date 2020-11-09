using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel;

namespace VirtualDeviceManage.App.ProtocolProviders
{
    [Export(typeof(ICommProtocol))]
    public class HTXCommProtocol : HTXDeviceViewModel, ICommProtocol
    {
        private DotnetSocketServer socketServer { get; set; }
        public string Name { get; private set; } = "HTX";

        public HTXCommProtocol(DotnetSocketServer server)
        {
            socketServer = server;
        }
        public HTXCommProtocol()
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
                    case "SHOW":
                        this.ShowId=int.Parse(cus_msg.msg.Substring(5, 2));
                        break;
                    case "STAT":
                         socketServer.SendBytes(cus_msg.remoteEndPoint, GetStatues());
                    break;
                    default: break; 
                }
                return;
        }

        private List<byte> GetStatues()
        {
            List<byte> result = new List<byte>();
            result.Add(byte.Parse("35"));
            result.AddRange(ConvertProvider.ConvertIntToByte(this.ShowId));
            
            result.AddRange(ConvertProvider.ConvertBoolListToByte(InterStatues));

            result.Add(byte.Parse("255"));

            result.AddRange(ConvertProvider.ConvertBoolListToByte(this.DeviceWarm));
            if (base.ModeWrong)
                result.Add(byte.Parse("1"));
            else
                result.Add(byte.Parse("0"));
            result.Add(byte.Parse("35"));
            return result;
        }


        public void doSomeWork()
        {
            //base.Power=!base.Power ;
            Console.WriteLine(" HTX doSomeWork");
        }
    }
}
