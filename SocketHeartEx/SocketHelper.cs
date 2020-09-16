using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;


namespace Common.SocketExtend
{
    #region 文件传送
    /// <summary>
    /// 文件传送类
    /// </summary>
    class TransferFiles
    {
        public static int SendData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            while (total < size)
            {
                sent = s.Send(data, total, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }

            return total;
        }

        public static byte[] ReceiveData(Socket s, int size)
        {
            int total = 0;
            int dataleft = size;
            byte[] data = new byte[size];
            int recv;
            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    recv = s.Receive(data, total, dataleft, SocketFlags.None);
                    if (recv == 0)
                    {
                        data = null;
                        break;
                    }

                    total += recv;
                    dataleft -= recv;
                }
            }
            return data;
        }

        public static int SendVarData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];

           
            try
            {
                datasize = BitConverter.GetBytes(size);
                sent = s.Send(datasize);

                while (total < size)
                {
                    sent = s.Send(data, total, dataleft, SocketFlags.None);
                    total += sent;
                    dataleft -= sent;
                }

                return total;
            }
            catch
            {

                return 3;
            }
        }

        public static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = s.Receive(datasize, 0, 4, SocketFlags.None);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];

            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }

                total += recv;
                dataleft -= recv;
            }

            return data;

        }
    }
    public static class FileServer
    {
        private static Socket serverSocket;
        public static void Init(string ipaddr, int port)
        {
            //服务器IP地址
            IPAddress ip = IPAddress.Parse(ipaddr);
            int myProt = Convert.ToInt32(port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //通过ClientSocket发送数据
            Thread mythread = new Thread(ListenClientConnect);
            mythread.Start();

        }

        public static void Exit()
        {
            serverSocket.Close();
            serverSocket = null;
        }
        private static void ListenClientConnect()
        {
            while (true)
            {
                if (serverSocket != null)
                {
                    try
                    {
                        Socket clientSocket = serverSocket.Accept();
                        Thread receiveThread = new Thread(Create);
                        receiveThread.Start(clientSocket);
                    }
                    catch
                    {

                        break;
                    }
                }
            }
        }

        private static void Create(object clientSocket)
        {
            Socket client = clientSocket as Socket;
            //锋利客户端节点对象
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;




            //获得【文件名】
            string SendFileName = Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));

            if (SendFileName == string.Empty)
                return;



            //获得【包大小】
            string bagSize = Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            if (bagSize == string.Empty)
                return;
            //获得[包的总数量]   

            int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client)));

            //获得[最后一个包的大小]   

            string bagLast = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            string fullPath = Path.Combine(Environment.CurrentDirectory, SendFileName);

            //创建一个新文件   
            FileStream MyFileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

            //已发送包的个数   
            int SendedCount = 0;

            while (true)
            {
                byte[] data = TransferFiles.ReceiveVarData(client);
                if (data.Length == 0)
                {
                    break;
                }
                else
                {
                    SendedCount++;                                  //将接收到的数据包写入到文件流对象   
                    MyFileStream.Write(data, 0, data.Length);       //显示已发送包的个数     
                }

            }

            //关闭文件流   

            MyFileStream.Close();

            //关闭套接字   

            client.Close();

            Console.WriteLine(SendFileName + "接收完毕！");

        }


    }
    public static class FileClient
    {
        public static bool SendFile(string IP, int Port, string fullPath)
        {
            //创建一个文件对象
            FileInfo EzoneFile = new FileInfo(fullPath);
            //打开文件流
            FileStream EzoneStream = EzoneFile.OpenRead();

            //包的大小
            int PacketSize = 10000;

            //包的数量
            int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));

            //最后一个包的大小
            int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));

            //指向远程服务端节点
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IP), Port);

            //创建套接字
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //连接到发送端
            try
            {
                client.Connect(ipep);
            }
            catch
            {
                Debug.WriteLine("连接服务器失败！");
                return false;
            }

            //获得客户端节点对象
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

            //发送[文件名]到客户端
            TransferFiles.SendVarData(client, System.Text.Encoding.Unicode.GetBytes(EzoneFile.Name));

            //发送[包的大小]到客户端
            TransferFiles.SendVarData(client, System.Text.Encoding.Unicode.GetBytes(PacketSize.ToString()));

            //发送[包的总数量]到客户端
            TransferFiles.SendVarData(client, System.Text.Encoding.Unicode.GetBytes(PacketCount.ToString()));

            //发送[最后一个包的大小]到客户端
            TransferFiles.SendVarData(client, System.Text.Encoding.Unicode.GetBytes(LastDataPacket.ToString()));

            bool isCut = false;

            //数据包
            byte[] data = new byte[PacketSize];

            //开始循环发送数据包
            for (int i = 0; i < PacketCount; i++)
            {

                //从文件流读取数据并填充数据包
                EzoneStream.Read(data, 0, data.Length);

                //发送数据包
                if (TransferFiles.SendVarData(client, data) == 3)
                {
                    isCut = true;
                    return false;
          
                }
            }

            //如果还有多余的数据包，则应该发送完毕！
            if (LastDataPacket != 0)
            {
                data = new byte[LastDataPacket];
                EzoneStream.Read(data, 0, data.Length);
                TransferFiles.SendVarData(client, data);
            }

            //关闭套接字
            client.Close();
            //关闭文件流
            EzoneStream.Close();

            if (!isCut)
            {
                return true;
            }
            return false;
        }
    }
    #endregion

    #region 通用/其它
    public class Network
    {
        /// <summary>
        /// 当前主机是否在线
        /// </summary>
        /// <param name="Ipaddr"></param>
        /// <returns></returns>
        public static bool PingHost(string Ipaddr)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(Ipaddr);
            if (pingReply.Status == IPStatus.Success)
            {
                Console.WriteLine("Ping :" + Ipaddr.ToString() + ",当前在线!");
                return true;
            }
            else
            {
                Console.WriteLine("Ping :" + Ipaddr.ToString() + ",当前离线!");
                return false;
            }
        }
    }
    #endregion

    //Socket通讯 带心跳包
    //参考https://www.cnblogs.com/liyijin/p/8733453.html

    #region 其它
    /// <summary>
    /// 自定义消息类
    /// </summary>
    public class CustomMsg
    {
        public CustomMsg(int msgType, string remoteEndPoint, string msg)
        {
            this.msgType = msgType;
            this.remoteEndPoint = remoteEndPoint;
            this.msg = msg;
        }
        /// <summary>
        /// 0为客户端连接成功，1为客户端断开,2为客户端重连成功,3为接收到客户端消息
        /// </summary>
        public int msgType { get; set; }

        public string remoteEndPoint { get; set; }

        public string msg { get; set; }
    }
    #endregion


}
