using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   
   class DigitalArduino : SerialArduino
   {
      int SampleID = 0;

      public DigitalArduino()
         //: base(115200, 12000)
          : base(1000000, 59250,6)
      {
         m_triggerValue = 1;
      }

      DateTime time = new DateTime();
      byte[] res = new byte[1];

      public void Config()
      {
         int numSamples = GetNumberOfSamplesPerChannel();

         byte[] configBuffer = new byte[4];
         configBuffer[0] = (byte)COMMANDS.READ_BIN_TRACE; ;
         configBuffer[1] = m_triggerValue;
         configBuffer[2] = (byte)(numSamples >> 8);
         configBuffer[3] = (byte)(numSamples & 0xff);

         Write(configBuffer, configBuffer.Length);
      }

      byte[] m_arduinoBuffer = new byte[1000];

      override public bool GetDataBlock(ref DataBlock db)
      {
         bool result;

         Config();

         time = DateTime.Now;

         Read(res, 1);
         if (res[0] == 85)
         {
            db.m_sample = SampleID++;
            db.m_start = DateTime.Now;

            db.m_channels = 6;
            db.m_triggerVoltage = 0;
            db.m_triggerPos = 0;
            db.m_sampleRate = GetSampleRate();
            db.m_samplesPerChannel = GetNumberOfSamplesPerChannel();
            db.m_channelsBitField = GetChannelBitField();
            db.m_dataType = DataBlock.DATA_TYPE.DIGITAL;

            db.Alloc();

            if (m_arduinoBuffer.Length != db.m_samplesPerChannel)
            {
                m_arduinoBuffer = new byte[db.m_samplesPerChannel];
            }

            result = Read(m_arduinoBuffer, m_arduinoBuffer.Length);

            for (int i = 0; i < m_arduinoBuffer.Length; i++)
            {
                db.SetVoltage(0, i, m_arduinoBuffer[i]);
            }

            db.m_stop = DateTime.Now;
            return result;
         }

         return false;
      }


   }
    
}
