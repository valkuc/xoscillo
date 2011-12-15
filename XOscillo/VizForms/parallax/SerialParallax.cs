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
      int SampleID = 0;

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
         switch (smp)
         {

            case 50: m_sampleRateOpCode = 0xf; fastMode = false; break;
            case 100: m_sampleRateOpCode = 0xe; fastMode = false; break;
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
            case 500000: m_sampleRateOpCode = 0x3; fastMode = false; break;
            case 1000000: m_sampleRateOpCode = 0x2; fastMode = true; break;
                
            default: return false;
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
      
      public float TriggerVoltage
      {
         get { return triggerVoltage; }
         set { triggerVoltage = (byte)(128 - ((value * 255) / 20)); }
      }

      VoltageRange voltageRange;

      public Edge edge;

      ChannelOnOFF channleOnOff;

      TriggerMode triggerMode;

      public TriggerChannel triggerChannel;

      public bool externalTrigger;

      //FastChannelSelection fastChannelSelection;

      bool fastMode;

      int preTrigger, postTrigger;

      SerialPort serialPort;

      public SerialParallax()
      {
         // Create a new SerialPort object with default settings.
         serialPort = new SerialPort();
         triggerVoltage = 1;
         voltageRange = VoltageRange.VR_20VPP_20VPP;
         edge = Edge.EDGE_RAISING;
         channleOnOff = ChannelOnOFF.COF_CH1;
         triggerMode = TriggerMode.TM_NORMAL;
         triggerChannel = TriggerChannel.TC_CH1;
         externalTrigger = false;
         m_sampleRates = new int[15] { 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000 };

         this.SetPreTrigger(10);
         this.SetPostTrigger(90);
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
            serialPort = new SerialPort(portName);
            serialPort.Open();
         }
         catch
         {
            DebugConsole.Instance.AddLn("Can't open");
            return false;
         }

         try
         {
            DebugConsole.Instance.Add("is parallax...");
            serialPort.WriteTimeout = 1000;
            serialPort.ReadTimeout = 1000;

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

         serialPort.Close();

         return false;
      }

      override public bool IsOpened()
      {
         return serialPort.IsOpen;
      }

      override public bool Close()
      {
         serialPort.Close();
         return true;
      }

      override public bool Reset()
      {
         byte[] data = { 175 };
         if (serialPort.IsOpen)
         {
            serialPort.Write(data, 0, 1);
         }
         serialPort.DiscardInBuffer();
         serialPort.DiscardOutBuffer();

         return true;
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

         // 50%
         configBuffer[5] = 240; //1520   
         configBuffer[6] = 5;
         configBuffer[7] = 239; //1007   
         configBuffer[8] = 3;

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
         Config();

         time = DateTime.Now;

         Read(res, 1);
         if (res[0] == 85)
         {
            db.m_sample = SampleID++; ;
            db.m_start = time;
            db.m_dataType = DataBlock.DATA_TYPE.ANALOG;
            db.m_stop = DateTime.Now;
            db.m_00 = 20;
            db.m_FF = -20;
            db.m_channels = fastMode ? 1 : 2;
            db.m_triggerVoltage = triggerVoltage;
            db.m_triggerPos = 1500/2;
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
         return false;
      }



   }



}
