using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace XOscillo
{
    class SerialTeensy : OscilloSerial
    {
        protected BinaryReader streamReader;

        bool running = false;

        public SerialTeensy()
            : base(6)
        {
        }

        override public string GetName()
        {
            return "Teensy";
        }

        override public bool Open(string portName)
        {
            string os = Environment.OSVersion.Platform.ToString();

            try
            {
                serialPort = new SerialPort(portName, 460800, Parity.None, 8, StopBits.One);
                serialPort.Handshake = Handshake.None;
                serialPort.ReadBufferSize = 1024 * 10;
                serialPort.Open();
                streamReader = new BinaryReader(serialPort.BaseStream);

                SetSampleRate(1000);
                SetOscSubsampling(3);//set subsampling at (1<<3) = 8
            }
            catch
            {
                DebugConsole.Instance.AddLn("Can't open!");
                return false;
            }

            return true;
        }

        public BinaryReader GetStream()
        {
            return streamReader;
        }

        override public bool Reset()
        {
            if (IsOpened() == false)
            {
                return false;
            }

            Stop();
            Start();

            return true;
        }

        override public bool Ping()
        {
            return true;
        }

        override public bool SetChannel(int i, bool enabled)
        {
            bool res = base.SetChannel(i, enabled);

            if (running == true)
            {
                Set(163, 169, 169);//set osc mode
                /*
                //this will interrupt the current data acquisition and restart
                Get(163, 0, 0);

                SendOscChannels((byte)GetNumberOfEnabledChannels());

                Start();
                 */
            }
            else
            {
                SendOscChannels((byte)GetNumberOfEnabledChannels());
            }

            return res;
        }

        override public bool Start()
        {
            //serialPort.DiscardInBuffer();
            Set(163, 162, 162);//set osc mode
            running = true;
            return true;
        }

        override public bool Stop()
        {
            Set(163, 169, 169); //set keyboard
            //serialPort.DiscardInBuffer();
            running = false;
            return true;
        }

        override public bool SetSampleRate(int v)
        {
            //set appropriate sample rate according to measuring time
            lock (thisLock)
            {
                base.SetSampleRate(v);
                Set(132, (byte)(v >> 8), (byte)(v & 0xff));
                return true;
            }
        }

        private void Set(byte b1, byte b2, byte b3)
        {
            byte[] data = { 177, b1, b2, b3 };
            Write(data, data.Length);
        }

        private void Get(byte b1, byte b2, byte b3)
        {
            byte[] data = { 169, b1, b2, b3 };
            Write(data, data.Length);
        }


        protected bool GetResponse(byte header)
        {
            byte[] data = GetStream().ReadBytes(3);
            if (header == 169)
            {

                if (data[1] == 133)
                {
                    //this.m_numberOfChannels = data[1];
                    return true;
                }
            }
#if DEBUG
            Console.WriteLine("Unknown command:{0} {1} {2} {3}", header, data[0], data[1], data[2]);
#endif
            return false;
        }

        private void SetOscSubsampling(byte v)
        {
            Set(136, 0, v); 
        }

        protected void SendOscChannels(byte n)
        {
            Set(133, 0, n);
        }

    }
}
