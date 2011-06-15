using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   class AnalogArduino : SerialArduino
   {
      int m_numSamples = 1024;
      int SampleID = 0;

      public AnalogArduino() : base(115200, 12000)
      {
         //needs tunning
         //baudrate = 1000000;
         //m_sampleRate = 59250; // this is actual number of samples per second I am able to archieve on the arduino
      }

      public void SetTriggerVoltage(byte v)
      {
         this.m_triggerValue = v;
      }

      DateTime time = new DateTime();
      byte[] res = new byte[1];

      public void Config()
      {
         byte[] configBuffer = new byte[9];
         configBuffer[0] = (byte)COMMANDS.READ_ADC_TRACE;
         configBuffer[1] = m_triggerValue;
         configBuffer[2] = (byte)((m_numSamples * m_numberOfChannels) >> 8);
         configBuffer[3] = (byte)((m_numSamples * m_numberOfChannels) & 0xff);
         configBuffer[4] = (byte)m_numberOfChannels;
         configBuffer[5] = (byte)127; //pwm

         Write(configBuffer, configBuffer.Length);
      }


      override public bool GetDataBlock(ref DataBlock db)
      {
         bool result;

         //assume it timed out
         db.m_result = DataBlock.RESULT.TIMEOUT;

         Config();

         time = DateTime.Now;

         Read(res, 1);
         if (res[0] == 85)
         {
            db.m_sample = SampleID++;
            db.m_start = DateTime.Now;

            db.m_channels = m_numberOfChannels;
            db.m_trigger = 0;
            db.m_sampleRate = m_sampleRate / m_numberOfChannels;
            db.m_stride = m_numberOfChannels;
            db.m_channelOffset = m_numberOfChannels;
            db.m_dataType = DataBlock.DATA_TYPE.ANALOG;
            

            if (db.m_Buffer == null || db.m_Buffer.Length != m_numSamples)
            {
               db.Alloc(m_numSamples);
            }

            result = Read(db.m_Buffer, m_numSamples);
            db.m_stop = DateTime.Now;

            db.m_result = DataBlock.RESULT.OK;

            return result;
         }

         return false;
      }
   

   }
}
