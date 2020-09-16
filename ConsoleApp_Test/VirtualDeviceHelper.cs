/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：ConsoleApp_Test
*文件名称   ：VirtualDeviceHelper.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 16:07:44 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 16:07:44 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/



using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceTestingManage.Domain;

namespace ConsoleApp_Test
{
    public class VirtualDeviceHelper
    {
        public VirtualDeviceHelper()
        {
        }

        public List<DotnetSocketServer> servers = new List<DotnetSocketServer>();

        public static void InitAllSocket()
        {
            var items = FindAllDevices();

            foreach (var item in items)
            {
                DotnetSocketServer s = new DotnetSocketServer();
                s.StartListen(RecviceDataMethod, item.IpAddrees, item.Port);
            }
        }

        private static void RecviceDataMethod(object obj)
        {
            var msg = obj as CustomMsg;
            Console.WriteLine($"【{msg.remoteEndPoint}】：{msg.msg}");
        }

       

        /// <summary>
        /// 查询所有设备
        /// </summary>
        public static List<VirtualNetworkDevice> FindAllDevices()
        {
            var items = GolbalData.context.VirtualDevices.ToList();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"--------------------------- 查询所有设备 共：{items.Count}  台设备 ------------");
            Console.WriteLine("---------------------------------------------------------------------");
            foreach (var i in items)
                Console.WriteLine(string.Format("Id = {0} , Name = {1} , Ipaddr = {2} ,Port = {3} ",i.Id,i.Name,i.IpAddrees,i.Port));

            return items;
        }

        

    }
}
