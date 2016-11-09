using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHartDebugTool.Core.Interfaces;

namespace SerialPortChannel.Services
{
    public class SerialPortService : ISerialPortService
    {
        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}
