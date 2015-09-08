using System;
using System.IO.Ports;
using System.Threading;

namespace XOscillo.VizForms.Arduino
{
    enum COMMANDS
    {
        IDLE = 0,
        RESET = 175,
        PING = 63,
        READ_ADC_TRACE = 170,
        READ_BIN_TRACE = 171
    }

    class SerialArduino : OscilloSerial
    {
        protected byte m_triggerValue = 127;
        int m_baudrate;


        public SerialArduino(int baudrate, int samplerate, int numChannels)
            : base(numChannels)
        {
            // Create a new SerialPort object with default settings.
            SerialPort = new SerialPort();

            m_baudrate = baudrate;
            SetSampleRate(samplerate);
        }

        override public string GetName()
        {
            return "Arduino";
        }

        override public bool Open(string portName)
        {
            string os = Environment.OSVersion.Platform.ToString();

            try
            {
                SerialPort = new SerialPort(portName, m_baudrate, Parity.None, 8, StopBits.One);
                SerialPort.Handshake = Handshake.None;
                SerialPort.ReadBufferSize = 1024 * 10;

                DebugConsole.Instance.Add(portName + ", rts:" + SerialPort.RtsEnable.ToString() + ", dtr:" + SerialPort.DtrEnable.ToString() + "   trying...");
                SerialPort.Open();
                //DebugConsole.Instance.Add(" rts:" + serialPort.RtsEnable.ToString() + ", dtr:" + serialPort.DtrEnable.ToString());

                if (os == "Unix")
                {
                    DebugConsole.Instance.Add("lowering RTS");
                    SerialPort.RtsEnable = false;
                    for (int i = 0; i < 8; i++)
                    {
                        Thread.Sleep(250);
                        DebugConsole.Instance.Add(".");
                    }
                }
            }
            catch
            {
                DebugConsole.Instance.AddLn("Can't open!");
                return false;
            }

            try
            {
                SerialPort.WriteTimeout = 1000;
                SerialPort.ReadTimeout = 1000;

                DebugConsole.Instance.Add("pinging....");
                if (Ping() == true)
                {
                    DebugConsole.Instance.AddLn("Found!");
                    return true;
                }
                else
                {
                    DebugConsole.Instance.AddLn("Bad reply");
                }
            }
            catch
            {
                DebugConsole.Instance.AddLn("Timeout");
            }

            SerialPort.Close();

            return false;
        }

        override public bool Reset()
        {
            if (IsOpened() == false)
            {
                return false;
            }

            try
            {
                Thread.Sleep(100);
                byte[] data = { (byte)COMMANDS.RESET };
                SerialPort.Write(data, 0, 1);
                SerialPort.DiscardInBuffer();

                byte[] readBuffer = new byte[2];
                Read(readBuffer, readBuffer.Length);


                return readBuffer.ToString() == "OK";
            }
            catch
            {
                return false;
            }
        }

        override public bool Ping()
        {
            if (IsOpened() == false)
            {
                return false;
            }

            Reset();

            byte[] cmd = { (byte)COMMANDS.PING };
            SerialPort.Write(cmd, 0, 1);

            byte[] readBuffer = new byte[7];
            Read(readBuffer, readBuffer.Length);

            return (readBuffer[0] == 79) && (readBuffer[1] == 67);
        }
    }
}
