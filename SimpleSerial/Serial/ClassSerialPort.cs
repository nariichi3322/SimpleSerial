using System;
using System.IO;
using System.IO.Ports;

namespace SimpleSerial.Serial
{
    /// <summary>
    /// Implement of SimpleSerial.
    /// </summary>
    public class ClassSerialPort : ISimpleSerial
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="queueSize">[in]Queue size.</param>
        public ClassSerialPort(int queueSize = 256)
        {
            Port.DataReceived += SerialPortDataReceived;
            ReadQueue = new ClassReadQueue(queueSize);
        }

        /// <summary>
        /// ReadQueue object.
        /// </summary>
        private ClassReadQueue ReadQueue { get; }

        /// <summary>
        /// Object of class SerialPort.
        /// </summary>
        private SerialPort Port { get; } = new SerialPort();

        /// <summary>
        /// Get port list.
        /// </summary>
        /// <param name="portList">[out]Port name array.</param>
        /// <returns>Bool - Return true for success.</returns>
        public bool GetPortList(out string[] portList)
        {
            // Get a list of serial port names
            portList = SerialPort.GetPortNames();
            return true;
        }

        /// <summary>
        /// Initialize serial port.
        /// </summary>
        /// <param name="portName">[in]Name of select port.</param>
        /// <param name="baudRate">[in]Port baud rate.</param>
        public void PortInitialize(string portName, int baudRate)
        {
            Port.PortName = portName;
            Port.BaudRate = baudRate;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Handshake = Handshake.None;
            Port.ReadTimeout = 500;
            Port.WriteTimeout = 500;
        }

        /// <summary>
        /// Open port.
        /// </summary>
        /// <returns>Bool - Return true for success.</returns>
        public bool OpenPort()
        {
            try
            {
                Port.Open();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is UnauthorizedAccessException || ex is IOException)
                {
                    return false;
                }
                throw;
            }
            return true;
        }

        /// <summary>
        /// Close port.
        /// </summary>
        /// <returns>Bool - Return true for success.</returns>
        public bool ClosePort()
        {
            try
            {
                Port.Close();
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException)
                {
                    return false;
                }
                throw;
            }
            return true;
        }

        /// <summary>
        /// Write data to SerialPort.
        /// </summary>
        /// <param name="data">[in]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        public bool Write(byte data)
        {
            byte[] dataArray = { data };
            Port.Write(dataArray, 0, 1);
            return true;
        }

        /// <summary>
        /// Read data from SerialPort.
        /// </summary>
        /// <param name="data">[out]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        public bool Read(out byte data)
        {
            return ReadQueue.Take(out data);
        }

        /// <summary>
        /// Data received handler.
        /// </summary>
        /// <param name="sender">[in]</param>
        /// <param name="args">[in]</param>
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            var count = Port.BytesToRead;
            var bytes = new byte[count];
            Port.Read(bytes, 0, count);
            foreach (var data in bytes)
            {
                ReadQueue.Add(data);
            }
        }
    }
}