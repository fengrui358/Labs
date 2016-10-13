using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfHartDebugTool.Core.Interfaces;

namespace SerialPortChannel
{
    public class SerialPortChannel : ISerialPortChannel
    {
        public event EventHandler<byte[]> DataReceived;

        private SerialPort _serialPort;
        private Thread _thread;
        public volatile bool KeepReading;

        public SerialPortChannel()
        {
            _serialPort = new SerialPort();
            _serialPort.ErrorReceived += SerialPortOnErrorReceived;
            _serialPort.DataReceived += SerialPortOnDataReceived;
            _thread = null;
            KeepReading = false;
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            if (_serialPort.IsOpen)
            {
                int count = _serialPort.BytesToRead;
                if (count > 0)
                {
                    byte[] readBuffer = new byte[count];
                    try
                    {
                        //MediaTypeNames.Application.DoEvents();
                        _serialPort.Read(readBuffer, 0, count);
                        DataReceived?.Invoke(this, readBuffer);

                        Thread.Sleep(100);
                    }
                    catch (TimeoutException)
                    {
                    }
                }
            }
        }

        private void SerialPortOnErrorReceived(object sender, SerialErrorReceivedEventArgs serialErrorReceivedEventArgs)
        {
            throw new NotImplementedException();
        }

        public bool IsOpen
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }

        //private void StartReading()
        //{
        //    if (!KeepReading)
        //    {
        //        KeepReading = true;
        //        _thread = new Thread(new ThreadStart(ReadPort));
        //        _thread.Start();
        //    }
        //}

        //private void StopReading()
        //{
        //    if (KeepReading)
        //    {
        //        KeepReading = false;
        //        _thread.Join();
        //        _thread = null;
        //    }
        //}

        private void ReadPort()
        {
            while (KeepReading)
            {
                if (_serialPort.IsOpen)
                {
                    int count = _serialPort.BytesToRead;
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        try
                        {
                            //MediaTypeNames.Application.DoEvents();
                            _serialPort.Read(readBuffer, 0, count);
                            if (DataReceived != null)
                                //DataReceived(readBuffer);
                            Thread.Sleep(100);
                        }
                        catch (TimeoutException)
                        {
                        }
                    }
                }
            }
        }

        public void Open(string portName, int baudRate)
        {
            Close();
                        
            _serialPort = new SerialPort(portName, baudRate, Parity.Odd);
            _serialPort.ErrorReceived += SerialPortOnErrorReceived;
            _serialPort.DataReceived += SerialPortOnDataReceived;

            _serialPort.Open();
            if (_serialPort.IsOpen)
            {
                //StartReading();
            }
            else
            {
                //MessageBox.Show("串口打开失败！");
            }
        }

        public void Close()
        {
            //StopReading();
            _serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                _serialPort.Write(send, offSet, count);
            }
        }

        public void WritePort(byte[] send)
        {
            WritePort(send, 0, send.Length);
        }        
    }
}
