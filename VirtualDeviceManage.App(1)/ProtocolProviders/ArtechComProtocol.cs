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
using VirtualDeviceManage.App.ViewModel;

namespace VirtualDeviceManage.App.ProtocolProviders
{
 
    [Export(typeof(ICommProtocol))]
    public class ArtechComProtocol : ArtechDeviceViewModel, ICommProtocol
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
            if (base.DeviceId!=null && base.DeviceId == Device)
            {
                 var Cmd = cus_msg.msg.Substring(3, 3);
                 switch (Cmd)
                {
                    ////开机
                    //case "ESS":
                    //        base.Power = true;
                    //        socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                    //        //根据当前状态判断控制是否成功执行
                    //        //if (StatuestoBool(Device))
                    //        //{
                    //        //    base.Power = true;
                    //        //    socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                    //        //}
                    //        //else
                    //        //    socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "ERR#");
                    //        break;
                    ////关机
                    //case "ESO":
                    //    base.Power = false;
                    //    socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                    //    break;
                    //表演段落
                    case "RSN":
                            base.ShowId = int.Parse(cus_msg.msg.Substring(8, 1));
                            socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            break;
                    //启用实时控制
                    case "LIV":
                            base.LiveControl = true;
                            socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            break;
                    //停用实时控制
                    case "PRG":
                            base.LiveControl = false;
                            socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            break;

                    //音量调整
                    case "VOL":
                            base.ShowId = int.Parse(cus_msg.msg.Substring(8, 1));
                            socketServer.Send(cus_msg.remoteEndPoint, "!" + Device + "OK#");
                            break;

                    //状态查询
                    case "STU":
                        var statue =  GetStatues();
                        socketServer.Send(cus_msg.remoteEndPoint, "!" + statue + "#");
                        break;

                    //警报查询
                    case "ALM":
                        var Warm = GetWarm();
                        socketServer.Send(cus_msg.remoteEndPoint, "!" + Warm + "#");
                        break;
                    default:
                        break;
                } 
            }
            }));

        }

        /// <summary>
        /// 16位bool数组转为16进制字符串
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        private string ConvertBoolListToASC(List<bool> bo)
        {
            int len = bo.Count;
            int value = 0;
            string result="";
            if (len <= 16 && len>8)
            {
                byte[] bytes = new byte[2];
                foreach (bool b in bo)
                {
                    value = (value << 1) + (b ? 1 : 0);
                }
                if ((byte)((value >> 8) & 0xFF) != 0)
                    bytes[0]=((byte)((value >> 8) & 0xFF));
                if ((byte)((value) & 0xFF) != 0)
                    bytes[1]=((byte)((value) & 0xFF));

                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            else if(len<=8)
            {
                byte[] bytes = new byte[1];
                foreach (bool b in bo)
                {
                    value = (value << 1) + (b ? 1 : 0);
                } 
                if ((byte)((value) & 0xFF) != 0)
                    bytes[0] = ((byte)((value) & 0xFF));

                result = BitConverter.ToString(bytes).Replace("-", "");
            }


            return result;

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
        private string GetStatues( )
        {
            string str;
            str = "!" + base.DeviceId +
                 base.DeviceId+ 
                ComboBoxItemtoString(base.Statue_Mode)[0] +
                ComboBoxItemtoString(base.Statue_EStop)[0] +
                ComboBoxItemtoString(base.Statue_Status)[0] +
                ComboBoxItemtoString(base.Statue_APU)[0];
            foreach (var item in base.Statue_Oiler)
            {
                if (item)
                    str += "R";
                else
                    str += "S";
            } 
            str = str + ConvertBoolListToASC(base.Statue_Motor);
            str = str + "#";
           
            return str;
        }

        /// <summary>
        /// 获取设备报警信息
        /// </summary>
        /// <returns></returns>
        private string GetWarm()
        {
            string str;
            str = "!" + base.DeviceId +
                 base.DeviceId ;
            str = str +  ConvertBoolListToASC(base.Warm);  
            str = str + "#";

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
                    if (GetStatues() == "!A1SSSR1RN111111111#") return true;
                    break;
                case "A2":
                    if (GetStatues() == "!A2SSSR1RN111111111#") return true;
                    break;
                case "A3":
                    if (GetStatues() == "!A3SSSR1RN#") return true;
                    break;
                case "A4":
                    if (GetStatues() == "!A4SSSR1RNN#") return true;
                    break;
                default:
                    break;
            }
            return false;
        }
        public void doSomeWork()
        {
            //base.Power=!base.Power ;
            Console.WriteLine(" Artech doSomeWork");
        }
    }
}
