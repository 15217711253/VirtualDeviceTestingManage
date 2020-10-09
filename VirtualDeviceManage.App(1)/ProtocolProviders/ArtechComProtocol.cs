using Common.SocketExtend;
using SocketHeartEx.DotnetSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceManage.App.DeviceVirtualStaute;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel.DeviceStatueViewModel;

namespace VirtualDeviceManage.App.ProtocolProviders
{
 
    [Export(typeof(ICommProtocol))]
    public class ArtechComProtocol : ArtechDeviceStatueViewModel, ICommProtocol
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
            //不同UI操作同一属性
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate
            {
                var cus_msg = obj as CustomMsg;

            string RevStr = $"[{cus_msg.remoteEndPoint}] : {cus_msg.msg}";

            if (socketServer == null)
                return;
            var Device = cus_msg.msg.Substring(1, 2);
            if (base.DeviceId!=null && ComboBoxItemtoString(base.DeviceId)== Device)
            {        
                switch (Device)
                {
                    //开机
                    case "ESS":
                            base.Power = true;
                            socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            //根据当前状态判断控制是否成功执行
                            //if (StatuestoBool(Device))
                            //{
                            //    base.Power = true;
                            //    socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            //}
                            //else
                            //    socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "ERR#");
                            break;
                    //关机
                    case "ESO":
                        base.Power = false;
                        socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                        break;
                    //表演段落
                    case "RSN":
                            base.ShowId = int.Parse(cus_msg.msg.Substring(7, 2));
                        break;
                    //状态查询
                    case "STU":
                        var statue =  GetStatues(Device);
                        break;
                    default:
                        break;
                }
                
            }
            }));

        }

        private string booltostring(bool bo)
        {
            if (bo) return "1";
            else return "0";
        }
        /// <summary>
        /// 从ComboBoxItem类型中获取数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ComboBoxItemtoString(object item)
        {
            return ((ComboBoxItem)item).Content.ToString();
        }
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        private string GetStatues(string DeviceId)
        {
            string str;
            switch (DeviceId)
            {
                //动雕1     没有 SERVO 状态
                case "A1":
                    {
                        str = "!" + ComboBoxItemtoString(base.DeviceId) + "SSS" + ComboBoxItemtoString(base.Statue_Mode)[0] +
                           ComboBoxItemtoString(base.Statue_EStop)[0] + ComboBoxItemtoString(base.Statue_Status)[0]
                          + ComboBoxItemtoString(base.Statue_APU)[0];
                        foreach (var item in base.MoverPower)
                        {
                            str = str + booltostring(item);
                        }
                        str = str + "#";
                        break;
                    }
                //动雕2     没有 SERVO 状态
                case "A2":
                    {
                        str = "!" + ComboBoxItemtoString(base.DeviceId) + "SSS" + ComboBoxItemtoString(base.Statue_Mode)[0] +
                          ComboBoxItemtoString(base.Statue_EStop)[0] + ComboBoxItemtoString(base.Statue_Status)[0]
                         + ComboBoxItemtoString(base.Statue_APU)[0];
                        foreach (var item in base.MoverPower)
                        {
                            str = str + booltostring(item);
                        }
                        str = str + "#";
                        break;
                    }
                //动雕3     没有Mover 和 SERVO 状态
                case "A3":
                    {
                        str = "!" + ComboBoxItemtoString(base.DeviceId) + "SSS" + ComboBoxItemtoString(base.Statue_Mode)[0] +
                          ComboBoxItemtoString(base.Statue_EStop)[0] + ComboBoxItemtoString(base.Statue_Status)[0]
                         + ComboBoxItemtoString(base.Statue_APU)[0];
                        
                        str = str + "#";
                        break;
                    }
                //动雕4     没有Mover 状态
                case "A4":
                    {
                        str = "!" + ComboBoxItemtoString(base.DeviceId) + "SSS" + ComboBoxItemtoString(base.Statue_Mode)[0] +
                          ComboBoxItemtoString(base.Statue_EStop)[0] + ComboBoxItemtoString(base.Statue_Status)[0]
                         + ComboBoxItemtoString(base.Statue_APU)[0]+ ComboBoxItemtoString(base.Statue_SERVO)[0];
                         str = str + "#";
                        break;
                    }
                default:
                    str = "Default";
                    break;

            }
            return str;
        }

        /// <summary>
        /// 获取设备状态是否正常
        /// </summary>
        private bool StatuestoBool(string DeviceId)
        {
            switch (DeviceId)
            {
                case "A1":
                    if (GetStatues(DeviceId) == "!A1SSSR1RN111111111#") return true;
                    break;
                case "A2":
                    if (GetStatues(DeviceId) == "!A2SSSR1RN111111111#") return true;
                    break;
                case "A3":
                    if (GetStatues(DeviceId) == "!A3SSSR1RN#") return true;
                    break;
                case "A4":
                    if (GetStatues(DeviceId) == "!A4SSSR1RNN#") return true;
                    break;
                default:
                    break;
            }
            return false;
        }
        public void doSomeWork()
        {
            base.Power=!base.Power ;
            Console.WriteLine(" Artech doSomeWork");
        }
    }
}
