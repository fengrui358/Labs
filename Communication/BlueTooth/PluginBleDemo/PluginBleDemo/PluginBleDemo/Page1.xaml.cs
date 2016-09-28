using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace PluginBleDemo
{
    public partial class Page1 : ContentPage
    {
        private IBluetoothLE _ble;
        private IAdapter _adapter;

        private string _bleState;
        private bool _isOn;
        private bool _isAvailable;

        public string BleState
        {
            get { return _bleState; }
            set
            {
                _bleState = value;
                OnPropertyChanged();
            }
        }

        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }

        public Page1()
        {
            InitializeComponent();

            BindingContext = this;

            _ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;

            _ble.StateChanged += (sender, args) =>
            {
                BleState = ((IBluetoothLE)sender).State.ToString();
                IsOn = ((IBluetoothLE)sender).IsOn;
                IsAvailable = ((IBluetoothLE)sender).IsAvailable;
            };

            BleState = _ble.State.ToString();
            IsOn = _ble.IsOn;
            IsAvailable = _ble.IsAvailable;
        }
    }
}
