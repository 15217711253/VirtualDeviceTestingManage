using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceTestingManage.Common;
using VirtualDeviceTestingManage.Dal;
using VirtualDeviceTestingManage.Domain;

namespace ConsoleApp_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new DeviceCommand
            {
                Id = 1, Name = "开机", CmdFormat = "partmer1 = {0} ,partmer2 = {1}"
            };

            var s = string.Format(cmd.CmdFormat, "123", "456");

            Console.ReadKey();
        }
    }

    public class DeviceCommand
    {
        public int Id { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 命令格式
        /// </summary>
        public string CmdFormat { get; set; }



    }

}
