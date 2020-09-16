/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ViewModel.VirtualDevice
*文件名称   ：ProtocolCboxViewModel.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/16 星期三 10:11:47 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/16 星期三 10:11:47 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDeviceManage.App.CommProviders;

namespace VirtualDeviceManage.App.ViewModel.VirtualDevice
{
    public class ProtocolCboxViewModel : ViewModelBase
    {
        public ProtocolCboxViewModel()
        {
            var ps = GlobalData.GetCommProtocols();
            CommProtocols = new List<string>();
            //myVar.AddRange(CommProtocols.ConvertAll(c=>c.Name));
            foreach (var p in ps)
            {
                CommProtocols.Add(p.Name);
            }

            this.RaisePropertyChanged("CommProtocols");
        }

        public List<string> CommProtocols
        {
            get { return _CommProtocols; }
            set { Set(ref _CommProtocols, value); }
        }

        private List<string> _CommProtocols;





    }
}
