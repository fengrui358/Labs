using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirKissLib;

namespace AirKissDemo.WPF
{
    class AirKissTask
    {
        private char _randomChar;
        private AirKissEncoder _airKissEncoder;
        private const int ReplyByteConfirmTimes = 5; //收到随机码的确认信息可表示配置成功
        private const int Port = 10000;

        private volatile bool _done; //是否配置完成

        private readonly byte[] _dummyData = new byte[1500];

        private UdpClient _sendUdpClient;

        public AirKissTask(AirKissEncoder airKissEncoder)
        {
            _randomChar = airKissEncoder.GetRandomChar();
            _airKissEncoder = airKissEncoder;
        }

        public void Execute()
        {
            //启动监听线程监听是否配置成功
            new Thread(() =>
            {
                var buffer = new byte[15000];
                try
                {
                    int replyByteCounter = 0;

                    var socketServer = new Socket(SocketType.Dgram, ProtocolType.Udp);
                    socketServer.Bind(new IPEndPoint(IPAddress.Any, Port));
                    socketServer.SendTimeout = 1000;

                    while (true)
                    {
                        //todo:可主动停止

                        try
                        {
                            socketServer.Receive(buffer);
                            foreach (var b in buffer)
                            {
                                if (b == _randomChar)
                                {
                                    replyByteCounter++;
                                }
                            }

                            if (replyByteCounter > ReplyByteConfirmTimes)
                            {
                                _done = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }

                    socketServer.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }) {IsBackground = true}.Start();

            //启动发送线程发送数据
            new Thread(() =>
            {
                _sendUdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
                var encodedData = _airKissEncoder.GetEncodedData();

                for (int i = 0; i < encodedData.Length; ++i)
                {
                    SendPacketAndSleep(encodedData[i]);
                    if (i % 200 == 0)
                    {
                        if (isCancelled() || _done) //todo:手动停止
                        {
                            break;
                        }
                    }
                }

            })
            {IsBackground = true}.Start();
        }

        private void SendPacketAndSleep(int length)
        {
            try
            {
                _sendUdpClient.Send(_dummyData, length, "255.255.255.255", 7788);

                Thread.Sleep(4);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
