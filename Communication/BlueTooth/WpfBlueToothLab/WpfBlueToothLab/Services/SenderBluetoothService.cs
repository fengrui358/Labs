using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bluetooth.Model;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using NLog;
using WpfBlueToothLab.Helper;

namespace Bluetooth.Services
{
    /// <summary>
    /// Define the sender Bluetooth service.
    /// </summary>
    public sealed class SenderBluetoothService : ISenderBluetoothService
    {
        private Guid _serviceClassId
        {
            get { return BlueToothHelper.ServiceClassId; }
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <returns>The list of the devices.</returns>
        public async Task<IList<Device>> GetDevices()
        {
            // for not block the UI it will run in a different threat
            var task = Task.Run(() =>
            {
                var devices = new List<Device>();
                using (var bluetoothClient = new BluetoothClient())
                {
                    var array = bluetoothClient.DiscoverDevices();
                    var count = array.Length;
                    for (var i = 0; i < count; i++)
                    {
                        devices.Add(new Device(array[i]));
                    }
                }
                return devices;
            });
            return await task;
        }

        /// <summary>
        /// Sends the data to the Receiver.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        /// <returns>If was sent or not.</returns>
        public async Task<bool> Send(Device device, string content)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            // for not block the UI it will run in a different threat
            var task = Task.Run(() =>
            {
                using (var bluetoothClient = new BluetoothClient())
                {
                    try
                    {
                        var ep = new BluetoothEndPoint(device.DeviceInfo.DeviceAddress, _serviceClassId);

                        // connecting
                        bluetoothClient.Connect(ep);

                        // get stream for send the data
                        var bluetoothStream = bluetoothClient.GetStream();

                        // if all is ok to send
                        if (bluetoothClient.Connected && bluetoothStream != null)
                        {
                            // write the data in the stream
                            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                            bluetoothStream.Write(buffer, 0, buffer.Length);
                            bluetoothStream.Flush();
                            bluetoothStream.Close();
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        // the error will be ignored and the send data will report as not sent
                        // for understood the type of the error, handle the exception
                        LogManager.GetCurrentClassLogger().Error(ex);
                    }
                }
                return false;
            });
            return await task;
        }
    }
}
