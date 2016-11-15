using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirKissLib;
using MvvmCross.Core.ViewModels;

namespace AirKissDemo.Core
{
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IUdpServer _udpServer;
        private readonly IUdpClient _udpClient;
        private readonly IWlanClient _wlanClient;

        private const int ReplyByteConfirmTimes = 5; //收到随机码的确认信息可表示配置成功的阀值次数
        private char _randomChar;

        private int _replyByteCounter; //收到随机码匹配成功的次数

        private bool _startConfig;

        private int _frameInterval = 8; //发包帧间隔默认8ms

        /// <summary>
        /// 工作状态
        /// </summary>
        private string _status;

        /// <summary>
        /// 工作状态
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        /// <summary>
        /// 当前连接Wifi的SSID
        /// </summary>
        public string SSID { get; set; }

        /// <summary>
        /// 当前连接Wifi的密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 发包帧间隔
        /// </summary>
        public int FrameInterval
        {
            get { return _frameInterval; }
            set { SetProperty(ref _frameInterval, value); }
        }

        /// <summary>
        /// 是否启动配置
        /// </summary>
        public bool StartConfig
        {
            get { return _startConfig; }
            set { SetProperty(ref _startConfig, value); }
        }

        public MainPageViewModel(IUdpServer udpServer, IUdpClient udpClient ,IWlanClient wlanClient)
        {
#if DEBUG

            SSID = "testwifi";
            PassWord = "testwifi";

#endif
            _udpServer = udpServer;
            _udpClient = udpClient;
            _wlanClient = wlanClient;

            _udpServer.NewDataReceiveEvent += UdpServerOnNewDataReceiveEvent;
            _udpClient.StatusEvent += UdpClientOnStatusEvent;

            StartCommand = new MvxCommand(StartCommandHander);
            StopCommand = new MvxCommand(StopCommandHander);

            ShouldAlwaysRaiseInpcOnUserInterfaceThread(false); //Mvx需要关闭必须UI线程刷新

            TrySetSSID();
        }

        private void UdpClientOnStatusEvent(object sender, string s)
        {
            Status = s;
        }

        private void UdpServerOnNewDataReceiveEvent(object sender, byte[] bytes)
        {
            foreach (var b in bytes)
            {
                if (b == _randomChar)
                {
                    _replyByteCounter++;
                }
            }

            if (_replyByteCounter > ReplyByteConfirmTimes)
            {
                try
                {
                    _udpServer.StopListening();
                    _udpClient.Stop();

                    StartConfig = _udpServer.Start && _udpClient.Start;

                    Task.Delay(200).Wait();

                    Debug.WriteLine("设置成功，" + DateTime.Now);
                    Status = "设置成功，" + DateTime.Now;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public MvxCommand StartCommand { get; }
        public MvxCommand StopCommand { get; }

        private void StartCommandHander()
        {
            if (!_udpServer.Start && !_udpClient.Start)
            {
                var airKissEncoder = new AirKissEncoder(SSID ?? string.Empty, PassWord ?? string.Empty);
                _randomChar = airKissEncoder.GetRandomChar();

                _replyByteCounter = 0;

                _udpClient.SleepingTime = _frameInterval > 0 ? _frameInterval : 1;
                _udpServer.StartListening();
                _udpClient.StartSend(airKissEncoder.GetEncodedData());

                StartConfig = _udpServer.Start && _udpClient.Start;
            }
        }

        private void StopCommandHander()
        {
            if (_udpServer.Start && _udpClient.Start)
            {
                _udpServer.StopListening();
                _udpClient.Stop();

                StartConfig = _udpServer.Start && _udpClient.Start;
            }
        }

        /// <summary>
        /// 尝试设置当前的SSID
        /// </summary>
        private void TrySetSSID()
        {
            var ssid = _wlanClient?.GetCurrentWifiSSID();
            if (!string.IsNullOrEmpty(ssid))
            {
                SSID = ssid;
            }
        }
    }
}
