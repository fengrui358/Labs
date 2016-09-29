using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Bluetooth.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using InTheHand.Net.Sockets;
using NLog;

namespace WpfBlueToothLab
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ILogger _logger;

        private Thread _discoverThread;

        private ObservableCollection<Device> _slaveDevices;

        private string _statusMsg;

        private string _sendStatus;

        private ObservableCollection<string> _receiveMsgs;

        private Device _selectedDevice;

        private string _toBeSendMsg;

        public ObservableCollection<Device> SlaveDevices
        {
            get { return _slaveDevices; }
            set { Set(() => SlaveDevices, ref _slaveDevices, value); }
        }

        public string StatusMsg
        {
            get { return _statusMsg; }
            set { Set(() => StatusMsg, ref _statusMsg, value); }
        }

        public string SendStatus
        {
            get { return _sendStatus; }
            set { Set(() => SendStatus, ref _sendStatus, value); }
        }

        public ObservableCollection<string> ReceiveMsgs
        {
            get { return _receiveMsgs; }
            set { Set(() => ReceiveMsgs, ref _receiveMsgs, value); }
        }

        public Device SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
                //todo:丢给选中设备面板处理
            }
        }

        /// <summary>
        /// 待发送的信息
        /// </summary>
        public string ToBeSendMsg
        {
            get { return _toBeSendMsg; }
            set { _toBeSendMsg = value; }
        }

        public RelayCommand SendCommand { get; private set; }

        public MainWindowViewModel()
        {
            DispatcherHelper.Initialize();

            SlaveDevices = new ObservableCollection<Device>();
            ReceiveMsgs = new ObservableCollection<string>();
            _logger = LogManager.GetCurrentClassLogger();

            SendCommand = new RelayCommand(Send, () => SelectedDevice != null && !string.IsNullOrEmpty(ToBeSendMsg));

            _discoverThread = new Thread(Discover) {IsBackground = true};
            _discoverThread.Start();
        }

        private void Discover()
        {
            while (true)
            {
                try
                {
                    StatusMsg = "开始搜索……";
                    var sw = Stopwatch.StartNew();

                    var devices = ServiceLocal.SenderBluetoothService.GetDevices().Result;

                    sw.Stop();
                    StatusMsg = $"搜索结束，本次耗时{sw.ElapsedMilliseconds}毫秒，共发现{devices.Count}个设备";

                    UpdateDevicesCollection(devices);

                    //开启监听
                    if (!ServiceLocal.ReceiverBluetoothService.WasStarted)
                    {
                        ServiceLocal.ReceiverBluetoothService.Start(NewReceiveHandler);
                    }
                }
                catch (PlatformNotSupportedException ex)
                {
                    StatusMsg = ex.Message;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is PlatformNotSupportedException)
                    {
                        StatusMsg = ex.InnerException.Message;
                    }
                    else
                    {
                        StatusMsg = ex.Message;
                    }
                }
                finally
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private void UpdateDevicesCollection(IEnumerable<Device> newDevices)
        {
            if (newDevices != null && newDevices.Any())
            {
                var toBeAdds = newDevices.Except(_slaveDevices, new DeviceEqualityComparer<Device>());

                if (toBeAdds.Any())
                {
                    DispatcherHelper.UIDispatcher.Invoke(() =>
                    {
                        foreach (var device in toBeAdds)
                        {
                            SlaveDevices.Add(device);
                        }
                    });
                }
            }
        }

        private async void Send()
        {
            var result = await ServiceLocal.SenderBluetoothService.Send(SelectedDevice, ToBeSendMsg);
            SendStatus = result ? $"信息:{ToBeSendMsg},发送成功,时间{DateTime.Now}" : $"信息:{ToBeSendMsg}发送失败,时间{DateTime.Now}";
        }

        private void NewReceiveHandler(string content)
        {
            _logger.Info("接收到信息：{0}", content);
            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                ReceiveMsgs.Add(content);
            });
        }
    }
}
