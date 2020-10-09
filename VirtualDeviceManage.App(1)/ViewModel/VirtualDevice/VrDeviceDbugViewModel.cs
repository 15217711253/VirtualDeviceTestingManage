/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ViewModel.VirtualDevice
*文件名称   ：VirtualDeviceDebugViewModel.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/10 星期四 14:29:06 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/10 星期四 14:29:06 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using Common.SocketExtend;
using GalaSoft.MvvmLight.Ioc;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceManage.App.DeviceVirtualStaute;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.Views;
using VirtualDeviceTestingManage.Domain;

namespace VirtualDeviceManage.App.ViewModel
{
    public class VrDeviceDbugViewModel : VrDeviceViewModelBase,IDisposable
    {
        public VrDeviceDbugViewModel(VirtualNetworkDevice networkDevice )
            : base(networkDevice)
        {
            InitialNetServer();
        
        }
        //public VrDeviceDbugViewModel(VirtualNetworkDevice networkDevice )
        //   : base(networkDevice)
        //{
        //    InitialNetServer();
        //}

        public ICommProtocol commProtocol { get; set; }
        public Window view { get; set; }
 
        public ObservableCollection<string> TestLogger 
        {
            get { return _TestLogger; }
            set { Set(ref _TestLogger, value); }
        }

        private ObservableCollection<string> _TestLogger = new ObservableCollection<string>();
        private DotnetSocketServer server;
        private void InitialNetServer()
        {
            if (base.Source.IpAddrees == null)
                return;
            if (server == null)
            {
                server = new DotnetSocketServer();
 

                var Protocols = GlobalData.GetCommProtocols();

                var ProtocolTypeName = Protocols.Where(x => x.Name == Source.CommProtocol).FirstOrDefault();

                Type type = Type.GetType(ProtocolTypeName.ToString(), false);

                if (type == null)
                {
                    Console.WriteLine("无法加载配置");
                    return;
                }
                var obj = Activator.CreateInstance(type,server);
                commProtocol = obj as ICommProtocol;
                


                //Debug设备页面
                var DeviceViews = GlobalData.GetDeviceViews();

                var DeviceViewTypeName = DeviceViews.Where(x => x.Name == Source.CommProtocol).FirstOrDefault();

                Type DeviceViewtype = Type.GetType(DeviceViewTypeName.ToString(), false);

                if (type == null)
                {
                    Console.WriteLine("无法加载配置");
                    return;
                }
                var viewobj = Activator.CreateInstance(DeviceViewtype,this);
                view = viewobj as Window;
                
                view.DataContext = commProtocol;
                view.Show();
                view.Closed += View_Closed;

                //Todo:有相同地址时会报错...
                server.StartListen(RevMsg, base.Source.IpAddrees, base.Source.Port);
            }

        }

        private void View_Closed(object sender, EventArgs e)
        {
            Dispose();
        }

        public void doSomeWork()
        {
            var i = commProtocol as IDevice_Stuate;
            i.doSomeWork();
        }

        


        public void RevMsg(object obj)
        {
            var cus_msg = obj as CustomMsg;
            
            /// 接收的数据打印到View
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate
            {
                TestLogger.Add($"[{cus_msg.remoteEndPoint}] : {cus_msg.msg}");
            }));

            /// 接收处理接口
            commProtocol.RevMsg(obj);

        }

        public void Dispose()
        {

            server.Stop();
            server = null;
            view = null;
            
        }
    }
}
