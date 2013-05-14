using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
    abstract class OscilloSerial : Oscillo
    {
        protected SerialPort serialPort;

        public OscilloSerial(int numChannels) : base(numChannels)
        {
            // Create a new SerialPort object with default settings.
            serialPort = new SerialPort();
        }

        //public abstract bool Open(string portName);

        override public bool IsOpened()
        {
            return serialPort.IsOpen;
        }

        override public bool Close()
        {
            base.Close();
            serialPort.Close();
            return true;
        }

        public void Write(byte[] writeBuffer, int length)
        {
            serialPort.Write(writeBuffer, 0, length);
        }

        public bool Read(byte[] readBuffer, int length)
        {
            if (IsOpened() == false)
            {
                return false;
            }

            int dataread = 0;
            while (dataread < length)
            {
                dataread += serialPort.Read(readBuffer, dataread, length - dataread);
            }
            return true;
        }



    }
}
