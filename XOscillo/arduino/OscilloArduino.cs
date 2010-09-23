using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace XOscillo
{
   class OscilloArduino : Oscillo
   {
      SerialPort serialPort;
      byte m_triggerValue = 127;
      int m_numSamples = 1024;
      //static int baudrate = 153600;
      static int baudrate = 115200;

      /*
            153600 / 8 = 19200 bytes/s
            
             1024 samples -> 0.053 seconds
       
       */
      //static uint baudrate = 230400;

      public OscilloArduino()
      {
         // Create a new SerialPort object with default settings.
         serialPort = new SerialPort();

         m_sampleRates = new int[1] { baudrate / 8 };

         //needs tunning
         SetSampleRate(15400);
      }

      private bool AutoDetect()
      {
         string[] ports = SerialPort.GetPortNames();
         
         foreach (string portName in ports)
         {
            try
            {
               serialPort = new SerialPort(portName, baudrate, Parity.None, 8,StopBits.One);
               serialPort.Handshake = Handshake.None;
               
               System.Console.Write(portName + ", rts:" + serialPort.RtsEnable.ToString() + ", dtr:" + serialPort.DtrEnable.ToString() + "   trying...");

               serialPort.RtsEnable = false;    
               serialPort.Open();
               
            }
            catch
            {
               System.Console.WriteLine("Can't open!");
               continue;
            }

            try
            {
               serialPort.WriteTimeout = 1000;
               serialPort.ReadTimeout = 1000;

               System.Console.Write("pinging...");
               if (Ping() == true)
               {
                  System.Console.WriteLine("Found!");
                  return true;
               }
               else
               {
                  System.Console.WriteLine("Bad reply");
               }
            }
            catch
            {
               System.Console.WriteLine("Timeout");
            }

            serialPort.Close();
         }

         return false;
      }

      override public bool Open()
      {
         // Allow the user to set the appropriate properties.
         if ( AutoDetect() )
         {
            serialPort.ReadTimeout = 10000;
            serialPort.WriteTimeout = 10000;
            return true;
         }

         return false;
      }

      override public bool Close()
      {
         serialPort.Close();
         return true;
      }

      override public bool SetSampleRate(int v)
      {
         this.m_sampleRate = v;
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
            db.m_sample = 0;
            db.m_start = DateTime.Now;
            
            db.m_channels = m_numberOfChannels;
            db.m_trigger = 0;
            db.m_sampleRate = m_sampleRate / m_numberOfChannels;
            db.m_stride = m_numberOfChannels;

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
