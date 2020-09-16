using Common.SocketExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace Common.SocketExtend
{
    #region Server
    public class SocketServer
    {
        public SocketServer(int p)
        {
            Port = p;
        }

        private int Port { get; set; }
        private bool sLock = false;
        public bool IsClose { get; set; }

        /// <summary>
        /// SocketServer对象
        /// </summary>
        public Socket socketServer { get; set; }

        /// <summary>
        /// 客户端队列集合
        /// </summary>
        public List<Socket> ClientList = new List<Socket>();

        /// <summary>
        /// Socket客户端列表，key为socket，value为心跳定时器
        /// </summary>
        public Dictionary<Socket, System.Timers.Timer> socketTimerDic = new Dictionary<Socket, System.Timers.Timer>();
        private int thdRevMethod1;
        private int v;



        /// <summary>
        /// 接受到消息委托
        /// </summary>
        public Action<object> RevMsg { get; set; }
        public SocketServer(Action<object> RevMsg, int p)
        {
            try
            {
                Port = p;
                this.RevMsg = RevMsg;
                //新建一个Socket服务端实例，并绑定到20000端口
                socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketServer.Bind(new IPEndPoint(IPAddress.Any, Port));

                //设置最大监听数量
                socketServer.Listen(10);

                //开启一个线程监听客户端
                Thread thd = new Thread(new ThreadStart(ListenSocket));
                thd.Start();
                ClientList = new List<Socket>();

                Console.WriteLine("服务器开启");
                IsClose = false;

            }
            catch (Exception ex)
            {
                IsClose = true;
                Console.WriteLine("程序出现异常:" + ex);
            }
        }

        public SocketServer(int thdRevMethod1, int v)
        {
            this.thdRevMethod1 = thdRevMethod1;
            this.v = v;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        private void ListenSocket()
        {
            try
            {
                while (true)
                {
                    if (IsClose) break;
                    Socket _socket = socketServer.Accept();
                    CustomMsg msg = new CustomMsg(0, _socket.RemoteEndPoint.ToString(), "连接成功！");
                    RevMsg.Invoke(msg);
                    Console.WriteLine("新客户端连接成功");
                    //开始心跳
                    System.Timers.Timer heartbeatTimer = new System.Timers.Timer();
                    heartbeatTimer.Interval = 1500;
                    heartbeatTimer.Elapsed += new System.Timers.ElapsedEventHandler((s, e) => heartbeatTimerIsUp(s, e, _socket));
                    heartbeatTimer.Start();



                    socketTimerDic.Add(_socket, null);

                    Thread thdReceive = new Thread(new ParameterizedThreadStart(thdRevMethod));
                    thdReceive.Start(_socket);

                    ClientList.Add(_socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出现异常:" + ex);
            }
        }

        public void Stop()
        {
            foreach (var s in socketTimerDic)
            {
                if (s.Value != null)
                    s.Value.Stop();
                s.Key.Close();
                Console.WriteLine("关闭一个客户端");
            }

            socketTimerDic.Clear();

            socketServer.Close();
            socketServer.Dispose();
            socketServer = null;
            IsClose = true;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="obj"></param>
        private void thdRevMethod(object obj)
        {
            Socket _socket = obj as Socket;

            while (true)
            {
                try
                {

                    if (IsClose) return;
                    byte[] resByte = new byte[1024];
                    int resInt = _socket.Receive(resByte);
                    if (resInt > 0)
                    {
                        string res = Encoding.Default.GetString(resByte, 0, resInt);
                        if (RevMsg != null)
                        {
                            CustomMsg msg = new CustomMsg(3, _socket.RemoteEndPoint.ToString(), res);
                            RevMsg.Invoke(msg);
                            Console.WriteLine(msg.msg);
                        }

                    }
                }
                catch (SocketException sex)
                {
                    if (sex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        //当客户端断开连接,从列表中移除该客户端
                        if (socketTimerDic.ContainsKey(_socket))
                        {

                            socketTimerDic.Remove(_socket);

                            CustomMsg msg = new CustomMsg(1, _socket.RemoteEndPoint.ToString(), "断开成功！");
                            RevMsg.Invoke(msg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("程序出现异常:" + ex);
                    continue;
                }
            }

        }

        /// <summary>
        /// 心跳定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="_socket">对应的socket客户端实例</param>
        private void heartbeatTimerIsUp(object sender, ElapsedEventArgs e, Socket _socket)
        {
            //如果客户端的状态是连接着的，则发送心跳消息；否则从客户端列表中移除该客户端
            try
            {
                if (_socket.Connected && IsClose != true)
                {
                    byte[] sendMsg = new byte[2];
                    if (sLock == false)
                        _socket.Send(sendMsg);
                }
                else
                {
                    if (socketTimerDic.ContainsKey(_socket))
                    {
                        socketTimerDic.Remove(_socket);
                        CustomMsg msg = new CustomMsg(1, _socket.RemoteEndPoint.ToString(), "断开成功！");
                        RevMsg.Invoke(msg);
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                socketTimerDic.Remove(_socket);
                CustomMsg msg = new CustomMsg(1, _socket.RemoteEndPoint.ToString(), "断开成功！");
                RevMsg.Invoke(msg);
                return;
            }
            
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ipAndPord"></param>
        public bool sendMsg(string msg, string ipAndPord)
        {
            try
            {
                Socket _socket = socketTimerDic.SingleOrDefault(r => string.Equals(r.Key.RemoteEndPoint.ToString(), ipAndPord)).Key;
                sLock = true;
                if (_socket != null)
                {
                    byte[] byteStr = Encoding.Default.GetBytes(msg);
                    _socket.Send(byteStr);
                    Thread.Sleep(50);
                    sLock = false;
                    return true;
                }
                else
                {
                    sLock = false;
                    return false;
                }
            }
            catch (Exception)
            {
                sLock = false;
                return false;
            }
        }
        public int sendVarMsgAll(string msg)
        {


            sLock = true;
            byte[] datasize = new byte[4];

            byte[] bytestr = Encoding.Default.GetBytes(msg);
            int size = bytestr.Length;

            datasize = BitConverter.GetBytes(size);
            try
            {
                foreach (var s in socketTimerDic)
                {
                    int total = 0;
                    int sent;
                    int dataleft = size;
                    var _socket = s.Key;

                    sent = _socket.Send(datasize);

                    while (total < size)
                    {
                        sent = _socket.Send(bytestr, total, dataleft, SocketFlags.None);
                        total += sent;
                        dataleft -= sent;
                    }
                    Thread.Sleep(50);
                }

                sLock = false;
            }
            catch (Exception)
            {
                sLock = false;
                return 0;
            }
            return size;
        }

        public void sendMsgAll(string msg)
        {
            sLock = true;
            try
            {
                foreach (var s in socketTimerDic)
                {
                    var _socket = s.Key;
                    if (_socket != null)
                    {


                        byte[] byteStr = Encoding.Default.GetBytes(msg);


                        _socket.Send(byteStr);

                    }
                }

                Thread.Sleep(50);
                sLock = false;
            }
            catch (Exception)
            {
                sLock = false;
                return;
            }
        }
    }
    #endregion
}
