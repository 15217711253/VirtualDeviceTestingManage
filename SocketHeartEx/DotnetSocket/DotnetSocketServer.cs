using Common.SocketExtend;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketHeartEx.DotnetSocket
{
    public class DotnetSocketServer
    {
        public DotnetSocketServer()
        {
        }
        /// <summary>
        /// 接受到消息委托
        /// </summary>
        public Action<object> RevMsg { get; set; }


        //存储已连接的客户端的泛型集合
        private  Dictionary<string, Socket> socketList = new Dictionary<string, Socket>();

        private Socket socket;

        /// <summary>
        /// 接收连接
        /// </summary>
        /// <param name="obj"></param>
        private void StartServer(object obj)
        {
            string str;
            while (true)
            {
                try
                {

                    //等待接收客户端连接 Accept方法返回一个用于和该客户端通信的Socket
                    Socket recviceSocket = ((Socket)obj).Accept();
                    //获取客户端ip和端口号
                    str = recviceSocket.RemoteEndPoint.ToString();
                    socketList.Add(str, recviceSocket);
                    //控件调用invoke方法 解决"从不是创建控件的线程访问它"的异常
                    CustomMsg msg = new CustomMsg(0, recviceSocket.RemoteEndPoint.ToString(), "连接成功！");
                    SetMsg(msg);

                    //Accept()执行过后 当前线程会阻塞 只有在有客户端连接时才会继续执行
                    //创建新线程,监控接收新客户端的请求数据
                    Thread thread = new Thread(startRecive);
                    thread.IsBackground = true;
                    thread.Start(recviceSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="obj">客户端socket</param>
        private void startRecive(object obj)
        {
            string str;
            string ip;
            string tempstr = string.Empty;
            while (true)
            {

                byte[] buffer = new byte[2048];

                int count;
                try
                {
                    //Receive(Byte[]) 从绑定的 Socket 套接字接收数据，将数据存入接收缓冲区。
                    //该方法执行过后同Accept()方法一样  当前线程会阻塞 等到客户端下一次发来数据时继续执行
                    if (((Socket)obj).Connected == false)
                    {
                        str = ((Socket)obj).RemoteEndPoint.ToString();
                        socketList.Remove(str);
                        return;
                    }
                    count = ((Socket)obj).Receive(buffer);
                    ip = ((Socket)obj).RemoteEndPoint.ToString();
                    if (count == 0)
                    {
                        str = ((Socket)obj).RemoteEndPoint.ToString();
                        socketList.Remove(str);
                        CustomMsg msg = new CustomMsg(1, ((Socket)obj).RemoteEndPoint.ToString(), "已断开连接！");
                        SetMsg(msg);
                        break;
                    }
                    else
                    {
                        str = Encoding.Default.GetString(buffer, 0, count);


                        if (str.IndexOf("<EOF>") == -1)
                        {
                            tempstr += str;
                        }
                        else
                        {
                            tempstr += str;
                            tempstr = tempstr.Replace("<EOF>", "");
                            CustomMsg msg = new CustomMsg(1, ((Socket)obj).RemoteEndPoint.ToString(), tempstr);
                            SetMsg(msg); 
                            tempstr = string.Empty;

                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
  
                    continue;
                    
                }
            }
        }

        public void StartListen(Action<object> RevMsg,int port)
        {
            this.RevMsg = RevMsg;
            //实例化一个Socket对象，确定网络类型、Socket类型、协议类型
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint IEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            //绑定ip和端口
            socket.Bind(IEP);
            //开启监听
            socket.Listen(10);

            Console.WriteLine("开始监听" + "\r\n");

            Thread thread = new Thread(new ParameterizedThreadStart(StartServer));
            thread.IsBackground = true;
            thread.Start(socket);


            #region 该部分实现只适用一个服务器只对应一个客户端

            //Task.Run(() => {

            //        string str;

            //        while (true)
            //        {
            //            //等待接收客户端连接 Accept返回一个用于和该客户端通信的Socket
            //           Socket recviceSocket = socket.Accept();

            //            //Accept()执行过后 当前线程会暂时挂起 只有在有客户端连接时才会继续执行
            //            richTextBox1.Invoke(new Action(() => { richTextBox1.AppendText(recviceSocket.RemoteEndPoint.ToString() + "已连接" + "\r\n"); }));

            //            //开启接收数据的任务
            //            Task.Run(() => {
            //                while (true)
            //                {
            //                    byte[] buffer = new byte[2048];
            //                    int count;
            //                     //Receive(Byte[]) 从绑定的 Socket 套接字接收数据，将数据存入接收缓冲区。
            //                    //该方法执行过后同上  当前线程会暂时挂起 等到客户端下一次发来数据时继续执行
            //                    count = recviceSocket.Receive(buffer);
            //                    if (count == 0)
            //                    {
            //                        richTextBox1.Invoke(new Action(() => { richTextBox1.AppendText(recviceSocket.RemoteEndPoint.ToString() + "已断开连接" + "\r\n"); }));

            //                        break;
            //                    }
            //                    else
            //                    {
            //                        str = Encoding.Default.GetString(buffer, 0, count);
            //                        richTextBox1.Invoke(new Action(() => { richTextBox1.AppendText("收到"+recviceSocket.RemoteEndPoint.ToString()+"数据:" + str + "\r\n"); }));

            //                    }
            //                }


            //            });


            //        }
            //});
            #endregion
        }
        public void StartListen(Action<object> RevMsg, string serverIP, int port)
        {
            this.RevMsg = RevMsg;
            //实例化一个Socket对象，确定网络类型、Socket类型、协议类型
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint IEP = new IPEndPoint(IPAddress.Parse(serverIP), port);
            //绑定ip和端口
            socket.Bind(IEP);
            //开启监听
            socket.Listen(10);

            Console.WriteLine("开始监听" + "\r\n");

            Thread thread = new Thread(new ParameterizedThreadStart(StartServer));
            thread.IsBackground = true;
            thread.Start(socket);
            
        }



        public void Send(string key,string msg)
        {
            byte[] bytes = new byte[2048];
            var smsg = msg + "<EOF>";
            bytes = Encoding.Default.GetBytes(smsg);
            //获取combobox的值 从泛型集合中获取对应的客户端socket 然后发送数据
            if (socketList.Count != 0)
            {
                if (key == null)
                {
                    Console.WriteLine("请选择一个客户端发送数据!");
                    return;
                }
                else
                {
                    socketList[key].Send(bytes);
                }
            }
            else
            {
                Console.WriteLine("当前无连接的客户端" + "\r\n");
            }
        
        }

        public void Stop()
        {

            List<Socket> tempsk = new List<Socket>();

            if (socket.Connected == false)
            {
                foreach (var s in socketList)
                {
                    tempsk.Add(s.Value);
               
                }

                foreach (var t in tempsk)
                {
                    t.Disconnect(true);
                    t.Close();
                }
                socketList.Clear();


            }

            socket.Close();
        }

        private void SetMsg(object str)
        {
            RevMsg.Invoke(str);
        }
    }
}
