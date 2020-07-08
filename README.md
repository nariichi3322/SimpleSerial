# SimpleSerial
Simple serial port handler.

## Intro

A simple serial port handler with receive buffer, received data will be stored in queue until user is taking out them.

## Usage

```C#
using SimpleSerial;
using SimpleSerial.Serial;

class Program
{
    static void main()
    {
        /* New simple serial class with buffer size 256 */
        ISimpleSerial Serial = new ClassSerialPort(256);
        /* Get port list */
        Serial.GetPortList(out string[] portList);
        /* Initialize port with port name and baud rate */
        Serial.PortInitialize(portList[0], 115200);
        /* Open port */
        Serial.OpenPort();
        /* Write a data byte to port */
        Serial.Write(0x30);
        /* Read a data byte from port */
        Serial.Read(out byte readData);
        /* Close port */
        Serial.ClosePort();
    }
}
```
