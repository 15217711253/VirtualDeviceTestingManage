/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App
*文件名称   ：GlobalData.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/10 星期四 15:49:00 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/10 星期四 15:49:00 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.CommProviders;

namespace VirtualDeviceManage.App
{
    public class GlobalData
    {
        public static string[] Ipadds { get; set; } = new string[] { };
        public static string[] SubMarks { get; set; }

        public static List<ICommProtocol> GetCommProtocols()
        {
            //var s = new Object().GetType().Assembly;

            var catalog = new AssemblyCatalog(GetTypeMethod().Assembly);
            var container = new CompositionContainer(catalog);
            var commProtocols = container.GetExportedValues<ICommProtocol>();

            return commProtocols.ToList();
        }

        private static Type GetTypeMethod()
        {
            GlobalData globalData = new GlobalData();
            return globalData.GetType();
        }
    }
}
