using System.IO.Ports;

namespace XOscillo.VizForms.Parallax
{

    class SerialParallax : OscilloSerial
    {
        public enum EdgeEnum
        {
            EDGE_RAISING = 3,
            EDGE_FALLING = 0,
        };

        public enum TriggerChannelEnum
        {
            TC_CH1 = 0,
            TC_CH2 = 1,
        };

        enum VoltageRangeEnum
        {
            VR_4VPP_4VPP = 0,
            VR_4VPP_20VPP = 1,
            VR_20VPP_4VPP = 2,
            VR_20VPP_20VPP = 3,
        };

        private bool fastMode;
        private byte m_sampleRateOpCode;

        override public bool SetSampleRate(int smp)
        {
            lock (ThisLock)
            {
                switch (smp)
                {
                    case 250: m_sampleRateOpCode = 0xd; fastMode = false; break;
                    case 500: m_sampleRateOpCode = 0xc; fastMode = false; break;
                    case 1000: m_sampleRateOpCode = 0xb; fastMode = false; break;
                    case 2500: m_sampleRateOpCode = 0xa; fastMode = false; break;
                    case 5000: m_sampleRateOpCode = 0x9; fastMode = false; break;
                    case 10000: m_sampleRateOpCode = 0x8; fastMode = false; break;
                    case 25000: m_sampleRateOpCode = 0x7; fastMode = false; break;
                    case 50000: m_sampleRateOpCode = 0x6; fastMode = false; break;
                    case 100000: m_sampleRateOpCode = 0x5; fastMode = false; break;
                    case 250000: m_sampleRateOpCode = 0x4; fastMode = false; break;
                    case 500000: m_sampleRateOpCode = 0x2; fastMode = false; break;
                    case 1000000: m_sampleRateOpCode = 0x1; fastMode = true; break; //ok

                    default: return false;
                }

                base.SetSampleRate(smp);
                return true;
            }
        }

        enum TriggerMode
        {
            TM_NORMAL = 0,
            TM_AUTO = 1,
        };

        enum FastChannelSelection
        {
            FCS_CH1 = 1,
            FCS_CH2 = 2,
        };

        private byte triggerVoltage;
        public float TriggerVoltage
        {
            get { return triggerVoltage; }
            set { lock (ThisLock) { triggerVoltage = (byte)(128 - ((value * 255) / 20)); } }
        }

        private EdgeEnum edge;
        public EdgeEnum Edge
        {
            get { return edge; }
            set { lock (ThisLock) { edge = value; } }
        }

        private TriggerChannelEnum triggerChannel;
        public TriggerChannelEnum TriggerChannel
        {
            get { return triggerChannel; }
            set { lock (ThisLock) { triggerChannel = value; } }
        }


        private bool externalTrigger;
        public bool ExternalTrigger
        {
            get { return externalTrigger; }
            set { lock (ThisLock) { externalTrigger = value; } }
        }

        //if this variables are accessed through the UI remember to create accessors and use lock(thisLock)
        private VoltageRangeEnum voltageRange;
        private TriggerMode triggerMode;

        public SerialParallax()
            : base(2)
        {
            // Create a new SerialPort object with default settings.
            SerialPort = new SerialPort();
            voltageRange = VoltageRangeEnum.VR_4VPP_4VPP;
        }

        ~SerialParallax()
        {
        }

        override public string GetName()
        {
            return "Parallax USB oscilloscope";
        }

        override public bool Open(string portName)
        {
            try
            {
                DebugConsole.Instance.Add("trying: " + portName + "...");
                SerialPort = new SerialPort(portName, 40000000);
                SerialPort.Open();
            }
            catch
            {
                DebugConsole.Instance.AddLn("Can't open");
                return false;
            }

            try
            {
                DebugConsole.Instance.Add("is parallax...");
                SerialPort.WriteTimeout = 1000;
                SerialPort.ReadTimeout = 1000;

                if (Ping() == true)
                {
                    DebugConsole.Instance.AddLn("Yes!");
                    return true;
                }
            }
            catch
            {
                DebugConsole.Instance.AddLn("nope.");
            }

            SerialPort.Close();

            return false;
        }

        override public bool Reset()
        {
            byte[] data = { 175 };
            if (SerialPort.IsOpen)
            {
                SerialPort.Write(data, 0, 1);
            }
            SerialPort.DiscardInBuffer();
            SerialPort.DiscardOutBuffer();

            return true;
        }

        override public bool Ping()
        {
            Reset();
            SerialPort.Write("?");

            byte[] readBuffer = new byte[7];
            Read(readBuffer, 7);

            return (readBuffer[0] == 79) /*&& (readBuffer[1]==67)*/;
        }

        private byte[] configBuffer = new byte[9];

        public void PrepareConfigBuffer()
        {
            configBuffer[0] = 170;
            configBuffer[1] = triggerVoltage;
            configBuffer[2] = (byte)(((byte)voltageRange << 6) | ((byte)edge << 4) | (byte)0);
            configBuffer[3] = (byte)(((byte)m_sampleRateOpCode << 3) | ((byte)triggerMode << 2) | ((byte)triggerChannel));

            if (fastMode == false)
            {
                configBuffer[4] = 0;
                // 50%
                configBuffer[5] = 240; //1520   
                configBuffer[6] = 5;
                configBuffer[7] = 239; //1007   
                configBuffer[8] = 3;
            }
            else
            {
                configBuffer[4] = 0x18;

                // 50%
                configBuffer[5] = 160; //928   
                configBuffer[6] = 3;
                configBuffer[7] = 33; //1313   
                configBuffer[8] = 5;

            }

            /*
            //  0 :  05df  06de , 1503  1758
            //  25:  074f  056f,  1871  1391
            //  50:  08d0  03ed,  2256  1005
            //  75:  0a4c  0272,  2636   626
            // 100:  0bc1  01fc,  3009   508

            //   0:  07ff  0569, 2048  1337
            //  25:  0810  04ae
            //  50:  08ca  03f3,  
            //  75:  098b  0333, 2443 819
            // 100:  0a46  0278

            float t = .5f;
            int pre = 500;// (int)(1503f * (1 - t) + 3009f * t);
            int post = 1500;// (int)(1758f * (1 - t) + 508f * t);

            configBuffer[5] = (byte)(pre & 0xff);
            configBuffer[6] = (byte)((pre>>8) & 0xff);
            configBuffer[7] = (byte)(post & 0xff);
            configBuffer[8] = (byte)((post >> 8) & 0xff);
            */
        }

        byte[] res = new byte[1];
        byte[] parallaxBuffer = new byte[3000];

        override public bool GetDataBlock(ref DataBlock db)
        {
            //need to make a copy of this variable just in case it changes
            //remember the ui lives in a different thread and can change values.
            bool copyFastMode;

            lock (ThisLock)
            {
                PrepareConfigBuffer();

                copyFastMode = fastMode;

                db.m_min = 0;
                db.m_max = 5;
                db.m_channels = fastMode ? 1 : GetNumberOfEnabledChannels();
                db.m_channelsBitField = fastMode ? 1 : GetChannelBitField();
                db.m_triggerVoltage = triggerVoltage;
                db.m_triggerPos = fastMode ? 3000 / 2 : 1500 / 2;
                db.m_sampleRate = GetSampleRate();
                db.m_samplesPerChannel = fastMode ? 3000 : 1500;
                db.Alloc();
            }

            if (db.m_sampleRate > 0)
            {
                SerialPort.ReadTimeout = (2 * db.m_samplesPerChannel * 1000) / db.m_sampleRate;
            }
            if (SerialPort.ReadTimeout < 200)
            {
                SerialPort.ReadTimeout = 200;
            }
            SerialPort.Write(configBuffer, 0, 9);
            Read(res, 1);
            if (res[0] == 85)
            {

                Read(parallaxBuffer, 3000);
                if (copyFastMode)
                {
                    for (int i = 0; i < 3000; i++)
                    {
                        db.SetVoltage(0, i, parallaxBuffer[i]);
                    }
                }
                else
                {
                    int index = 0;
                    if ((db.m_channelsBitField & 1) > 0)
                    {
                        for (int i = 0; i < 1500; i++)
                        {
                            db.SetVoltage(index, i, parallaxBuffer[i]);
                        }
                        index++;
                    }
                    if ((db.m_channelsBitField & 2) > 0)
                    {
                        for (int i = 0; i < 1500; i++)
                        {
                            db.SetVoltage(index, i, parallaxBuffer[i + 1500]);
                        }
                        index++;
                    }
                }

                return true;
            }
            return false;
        }
    }



}
