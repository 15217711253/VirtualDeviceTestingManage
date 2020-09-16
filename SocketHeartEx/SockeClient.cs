using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace Common.SocketExtend
{
    #region client
    public class SocketClient
    {
        /// <summary>
        /// Socket客户端
        /// </summary>
        public static Socket socketClient { get; set; }
        /// <summary>
        /// 重连定时器
        /// </summary>
        public static System.Timers.Timer reconnectTimer;
        /// <summary>
        /// 心跳定时器
        /// </summary>
        public static System.Timers.Timer heartbeatTimer;

        public SocketClient()
        {
        }
        public Action<object> RevMsg { get; set; }
        public void InitSocket(string ip, int port, Action<object> RevMsg)
        {
            Ipaddr = ip;
            Port = port;
            this.RevMsg = RevMsg;
            try
            {
                //5000ms重连一次

                reconnectTimer = new System.Timers.Timer();
                reconnectTimer.Interval = 5000;
                reconnectTimer.Elapsed += new System.Timers.ElapsedEventHandler(timeIsUp);

                heartbeatTimer = new System.Timers.Timer();
                heartbeatTimer.Interval = 60000;
                heartbeatTimer.Elapsed += new System.Timers.ElapsedEventHandler(heartbeatTimerIsUp);
                ConneceToServer();
            }
            catch (SocketException)
            {
                Console.WriteLine("连接到服务端失败！5s后开始重连！");
                reconnectTimer.Start();
                heartbeatTimer.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
                reconnectTimer.Start();
                heartbeatTimer.Stop();
            }

        }

        public string Ipaddr { get; set; }
        public int Port { get; set; }


        /// <summary>
        /// 心跳定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void heartbeatTimerIsUp(object sender, ElapsedEventArgs e)
        {
            //状态为连接则发送当前时间,否则停止心跳定时器
            //if (socketClient.Connected)
            //{
            //    byte[] resByte = Encoding.Default.GetBytes(DateTime.Now.ToShortDateString());
            //    socketClient.Send(resByte);
            //}
            //else
            //{
            //    heartbeatTimer.Stop();
            //    reconnectTimer.Start();
            //}

            if (socketClient.Connected != true)
            {
                heartbeatTimer.Stop();
                reconnectTimer.Start();
            }
        }

        /// <summary>
        /// 重连定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeIsUp(object sender, EventArgs e)
        {
            reconnectTimer.Stop();
            ConneceToServer();
        }

        /// <summary>
        /// 连接到服务端
        /// </summary>
        public void ConneceToServer()
        {
            try
            {
                // 新建客户端实例，并连接到服务端所在的端口号
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketClient.Connect(Ipaddr, Port);
                Console.WriteLine("成功连接到服务端");
                //heartbeatTimer.Start();

                //开启一个线程发送消息
                Thread thdSend = new Thread(new ThreadStart(thdSendMethod));
                thdSend.Start();

                //开启一个线程接收信息
                Thread thdRev = new Thread(new ThreadStart(thdRevVarMethod));
                thdRev.Start();

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
            catch (Exception)
            {
                heartbeatTimer.Stop();
                throw;
            }
        }

        public void DisConnect()
        {
            if (socketClient != null)
            {
                socketClient.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                socketClient.Close();
                socketClient = null;

                reconnectTimer.Stop();
                heartbeatTimer.Stop();

            }
        }

        private void thdRevVarMethod()
        {//socketClient

         
            //byte[] resByte = new byte[1024];
            //int resInt = socketClient.Receive(resByte);
            //if (resInt > 0)
            //{
            //    RevMsg.Invoke(Encoding.Default.GetString(resByte, 0, resInt));
            //    Console.WriteLine(Encoding.Default.GetString(resByte, 0, resInt));
            //}

            int total = 0;
            int recv;
            byte[] datasize = new byte[4];


            while (true)
            {
                try
                {
                    GC.Collect();
                    recv = socketClient.Receive(datasize, 0, 4, SocketFlags.None);
                    int size = BitConverter.ToInt32(datasize, 0);
                    int dataleft = size;
                    byte[] data = new byte[size];
                    if (data.Length == 0)
                        continue;
                    var cts = new CancellationTokenSource();
                    cts.Token.Register(() =>
                    {
                        size = 0;
                        dataleft = 0;

                        recv = 0;
                        total = 0;
                        data = null;

                        Console.WriteLine("线程中止");
                    });

                    cts.CancelAfter(50);


                    while (total < size)
                    {
                        recv = socketClient.Receive(data, total, dataleft, SocketFlags.None);
                        Console.WriteLine(string.Format("total : {0} , dataleft : {1}\n", total.ToString(), dataleft.ToString()));
                        if (recv == 0)   //接收到空字节时
                        {
                            data = null;
                            break;
                        }

                        total += recv;
                        dataleft -= recv;
                    }
                    Console.WriteLine(Encoding.Default.GetString(data, 0, size));
                    RevMsg.Invoke(Encoding.Default.GetString(data, 0, size));
                }
                catch (SocketException sex)
                {
                    if (sex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        Console.WriteLine("服务端断开！5s后重连！");
                        reconnectTimer.Start();
                        heartbeatTimer.Stop();
                    }
                    if (sex.SocketErrorCode == SocketError.ConnectionAborted)
                    {
                        Console.WriteLine("服务端断开！5s后重连！");
                        reconnectTimer.Start();
                        heartbeatTimer.Stop();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("程序出现异常:" + ex);
                    heartbeatTimer.Stop();
                }


            }


        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void thdRevMethod()
        {
            try
            {
                while (true)
                {
                    byte[] resByte = new byte[1024];
                    int resInt = socketClient.Receive(resByte);
                    if (resInt > 0)
                    {
                        RevMsg.Invoke(Encoding.Default.GetString(resByte, 0, resInt));
                        Console.WriteLine(Encoding.Default.GetString(resByte, 0, resInt));
                    }
                }

            }
            catch (SocketException sex)
            {
                if (sex.SocketErrorCode == SocketError.ConnectionReset)
                {
                    Console.WriteLine("服务端断开！5s后重连！");
                    reconnectTimer.Start();
                    heartbeatTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
                heartbeatTimer.Stop();
            }
        }

        //发送信息
        private void thdSendMethod()
        {
            try
            {
                while (true)
                {
                    string res = Console.ReadLine();
                    byte[] resByte = Encoding.Default.GetBytes(res);
                    socketClient.Send(resByte);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
                //heartbeatTimer.Stop();
            }
        }


        public void SendMsg(string res)
        {
            try
            {
                if (socketClient.Connected == false)
                    return;

                byte[] resByte = Encoding.Default.GetBytes(res);
                socketClient.Send(resByte);
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
                //heartbeatTimer.Stop();
            }
        }
    }
    #endregion
}
