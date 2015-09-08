using System.IO.Ports;

namespace XOscillo
{
    abstract class OscilloSerial : Oscillo
    {
        protected SerialPort SerialPort;

        public OscilloSerial(int numChannels) : base(numChannels)
        {
            // Create a new SerialPort object with default settings.
            SerialPort = new SerialPort();
        }

        //public abstract bool Open(string portName);

        override public bool IsOpened()
        {
            return SerialPort.IsOpen;
        }

        override public bool Close()
        {
            base.Close();
            SerialPort.Close();
            return true;
        }

        public void Write(byte[] writeBuffer, int length)
        {
            SerialPort.Write(writeBuffer, 0, length);
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
                dataread += SerialPort.Read(readBuffer, dataread, length - dataread);
            }
            return true;
        }



    }
}
