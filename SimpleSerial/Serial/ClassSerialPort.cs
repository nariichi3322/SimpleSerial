using System;
using System.IO;
using System.IO.Ports;

namespace SimpleSerial.Class
{
    /// <summary>
    /// Implement of SimpleSerial.
    /// </summary>
    public class ClassSerialPort : ISimpleSerial
    {
        /// <summary>
        /// ReadQueue object.
        /// </summary>
        private ClassReadQueue ReadQueue { get; } = new ClassReadQueue(256);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClassSerialPort()
        {
            Port.DataReceived += SerialPortDataReceived;
        }

        /// <summary>
        /// Object of class SerialPort.
        /// </summary>
        private SerialPort Port { get; set; } = new SerialPort();

        /// <summary>
        /// Get port list.
        /// </summary>
        /// <param name="PortList">[out]Port name array.</param>
        /// <returns>Bool</returns>
        public bool GetPortList(out string[] PortList)
        {
            // Get a list of serial port names
            PortList = SerialPort.GetPortNames();
            return true;
        }

        /// <summary>
        /// Initialize serial port.
        /// </summary>
        /// <param name="PortName">[in]Name of select port.</param>
        /// <param name="BaudRate">[in]Port baudrate.</param>
        public void PortInitialize(string PortName, int BaudRate)
        {
            Port.PortName = PortName;
            Port.BaudRate = BaudRate;
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
        /// <returns>Bool</returns>
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
                else throw ex;
            }
            return true;
        }

        /// <summary>
        /// Close port.
        /// </summary>
        /// <returns>Bool</returns>
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
                else throw ex;
            }
            return true;
        }

        /// <summary>
        /// Write data to SerialPort.
        /// </summary>
        /// <param name="Data">[in]Data byte.</param>
        /// <returns>Bool</returns>
        public bool Write(byte Data)
        {
            byte[] DataArray = { Data };
            try
            {
                Port.Write(DataArray, 0, 1);
            }
            catch { throw; }
            return true;
        }

        /// <summary>
        /// Read data from SerialPort.
        /// </summary>
        /// <param name="Data">[out]Data byte.</param>
        /// <returns>Bool</returns>
        public bool Read(out byte Data)
        {
            return ReadQueue.Take(out Data);
        }

        /// <summary>
        /// Data received handler.
        /// </summary>
        /// <param name="sender">[in]</param>
        /// <param name="args">[in]</param>
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                int Count = Port.BytesToRead;
                byte[] Bytes = new byte[Count];
                Port.Read(Bytes, 0, Count);
                foreach (byte Data in Bytes)
                {
                    ReadQueue.Add(Data);
                }
            }
            catch { throw; }
        }
    }
}
