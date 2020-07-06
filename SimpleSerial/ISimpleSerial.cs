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
        /// <param name="PortList">[out]Port name array.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool GetPortList(out string[] PortList);

        /// <summary>
        /// Initialize serial port.
        /// </summary>
        /// <param name="PortName">[in]Name of select port.</param>
        /// <param name="BaudRate">[in]Port baudrate.</param>
        void PortInitialize(string PortName, int BaudRate);

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
        /// <param name="Data">[in]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool Write(byte Data);

        /// <summary>
        /// Read data from SerialPort.
        /// </summary>
        /// <param name="Data">[out]Data byte.</param>
        /// <returns>Bool - Return true for success.</returns>
        bool Read(out byte Data);
    }
}
