using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace XOscillo
{
   class SerialArduino : Oscillo
   {
      SerialPort serialPort;
      byte m_triggerValue = 127;
      int m_numSamples = 1024;
      int baudrate;
      int SampleID = 0;

      public SerialArduino()
      {
         // Create a new SerialPort object with default settings.
         serialPort = new SerialPort();
         /*
         {
            baudrate = 1000000;
            m_sampleRate = 59250; // this is actual number of samples per second I am able to archieve on the arduino
         }
          */
         //needs tunning
         
         {
            baudrate = 115200;
            m_sampleRate = 12000;
         }
         

         m_sampleRates = new int[1] { m_sampleRate };        
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
            serialPort = new SerialPort(portName, baudrate, Parity.None, 8,StopBits.One);
            serialPort.Handshake = Handshake.None;

            DebugConsole.Instance.Add(portName + ", rts:" + serialPort.RtsEnable.ToString() + ", dtr:" + serialPort.DtrEnable.ToString() + "   trying...");
            serialPort.Open();
            //DebugConsole.Instance.Add(" rts:" + serialPort.RtsEnable.ToString() + ", dtr:" + serialPort.DtrEnable.ToString());

            if ( os == "Unix" )
            {
               DebugConsole.Instance.Add("lowering RTS");
               serialPort.RtsEnable = false;
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
            serialPort.WriteTimeout = 8000;
            serialPort.ReadTimeout = 8000;

            DebugConsole.Instance.Add("pinging....");
            if (Ping() == true)
            {
               DebugConsole.Instance.AddLn("Found!");
               serialPort.ReadTimeout = 10000;
               serialPort.WriteTimeout = 10000;
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

      override public bool SetSamplingRate(int v)
      {
         return true;
      }

      public void SetTriggerVoltage( byte v )
      {
         this.m_triggerValue = v;
      }

      override public bool SetNumberOfChannels(int n)
      {
         this.m_numberOfChannels = n;
         return true;
      }

       public bool Read(byte[] readBuffer, int length)
      {
         int dataread = 0;
         while (dataread < length)
         {
            dataread += serialPort.Read(readBuffer, dataread, length - dataread);
         }
         return true;
      }

      override public void Reset()
      {
         byte[] data = { 175 };
         if (serialPort.IsOpen)
         {
            serialPort.Write(data, 0, 1);
         }
      }

      override public bool Ping()
      {
         Reset();

         serialPort.Write("?");


         byte[] readBuffer = new byte[7];
         Read(readBuffer, 7);

         return (readBuffer[0] == 79) && (readBuffer[1]==67);
      }


      DateTime time = new DateTime();
      byte[] res = new byte[1];

      public void Config()
      {
         byte[] configBuffer = new byte[9];
         configBuffer[0] = 170;
         configBuffer[1] = m_triggerValue;
         configBuffer[2] = (byte)( ( m_numSamples * m_numberOfChannels ) >> 8);
         configBuffer[3] = (byte)( ( m_numSamples * m_numberOfChannels ) & 0xff);
         configBuffer[4] = (byte)m_numberOfChannels;
         configBuffer[5] = (byte)127; //pwm

         serialPort.DiscardInBuffer();
         serialPort.Write(configBuffer, 0, 9);
      }


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
            
            db.m_channels = m_numberOfChannels;
            db.m_trigger = 0;
            db.m_sampleRate = m_sampleRate / m_numberOfChannels;
            db.m_stride = m_numberOfChannels;
            db.m_channelOffset = m_numberOfChannels;

            if (db.m_Buffer == null || db.m_Buffer.Length != m_numSamples)
            {
               db.Alloc(m_numSamples);
            }

            result = Read(db.m_Buffer, m_numSamples);
            db.m_stop = DateTime.Now;
            return result;
         }

         return false;
      }


   }
}
