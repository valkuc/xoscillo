using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace XOscillo
{
   class SerialTeensy : Oscillo
   {
      private SerialPort serialPort;

      public SerialTeensy()
      {
         // Create a new SerialPort object with default settings.
         serialPort = new SerialPort();

         m_sampleRates = new int[1] { 1000 }; 
      }

      override public string GetName()
      {
         return "Teensy";
      }

      private void Set(byte b1, byte b2, byte b3)
      {
          byte [] data = { 177,b1,b2,b3};
          Write(data, data.Length);
      }

      override public bool Open(string portName)
      {
         string os = Environment.OSVersion.Platform.ToString();

         try
         {
             serialPort = new SerialPort(portName, 460800, Parity.None, 8, StopBits.One);
            serialPort.Handshake = Handshake.None;

            DebugConsole.Instance.Add(portName + ", rts:" + serialPort.RtsEnable.ToString() + ", dtr:" + serialPort.DtrEnable.ToString() + "   trying...");
            serialPort.Open();

             //set osc mode
            Set(163, 162, 162);
            Set(133, 0, 1);
            SetSamplingRate(100);
            SetNumberOfChannels(1);
         }
         catch
         {
            DebugConsole.Instance.AddLn("Can't open!");
            return false;
         }
        
         return true;
      }

      override public bool IsOpened()
      {
         return serialPort.IsOpen;
      }

      override public bool Close()
      {
         if (IsOpened() == false)
         {
            return false;
         }

         serialPort.Close();
         return true;
      }

      public bool SetNumberOfSamples(int v)
      {
         //this.m_numSamples = v;
         return true;
      }

      override public bool SetSamplingRate(int v)
      {
          this.Set(132, (byte)(v >> 8), (byte)(v & 0xff));
         return true;
      }

      override public bool SetNumberOfChannels(int n)
      {
         this.m_numberOfChannels = n;
         //this.Set( 133, 0, (byte)n);
         return true;
      }

      public bool Read(byte[] readBuffer, int length)
      {
         if (IsOpened() == false)
         {
            return false;
         }

         int dataread = 0;
         while (dataread < length)
         {
            dataread += serialPort.Read(readBuffer, dataread, length - dataread);
         }
         return true;
      }

      public void Write(byte[] writeBuffer, int length)
      {
         serialPort.Write(writeBuffer, 0, length);
      }

      override public bool Reset()
      {
         if (IsOpened() == false)
         {
            return false;
         }

         return true;
      }

      override public bool Ping()
      {
         return true;
      }
   }
}
