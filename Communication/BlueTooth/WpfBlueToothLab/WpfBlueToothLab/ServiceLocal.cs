using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluetooth.Services;

namespace WpfBlueToothLab
{
    public static class ServiceLocal
    {
        public static ISenderBluetoothService SenderBluetoothService { get; private set; }
        public static IReceiverBluetoothService ReceiverBluetoothService { get; private set; }

        static ServiceLocal()
        {
            SenderBluetoothService = new SenderBluetoothService();
            ReceiverBluetoothService = new ReceiverBluetoothService();
        }
    }
}
