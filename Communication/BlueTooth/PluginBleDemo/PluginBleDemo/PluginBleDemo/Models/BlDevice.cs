using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Plugin.BLE.Abstractions.Contracts;

namespace PluginBleDemo.Models
{
    public class BlDevice : ObservableObject
    {
        private readonly IDevice _device;

        public string Name => _device.Name;

        public Guid Id => _device.Id;

        public BlDevice(IDevice device)
        {
            _device = device;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
