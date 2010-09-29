using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace XOscillo
{
	public enum Edge
	{
		EDGE_RAISING = 3,
		EDGE_FALLING = 0,
	};

	public enum TriggerChannel
	{
		TC_CH1 = 0,
		TC_CH2 = 1,
	};

   public class SerialParallax : Oscillo
   {

      enum VoltageRange
      {
         VR_4VPP_4VPP = 0,
         VR_4VPP_20VPP = 1,
         VR_20VPP_4VPP = 2,
         VR_20VPP_20VPP = 3,
      };

      enum ChannelOnOFF
      {
         COF_CH1 = 0,
         COF_CH2 = 1,
      }

      byte m_sampleRateOpCode;

      override public int[] GetSampleRates()
      {
         return m_sampleRates;
      }

      override public bool SetSamplingRate(int smp)
      {
         if (smp == 100)
         {
            m_sampleRateOpCode = 0xe;
            fastMode = false;
         }
         else if (smp == 250)
         {
            m_sampleRateOpCode = 0xd;
            fastMode = false;
         }
         else if (smp == 500)
         {
            m_sampleRateOpCode = 0xc;
            fastMode = false;
         }
         else if (smp == 1000)
         {
            m_sampleRateOpCode = 0xb;
            fastMode = false;
         }
         else if (smp == 2500)
         {
            m_sampleRateOpCode = 0xa;
            fastMode = false;
         }
         else if (smp == 5000)
         {
            m_sampleRateOpCode = 0x9;
            fastMode = false;
         }
         else if (smp == 10000)
         {
            m_sampleRateOpCode = 0x8;
            fastMode = false;
         }
         else if (smp == 25000)
         {
            m_sampleRateOpCode = 0x7;
            fastMode = false;
         }
         else if (smp == 50000)
         {
            m_sampleRateOpCode = 0x6;
            fastMode = false;
         }
         else if (smp == 100000)
         {
            m_sampleRateOpCode = 0x5;
            fastMode = false;
         }
         if (smp == 250000)
         {
            m_sampleRateOpCode = 0x4;
            fastMode = false;
         }
         else if (smp == 500000)
         {
            m_sampleRateOpCode = 0x3;
            fastMode = false;
         }
         else if (smp == 1000000)
         {
            m_sampleRateOpCode = 0x2;
            fastMode = false;
         }
         else if (smp == 2500000)
         {
            m_sampleRateOpCode = 0x1;
            fastMode = false;
         }
         else if (smp == 5000000)
         {
            m_sampleRateOpCode = 0x1;
            fastMode = true;
         }

         m_sampleRate = smp;
         return true;
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
      public byte TriggerVoltage
      {
         get { return triggerVoltage; }
         set { triggerVoltage = value; }
      }

      VoltageRange voltageRange;

      public Edge edge;

      ChannelOnOFF channleOnOff;

      TriggerMode triggerMode;

      public TriggerChannel triggerChannel;

      public bool externalTrigger;

      FastChannelSelection fastChannelSelection;

      bool fastMode;

      int preTrigger, postTrigger;

      SerialPort serialPort;

      public SerialParallax()
      {
         // Create a new SerialPort object with default settings.
         serialPort = new SerialPort();
         triggerVoltage = 128;
         voltageRange = VoltageRange.VR_20VPP_20VPP;
         edge = Edge.EDGE_RAISING;
         channleOnOff = ChannelOnOFF.COF_CH1;
         triggerMode = TriggerMode.TM_AUTO;
         triggerChannel = TriggerChannel.TC_CH1;
         externalTrigger = false;
         m_sampleRates = new int[15] { 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000 };

         this.SetPreTrigger(10);
         this.SetPostTrigger(90);
      }

      ~SerialParallax()
      {
      }

      override public bool Open()
      {
         // Allow the user to set the appropriate properties.
         if (AutoDetect())
         {
            serialPort.ReadTimeout = 10000;
            serialPort.WriteTimeout = 10000;
            return true;
         }

         return false;
      }

      virtual public bool AutoDetect()
      {
         string[] ports = SerialPort.GetPortNames();

         foreach (string portName in ports)
         {
            try
            {
               serialPort = new SerialPort(portName);
               serialPort.Open();
            }
            catch
            {
               continue;
            }

            try
            {
               serialPort.WriteTimeout = 4000;
               serialPort.ReadTimeout = 4000;

               if (Ping() == true)
               {
                  return true;
               }
            }
            catch
            {
            }

            serialPort.Close();
         }

         return false;
      }


      override public bool Close()
      {
         serialPort.Close();
         return true;
      }

      override public void Reset()
      {
         byte[] data = { 175 };
         if (serialPort.IsOpen)
         {
            serialPort.Write(data, 0, 1);
         }
         serialPort.DiscardInBuffer();
         serialPort.DiscardOutBuffer();
      }

      override public bool Ping()
      {
         Reset();
         serialPort.Write("?");

         byte[] readBuffer = new byte[7];
         Read(readBuffer, 7);

         return (readBuffer[0] == 79) /*&& (readBuffer[1]==67)*/;
      }

      public void SetPreTrigger(byte f)
      {
         preTrigger = (3000 * f) / 100;
      }

      public void SetPostTrigger(byte f)
      {
         postTrigger = 3000 - (3000 * f) / 100;
      }


      public void Config()
      {
         byte[] configBuffer = new byte[9];
         configBuffer[0] = 170;
         configBuffer[1] = triggerVoltage;
         configBuffer[2] = (byte)(((byte)voltageRange << 6) | ((byte)edge << 4) | (byte)channleOnOff);
         configBuffer[3] = (byte)(((byte)m_sampleRateOpCode << 3) | ((byte)triggerMode << 2) | ((byte)triggerChannel));
         configBuffer[4] = 0;

         if (fastMode)
            configBuffer[4] = 8 + 16;

         // 90%
         configBuffer[5] = 180; //2740
         configBuffer[6] = 10;
         configBuffer[7] = 140;  //396
         configBuffer[8] = 1;

         // 50%
         configBuffer[5] = 240; //1520
         configBuffer[6] = 5;
         configBuffer[7] = 239;  //1007
         configBuffer[8] = 3;

         /*
         configBuffer[5] = 180; //2740
         configBuffer[6] = 10;
         configBuffer[7] = 80;  
         configBuffer[8] = 6;
         */

         serialPort.Write(configBuffer, 0, 9);
      }

      public void Read(byte[] readBuffer, int length)
      {
         int dataread = 0;
         while (dataread < length)
            dataread += serialPort.Read(readBuffer, dataread, length - dataread);
      }

      DateTime time = new DateTime();
      byte[] res = new byte[1];

      override public bool GetDataBlock(ref DataBlock db)
      {
         try
         {
            Config();

            time = DateTime.Now;

            Read(res, 1);
            if (res[0] == 85)
            {
               db.m_sample = 0;
               db.m_start = time;
               db.m_stop = DateTime.Now;
               db.m_channels = fastMode ? 1 : 2;
               db.m_trigger = (3000 / 2) / (fastMode ? 1 : 2);
               db.m_sampleRate = m_sampleRate;
               db.m_stride = 1;
               db.m_channelOffset = 1500;
               if (db.m_Buffer == null || db.m_Buffer.Length != 3000)
               {
                  db.Alloc(3000);
               }

               Read(db.m_Buffer, 3000);
               return true;
            }
         }
         catch
         {
            Reset();
         }
         return false;
      }



   }



}
