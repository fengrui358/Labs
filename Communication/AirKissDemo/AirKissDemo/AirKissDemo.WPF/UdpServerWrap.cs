using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using AirKissDemo.Core;

namespace AirKissDemo.UDP
{
    public class UdpServerWrap : IUdpServer
    {
        private UdpClient _udpClient;

        public int Port { get; set; } = 10000;

        public bool Start { get; private set; }

        public void StartListening()
        {
            if (Start)
            {
                return;
            }

            _udpClient = new UdpClient(Port);
            var ipEndPoint = new IPEndPoint(IPAddress.Any, Port);
            Start = true;

            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var result = _udpClient.Receive(ref ipEndPoint);

                        Debug.WriteLine($"接收到数据:{Encoding.UTF8.GetString(result)}");
                        StatusEvent?.Invoke(this, $"接收到数据:{Encoding.UTF8.GetString(result)}");

                        NewDataReceiveEvent?.Invoke(this, result);

                        if (!Start)
                        {
                            _udpClient.Close();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        _udpClient.Close();
                    }
                }

            }) {IsBackground = true}.Start();
        }

        public void StopListening()
        {
            Start = false;
        }

        public event EventHandler<byte[]> NewDataReceiveEvent;
        public event EventHandler<string> StatusEvent;
    }
}
