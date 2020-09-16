using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SocketHeartEx.DotnetSocket
{
    public class DotnetSocketClient
    {
        public Action<object> RevMsg { get; set; }
        public DotnetSocketClient()
        {
            
        }
        /// <summary>
        /// 重连定时器
        /// </summary>
        public System.Timers.Timer reconnectTimer;
        byte[] buffer = new byte[2048];
        Socket socket;
        Thread thread;
        public bool Connected => socket.Connected;
        public string Ipaddr { get; set; }
        public int Port { get; set; }

        public void ConnToServer(Action<object> RevMsg, string host, int port)
        {
            Ipaddr = host;
            Port = port;
            this.RevMsg = RevMsg;
            try
            {
                //5000ms重连一次

                reconnectTimer = new System.Timers.Timer();
                reconnectTimer.Interval = 5000;
                reconnectTimer.Elapsed += new System.Timers.ElapsedEventHandler(timeIsUp);
                ConnToServer();
            }
            catch (SocketException)
            {
                Console.WriteLine("连接到服务端失败！5s后开始重连！");
                reconnectTimer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
                reconnectTimer.Start();
            }
        }

        private void ConnToServer()
        {
            try
            {

                //实例化socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //连接服务器
                socket.Connect(Ipaddr, Port);
                thread = new Thread(StartReceive);
                thread.IsBackground = true;
                thread.Start(socket);
            }
            catch (SocketException sex)
            {
                if (sex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    Console.WriteLine("连接到服务端失败！5s后开始重连！");
                    reconnectTimer.Start();
                    //heartbeatTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务器异常:" + ex.Message);
        
            }
        }

        private void timeIsUp(object sender, ElapsedEventArgs e)
        {
            reconnectTimer.Stop();
            ConnToServer();
        }

        /// <summary>
        /// 开启接收
        /// </summary>
        /// <param name="obj"></param>
        private void StartReceive(object obj)
        {
            string str;
            string tempstr = string.Empty;
            while (true)
            {
                Socket receiveSocket = obj as Socket;
                try
                {
                    int result = receiveSocket.Receive(buffer,SocketFlags.None);
                    if (result == 0)
                    {
                        Console.WriteLine("服务端断开！5s后重连！");
                        reconnectTimer.Start();
                        break;
                    }
                    else
                    {
                        str = Encoding.Default.GetString(buffer,0,result);
                       

                        if (str.IndexOf("<EOF>") == -1)
                        {
                            tempstr += str;
                        }
                        else
                        {
                            tempstr += str;
                            tempstr = tempstr.Replace("<EOF>", "");
                            SetMsg(tempstr);
                            tempstr = string.Empty;

                        }
                    }



                }
                catch (SocketException sex)
                {
                    if (sex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        Console.WriteLine("服务端断开！5s后重连！");
                        reconnectTimer.Start();
                    }
                    if (sex.SocketErrorCode == SocketError.ConnectionAborted)
                    {
                        Console.WriteLine("服务端断开！5s后重连！");
                        reconnectTimer.Start();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务器异常:" + ex.Message);

                }
            }

        }

        public void Send(string str)
        {
            try
            {
                var msg = str + "<EOF>";
                socket.Send(Encoding.Default.GetBytes(msg));
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            
        }

        public void Close()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                thread.Abort();
                SetMsg("关闭与远程服务器的连接!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常" + ex.Message);
            }
        }

        private void SetMsg(string msg)
        {
            RevMsg.Invoke(msg);
        }
    }


}
