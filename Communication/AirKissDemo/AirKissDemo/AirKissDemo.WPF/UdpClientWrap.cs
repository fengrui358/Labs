﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AirKissDemo.Core;

namespace AirKissDemo.UDP
{
    public class UdpClientWrap : IUdpClient
    {
        private readonly byte[] _dummyData = new byte[1500];
        private UdpClient _udpClient;

        public int SleepingTime { get; set; } = 4;

        public int Port { get; set; } = 7788;

        /// <summary>
        /// 是否启动
        /// </summary>
        public bool Start { get; private set; }

        public void StartSend(int[] data)
        {
            if (Start)
            {
                return;
            }

            Start = true;

            //启动发送线程发送数据
            new Thread(() =>
            {
                _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
                var index = 1;

                while (true)
                {
                    try
                    {
                        if (!Start)
                        {
                            break;
                        }

                        Debug.WriteLine($"开始第{index}轮发送数据，{DateTime.Now}");
                        StatusEvent?.Invoke(this, $"开始第{index}轮发送数据，{DateTime.Now}");

                        for (int i = 0; i < data.Length; i++)
                        {
                            SendPacketAndSleep(data[i]);
                            if (i%200 == 0)
                            {
                                if (!Start)
                                {
                                    break;
                                }
                            }

                            Thread.Sleep(SleepingTime);
                        }

                        Debug.WriteLine($"第{index}轮数据发送完毕，{DateTime.Now}");
                        StatusEvent?.Invoke(this, $"第{index}轮数据发送完毕，{DateTime.Now}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    finally
                    {
                        index++;
                        Thread.Sleep(500);
                    }
                }
            })
            { IsBackground = true }.Start();
        }

        public void Stop()
        {
            Start = false;
        }

        public event EventHandler<string> StatusEvent;

        private void SendPacketAndSleep(int length)
        {
            try
            {
                _udpClient?.Send(_dummyData, length, "255.255.255.255", 7788);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
