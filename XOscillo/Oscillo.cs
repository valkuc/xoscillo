namespace XOscillo
{
    public abstract class Oscillo
    {
        protected readonly object ThisLock = new object();

        private int m_time = 1000;
        private int m_sampleRate;

        private bool[] m_channelsEnabled;

        public Oscillo()
        {
        }

        public Oscillo(int numChannels)
        {
            m_channelsEnabled = new bool[numChannels];
        }

        public abstract bool Open(string portName);

        public abstract bool IsOpened();

        public abstract string GetName();

        virtual public bool Close()
        {
            if (IsOpened() == false)
            {
                return false;
            }

            return Stop();
        }

        virtual public bool Reset()
        {
            return false;
        }

        virtual public bool Ping()
        {
            return true;
        }

        virtual public bool GetDataBlock(ref DataBlock db)
        {
            return false;
        }


        virtual public bool SetChannel(int i, bool enabled)
        {
            lock (ThisLock)
            {
                if (m_channelsEnabled == null || (i < 0) || (i >= m_channelsEnabled.Length))
                    return false;

                m_channelsEnabled[i] = enabled;
                return true;
            }
        }

        virtual public uint GetChannelBitField()
        {
            lock (ThisLock)
            {
                if (m_channelsEnabled == null)
                    return 0;

                uint ch = 0;
                for (int i = 0; i < m_channelsEnabled.Length; i++)
                {
                    if (m_channelsEnabled[i])
                        ch |= (uint)(1 << i);
                }

                return ch;
            }
        }

        virtual public int GetNumberOfSupportedChannels()
        {
            return m_channelsEnabled.Length;
        }

        virtual public int GetNumberOfEnabledChannels()
        {
            lock (ThisLock)
            {
                if (m_channelsEnabled == null)
                    return -1;

                int ch = 0;
                for (int i = 0; i < m_channelsEnabled.Length; i++)
                {
                    if (m_channelsEnabled[i])
                        ch++;
                }
                return ch;
            }
        }

        virtual public bool Stop()
        {
            return true;
        }

        virtual public bool Start()
        {
            return true;
        }

        virtual public bool SetMeasuringTime(int v)
        {
            lock (ThisLock)
            {
                m_time = v;
                return false;
            }
        }

        virtual public int GetMeasuringTime()
        {
            return m_time;
        }

        virtual public int GetNumberOfSamplesPerChannel()
        {
            lock (ThisLock)
            {
                return (m_time * m_sampleRate) / 1000;
            }
        }

        virtual public int GetSampleRate()
        {
            return m_sampleRate;
        }

        virtual public bool SetSampleRate(int s)
        {
            lock (ThisLock)
            {
                m_sampleRate = s;
                return true;
            }
        }

    }
}
