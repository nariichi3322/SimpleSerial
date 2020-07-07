namespace SimpleSerial
{
    /// <summary>
    /// Interface of SimpleSerial.
    /// </summary>
    public interface ISimpleSerial
    {
        /// <summary>
        /// Get port list.
        /// </summary>
        /// <param name="portList">[out]Port name array.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool GetPortList(out string[] portList);

        /// <summary>
        /// Initialize serial port.
        /// </summary>
        /// <param name="portName">[in]Name of select port.</param>
        /// <param name="baudRate">[in]Port baud rate.</param>
        void PortInitialize(string portName, int baudRate);

        /// <summary>
        /// Open port.
        /// </summary>
        /// <returns>Bool - Return true for success.</returns>
        bool OpenPort();

        /// <summary>
        /// Close port.
        /// </summary>
        /// <returns>Bool - Return true for success.</returns>
        bool ClosePort();

        /// <summary>
        /// Write data to SerialPort.
        /// </summary>
        /// <param name="data">[in]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool Write(byte data);

        /// <summary>
        /// Read data from SerialPort.
        /// </summary>
        /// <param name="data">[out]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool Read(out byte data);
    }
}
