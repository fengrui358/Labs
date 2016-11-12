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
        private readonly char _randomChar;
        private readonly AirKissEncoder _airKissEncoder;
        private const int ReplyByteConfirmTimes = 5; //收到随机码的确认信息可表示配置成功
        private const int Port = 10000;

        private volatile bool _isCancel = true; //是否取消
        private volatile bool _done; //是否配置完成

        private readonly byte[] _dummyData = new byte[1500];

        private UdpClient _sendUdpClient;
        private Socket _socketServer;

        public AirKissTask(AirKissEncoder airKissEncoder)
        {
            _randomChar = airKissEncoder.GetRandomChar();
            _airKissEncoder = airKissEncoder;
        }

        public void Execute()
        {
            _isCancel = false;

            //启动监听线程监听是否配置成功
            new Thread(() =>
            {
                var buffer = new byte[15000];
                try
                {
                    int replyByteCounter = 0;

                    _socketServer = new Socket(SocketType.Dgram, ProtocolType.Udp);
                    _socketServer.Bind(new IPEndPoint(IPAddress.Any, Port));

                    while (true)
                    {
                        if (_isCancel)
                        {
                            break;
                        }

                        try
                        {
                            _socketServer.Receive(buffer);
                            foreach (var b in buffer)
                            {
                                if (b == _randomChar)
                                {
                                    replyByteCounter++;
                                }
                            }

                            if (replyByteCounter > ReplyByteConfirmTimes)
                            {
                                Debug.WriteLine("设置成功");

                                _done = true;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }

                    _socketServer.Close();
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

                while (true)
                {
                    if (_isCancel || _done)
                    {
                        break;
                    }

                    Debug.WriteLine("开始发送数据" + DateTime.Now);

                    for (int i = 0; i < encodedData.Length; i++)
                    {
                        SendPacketAndSleep(encodedData[i]);
                        if (i%200 == 0)
                        {
                            if (_isCancel || _done)
                            {
                                break;
                            }
                        }
                    }

                    Debug.WriteLine("数据发送完毕" + DateTime.Now);
                }
            })
            {IsBackground = true}.Start();
        }

        public void Stop()
        {
            _isCancel = true;
            
            _socketServer?.Close();
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
