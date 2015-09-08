using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
    class AnalogArduino : SerialArduino
    {
        public AnalogArduino() 
            //: base(115200, 12000)
            : base(1000000, 59250, 2)
        {
            
            //needs tunning
            //baudrate = 1000000;
            SetSampleRate(59250); // this is actual number of samples per second I am able to archieve on the arduino
        }

        public void SetTriggerVoltage(byte v)
        {
            this.m_triggerValue = v;
        }

        byte[] res = new byte[1];
        byte [] m_arduinoBuffer = new byte[1000];

        override public bool GetDataBlock(ref DataBlock db)
        {
            bool result;

            //assume it timed out
            db.m_result = DataBlock.RESULT.TIMEOUT;

            if (GetNumberOfEnabledChannels() == 0)
            {
                return false;
            }

            //-------------Get settings this needs a crytical section)
            //
            int numberOfSamples;
            int numberOfEnabledChannels;
            uint channelsBitField;
            int sampleRate;
            lock (thisLock)
            {
                numberOfSamples = GetNumberOfSamplesPerChannel();
                numberOfEnabledChannels = GetNumberOfEnabledChannels();
                channelsBitField = GetChannelBitField();
                sampleRate = GetSampleRate();
            }

            //-------------Request data
            byte[] configBuffer = new byte[10];
            configBuffer[0] = (byte)COMMANDS.READ_ADC_TRACE;
            configBuffer[1] = m_triggerValue;
            configBuffer[2] = (byte)(numberOfSamples >> 8);
            configBuffer[3] = (byte)(numberOfSamples & 0xff);
            configBuffer[4] = (byte)numberOfEnabledChannels;

            int index = 0;
            for (byte i = 0; i < 4; i++)
            {
                if (((channelsBitField>>i)&1)!=0)
                {
                    configBuffer[5 + index] = i;
                    index++;
                }
            }

            configBuffer[9] = (byte)127; //pwm

            Write(configBuffer, configBuffer.Length);

            //-------------Get data
            Read(res, 1);
            if (res[0] == 85)
            {
                db.m_min = 0;
                db.m_max = 5;
                db.m_channels = numberOfEnabledChannels;
                db.m_channelsBitField = channelsBitField;
                db.m_triggerVoltage = 0;
                db.m_triggerPos = 0;
                db.m_sampleRate = (db.m_channels > 0) ? (sampleRate / db.m_channels) : 0;
                db.m_samplesPerChannel = numberOfSamples;
                db.m_dataType = DataBlock.DATA_TYPE.ANALOG;            
                db.Alloc();

                //read actual data
                if (m_arduinoBuffer.Length != db.m_samplesPerChannel * db.m_channels)
                {
                    m_arduinoBuffer = new byte[db.m_samplesPerChannel * db.m_channels];
                }            
                result = Read(m_arduinoBuffer, m_arduinoBuffer.Length);

                index = 0;
                for(int ch=0;ch<2;ch++)
                {
                    if ( ((db.m_channelsBitField>>ch)&1) == 1)
                    {
                        for (int i = 0; i < db.GetChannelLength(); i++)
                        {
                            db.SetVoltage(index, i, m_arduinoBuffer[i * db.m_channels + index]);
                        }
                        index++;
                    }
                }

                db.m_result = DataBlock.RESULT.OK;

                return result;
            }

            return false;
      }

   }
}
