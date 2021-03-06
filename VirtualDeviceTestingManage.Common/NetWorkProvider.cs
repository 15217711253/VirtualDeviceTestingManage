﻿using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;

namespace VirtualDeviceTestingManage.Common
{

    public class NetWorkProvider
    {

        /// <summary></summary>  
        /// 显示本机各网卡的详细信息  
        /// <summary></summary>  
        public static void ShowNetworkInterfaceMessage()
        {
            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in fNetworkInterfaces)
            {
                #region " 网卡类型 "
                string fCardType = "未知网卡";
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                if (rk != null)
                {
                    // 区分 PnpInstanceID   
                    // 如果前面有 PCI 就是本机的真实网卡  
                    // MediaSubType 为 01 则是常见网卡，02为无线网卡。  
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                    if (fPnpInstanceID.Length > 3 &&
                        fPnpInstanceID.Substring(0, 3) == "PCI")
                    {

                        fCardType = "物理网卡";
                        #region 网卡信息
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("-- " + fCardType);
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("Id .................. : {0}", adapter.Id); // 获取网络适配器的标识符  
                        Console.WriteLine("Name ................ : {0}", adapter.Name); // 获取网络适配器的名称  
                        Console.WriteLine("Description ......... : {0}", adapter.Description); // 获取接口的描述  
                        Console.WriteLine("Interface type ...... : {0}", adapter.NetworkInterfaceType); // 获取接口类型  
                        Console.WriteLine("Is receive only...... : {0}", adapter.IsReceiveOnly); // 获取 Boolean 值，该值指示网络接口是否设置为仅接收数据包。  
                        Console.WriteLine("Multicast............ : {0}", adapter.SupportsMulticast); // 获取 Boolean 值，该值指示是否启用网络接口以接收多路广播数据包。  
                        Console.WriteLine("Speed ............... : {0}", adapter.Speed); // 网络接口的速度  
                        Console.WriteLine("Physical Address .... : {0}", adapter.GetPhysicalAddress().ToString()); // MAC 地址  

                        IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();
                        UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;
                        foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                        {
                            if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                                Console.WriteLine("Ip Address .......... : {0}", UnicastIPAddressInformation.Address); // Ip 地址  
                        }
                        Console.WriteLine();
                        #endregion

                    }
                    else if (fMediaSubType == 1)
                        fCardType = "虚拟网卡";
                    else if (fMediaSubType == 2)
                    {
                        fCardType = "无线网卡";


                    }
                }
                #endregion
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 新增IP地址
        /// </summary>
        /// <param name="ipAddrs"></param>
        /// <param name="subnetMask"></param>
        /// <param name="ipGateway"></param>
        public static void UpdateNetWorkIpAddress(string[] ipAddrs,string[] subnetMask,string[] ipGateway)
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;
                //设置ip地址和子网掩码 
                inPar = mo.GetMethodParameters("EnableStatic");
                inPar["IPAddress"] = ipAddrs;
                inPar["SubnetMask"] = subnetMask;
                outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                //设置网关地址 
                inPar = mo.GetMethodParameters("SetGateways");
                inPar["DefaultIPGateway"] = ipGateway;
                outPar = mo.InvokeMethod("SetGateways", inPar, null);
                //设置DNS 
                inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                inPar["DNSServerSearchOrder"] = new string[] { "221.11.1.67" };
                outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
            }
        }

        /// <summary>
        /// 退出程序将IP设为DHCP
        /// </summary>
        public static void setNetWorkDHCP()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"]) continue;
                mo.InvokeMethod("SetDNSServerSearchOrder", null);
                mo.InvokeMethod("EnableStatic", null);
                mo.InvokeMethod("SetGateways", null);
                mo.InvokeMethod("EnableDHCP", null);
                break;
            }

        }
    }
}