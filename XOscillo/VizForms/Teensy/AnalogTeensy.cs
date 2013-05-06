using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   class AnalogTeensy : SerialTeensy
   {
      int SampleID = 0;

      int m_numSamples = 1024;

      public AnalogTeensy() 
      {
            
         //needs tunning
         //baudrate = 1000000;
         m_sampleRate = 10*1024; // this is actual number of samples per second I am able to archieve on the Teensy
      }

      public void SetTriggerVoltage(byte v)
      {
         //this.m_triggerValue = v;
      }

      DateTime time = new DateTime();
      byte[] res = new byte[1];

      int index = 0;
      Random random = new Random();
      
      override public bool GetDataBlock(ref DataBlock db)
      {
         bool result=true;

         //assume it timed out
        db.m_result = DataBlock.RESULT.TIMEOUT;

        time = DateTime.Now;

        m_numberOfChannels = 1;

        db.m_sample = SampleID++;
        db.m_start = DateTime.Now;
        db.m_00 = 0;
        db.m_FF = 5;
        db.m_channels = m_numberOfChannels;
        db.m_triggerVoltage = 0;
        db.m_triggerPos = 0;
        db.m_sampleRate = m_sampleRate / m_numberOfChannels;
        db.m_stride = m_numberOfChannels;
        db.m_channelOffset = m_numberOfChannels;
        db.m_dataType = DataBlock.DATA_TYPE.ANALOG;
            

        if (db.m_Buffer == null || db.m_Buffer.Length != m_numSamples)
        {
            db.Alloc(m_numSamples);
        }

        byte[] readBuffer = new byte[6];
        Read(readBuffer, readBuffer.Length);

        //Thread.Sleep(100);
        //int index = (readBuffer[0] >> 4) & 0x7;

        UInt16 v = (UInt16)(readBuffer[3] * 256 + readBuffer[4]);

        db.m_Buffer[index % m_numSamples] = (byte)(v/256);
        index++;

        //result = Read(db.m_Buffer, m_numSamples);
        db.m_stop = DateTime.Now;

        db.m_result = DataBlock.RESULT.OK;

        return result;
      }
  

   }
}
