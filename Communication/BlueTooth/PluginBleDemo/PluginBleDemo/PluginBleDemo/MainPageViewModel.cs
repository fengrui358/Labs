using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using PluginBleDemo.Models;
using Xamarin.Forms;

namespace PluginBleDemo
{
    public class MainPageViewModel : ViewModelBase
    {
        #region 字段

        private IBluetoothLE _ble;

        private string _bleState;
        private bool _isOn;
        private bool _isAvailable;

        private string _scanStatus;

        private ObservableCollection<BlDevice> _devices;
        private INavigation _navigation;
        private bool _isScanning;

        #endregion

        #region 属性

        public string BleState
        {
            get { return _bleState; }
            set { Set(() => BleState, ref _bleState, value); }
        }

        public bool IsOn
        {
            get { return _isOn; }
            set { Set(() => IsOn, ref _isOn, value); }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { Set(() => IsAvailable, ref _isAvailable, value); }
        }

        public string ScanStatus
        {
            get { return _scanStatus; }
            set { Set(() => ScanStatus, ref _scanStatus, value); }
        }

        public ObservableCollection<BlDevice> Devices
        {
            get { return _devices; }
            set { Set(() => Devices, ref _devices, value); }
        }

        /// <summary>
        /// 是否正在扫描
        /// </summary>
        public bool IsScanning
        {
            get { return _isScanning; }
            private set
            {
                if (_isScanning != value)
                {
                    _isScanning = value;
                    RaisePropertyChanged();

                    StartScanCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 命令

        public RelayCommand StartScanCommand { get; private set; }

        #endregion

        #region 构造

        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _devices = new ObservableCollection<BlDevice>();

            _ble = CrossBluetoothLE.Current;

            _ble.StateChanged += (sender, args) =>
            {
                BleState = ((IBluetoothLE) sender).State.ToString();
                IsOn = ((IBluetoothLE) sender).IsOn;
                IsAvailable = ((IBluetoothLE) sender).IsAvailable;
            };

            _ble.Adapter.DeviceDiscovered += AdapterOnDeviceDiscovered;

            BleState = _ble.State.ToString();
            IsOn = _ble.IsOn;
            IsAvailable = _ble.IsAvailable;

            //启动扫描线程
            StartScanCommand = new RelayCommand(StartScan, () => !IsScanning);
        }

        #endregion

        #region 公有方法

        public async void ItemTap(IDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            await _navigation.PushAsync(new DeviceDetailView(device));
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 发现设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="deviceEventArgs"></param>
        private void AdapterOnDeviceDiscovered(object sender, DeviceEventArgs deviceEventArgs)
        {
            var exist =
                Devices.FirstOrDefault(s => s.Id == deviceEventArgs.Device.Id && s.Name == deviceEventArgs.Device.Name);

            if (exist == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Devices.Add(new BlDevice(deviceEventArgs.Device));
                });
            }
        }

        private async void StartScan()
        {
            try
            {
                if (!_ble.Adapter.IsScanning && _ble.IsAvailable && _ble.IsOn)
                {
                    IsScanning = true;

                    var sw = Stopwatch.StartNew();
                    ScanStatus = "开始扫描……";

                    await _ble.Adapter.StartScanningForDevicesAsync();

                    sw.Stop();
                    ScanStatus = $"一轮扫描结束，停止扫描，用时：{sw.ElapsedMilliseconds}毫秒";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsScanning = false;
            }
        }

        #endregion
    }
}
