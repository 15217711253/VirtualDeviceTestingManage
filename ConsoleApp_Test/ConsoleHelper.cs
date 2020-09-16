/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：ConsoleApp_Test
*文件名称   ：ConsoleHelper.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 15:59:32 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 15:59:32 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceTestingManage.Dal;

namespace ConsoleApp_Test
{
    public class GolbalData
    {
        public static VirtualDeviceManageContext context = new VirtualDeviceManageContext();
    }
    public class ConsoleHelper
    {
        /// <summary>
        /// 主菜单
        /// </summary>
        public static void MainMenu()
        { 


            while(true)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("--------------------------- 主菜单 ----------------------------------");
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("1. 虚拟设备管理 ");
                Console.WriteLine("2. 通讯协议 ");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.Write("请输入：");
                var i = (MainMenuEnum)int.Parse(Console.ReadLine());
                switch (i)
                {
                    case MainMenuEnum.VirtualDev:
                        VirtualDeviceHelper.FindAllDevices();
                        break;
                    case MainMenuEnum.CommProtocol:
                        break;
                    default:
                        return;
                }
                Console.WriteLine("");
                Console.WriteLine("");

            }
        }
    }


    public enum MainMenuEnum
    {
        VirtualDev = 1 , CommProtocol = 2

    }
}
