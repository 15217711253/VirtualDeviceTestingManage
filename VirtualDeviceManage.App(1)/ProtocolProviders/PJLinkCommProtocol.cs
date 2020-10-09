/************************************************************************

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
using GalaSoft.MvvmLight.Command;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualDeviceManage.App.DeviceVirtualStaute;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel.DeviceStatueViewModel;

namespace VirtualDeviceManage.App.CommProviders
{
    [Export(typeof(ICommProtocol))]
    public class PJLinkCommProtocol : PJLinkDeviceStatueViewModel, ICommProtocol
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
                ///开机码
                case "%1powr 1":
                    
                    
                    Power = true; //改变投影机状态
                    socketServer.Send(cus_msg.remoteEndPoint, "%1powr=ok");
                    break;           
                    
                 ///关机码
                case "%1powr 0":
                    base.Power = false; //改变投影机状态
                    socketServer.Send(cus_msg.remoteEndPoint, "%1powr=ok");
                    break;
                 ///查询开关机状态
                case "%1powr ?":       
                    if(base.Power)
                        socketServer.Send(cus_msg.remoteEndPoint, "%1powr=1");
                    else
                        socketServer.Send(cus_msg.remoteEndPoint, "%1powr=0");
                    break;
                case "%1Lamp ?":
                    if (base.Power)
                        socketServer.Send(cus_msg.remoteEndPoint, "%1Lamp="+base.LampTime+"1");
                    else
                        socketServer.Send(cus_msg.remoteEndPoint, "%1Lamp=" + base.LampTime + "0");
                    break;
                case " %1ERST ?":
                    socketServer.Send(cus_msg.remoteEndPoint, GetStatue());
                    break;

                default :
                    break;
            }

        }

        private string GetStatue()
        {
            string statue =
                "%1ERST="
                + ((KeyValuePair<int, string>)base.FanError).Key.ToString()
                + ((KeyValuePair<int, string>)base.LightError).Key.ToString()
                + ((KeyValuePair<int, string>)base.TempError).Key.ToString()
                + ((KeyValuePair<int, string>)base.OpenCoverError).Key.ToString()
                + ((KeyValuePair<int, string>)base.FilterError).Key.ToString()
                + ((KeyValuePair<int, string>)base.OtherError).Key.ToString();

            return statue;
        }

        public void doSomeWork()
        {
            base.Power = !base.Power;
            Console.WriteLine("PJLink doSomeWork");
        }
        private Thread ChangeLampTimeThread  ;
        private RelayCommand<string> _ChangeLampTimeCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand<string> ChangeLampTimeCommand
        {
            get
            {
                return _ChangeLampTimeCommand
                    ?? (_ChangeLampTimeCommand = new RelayCommand<string>(
                    p =>
                    {
                        if (p == "Add")
                        {
                            if (ChangeLampTimeThread != null && ChangeLampTimeThread.IsAlive) ChangeLampTimeThread.Abort();
                            ChangeLampTimeThread = new Thread(new ThreadStart(new Action(() =>
                            {
                                while (true)
                                {
                                    LampTime++;
                                    System.Threading.Thread.Sleep(1000);
                                }
                            })));
 
                            ChangeLampTimeThread.Start();
                             
                        }
                        else
                        {
                            if (ChangeLampTimeThread != null && ChangeLampTimeThread.IsAlive) ChangeLampTimeThread.Abort();
                            ChangeLampTimeThread = new Thread(new ThreadStart(new Action(() =>
                            {
                                while (true)
                                {
                                    LampTime--;
                                    System.Threading.Thread.Sleep(1000);
                                }
                            })));
                            
                            ChangeLampTimeThread.Start();
                        }
                    }));
            }
        }

    }
}
