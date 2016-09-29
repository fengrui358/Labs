using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace PluginBleDemo
{
    public class DeviceDetailViewModel : ViewModelBase
    {
        private INavigation _navigation;

        private IDevice _device;

        public DeviceDetailViewModel(INavigation navigation, IDevice device)
        {
            _navigation = navigation;
            _device = device;
        }
    }
}
