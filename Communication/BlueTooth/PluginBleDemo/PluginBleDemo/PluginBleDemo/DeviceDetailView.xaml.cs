using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace PluginBleDemo
{
    public partial class DeviceDetailView : ContentPage
    {
        public DeviceDetailView(IDevice device)
        {
            InitializeComponent();

            BindingContext = new DeviceDetailViewModel(Navigation, device);
        }
    }
}
