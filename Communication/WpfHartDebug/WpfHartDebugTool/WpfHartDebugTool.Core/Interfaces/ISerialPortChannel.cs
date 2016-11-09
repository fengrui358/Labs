using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHartDebugTool.Core.Interfaces
{
    public interface ISerialPortChannel
    {
        bool IsOpen { get; }

        void Open(string portName, int baudRate);

        void Close();

        void WritePort(byte[] send, int offSet, int count);

        void WritePort(byte[] send);

        event EventHandler<byte[]> DataReceived;        
    }
}
