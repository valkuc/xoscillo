using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   public class Oscillo
   {
      public delegate void GotFrame(ref DataBlock db);

      protected int m_sampleRate;

      protected int[] m_sampleRates = new int[1] { 0 };

      protected int m_numberOfChannels;

      virtual public string GetName()
      {
         return "";
      }

      virtual public bool Open()
      {
         return true;
      }

      virtual public bool Close()
      {
         return true;
      }

      virtual public void Reset()
      {
      }

      virtual public bool Ping()
      {
         return true;
      }

      virtual public bool GetDataBlock(ref DataBlock db)
      {
         return false;
      }

      virtual public int[] GetSampleRates()
      {
         return m_sampleRates;
      }

      virtual public bool SetSamplingRate(int smp)
      {
         return false;
      }

      virtual public int GetSampleRate()
      {
         return m_sampleRate;
      }


      virtual public bool SetNumberOfChannels( int n )
      {
         return false;
      }

      virtual public int GetSamples()
      {
         return 0;
      }

      virtual public bool SetRunning(bool r)
      {
         return false;
      }

      virtual public int GetLength()
      {
         return 0;
      }

   }
}
