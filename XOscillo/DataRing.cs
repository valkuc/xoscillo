using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   class OldRing
   {
      Thread oThread;
      int Samples;

      public delegate bool GetDataBlock(ref DataBlock db);

      GetDataBlock m_getData;

      DataBlock[] m_DataBlock = new DataBlock[16];
		int ini = 0;
		int len = 0;
		bool m_running = false;

      public OldRing( GetDataBlock getData )
      {
         m_getData = getData;
         Samples = 0;

         for (int i = 0; i < 16; i++)
            m_DataBlock[i] = new DataBlock();
      }

      public void SamplingLoop()
		{


			while (m_running == true)
			{
            try
            {
               int i = (ini + len) & 0xf;

               if (m_getData(ref m_DataBlock[i]) == true)
               {
                  m_DataBlock[i].m_sample = Samples;

                  len++;
                  if (len >= 16)
                     len = 15;

                  Samples++;
               }
            }
            catch
            {
               //Reset();
            }
			}
		}

      public bool GetRunning()
      {
         return m_running;
      }
      
      public bool SetRunning(bool r)
      {
         if (r == true)
         {
            if (m_running == false)
            {
               oThread = new Thread(new ThreadStart(SamplingLoop));
               oThread.Start();
               m_running = true;
            }
         }
         else
         {
            m_running = false;
            //oThread.w
         }

         return m_running;
      }

		public int GetLength()
		{
			return len;
		}


		public bool Lock( out DataBlock data )
		{
         int smp = GetSamples();

         if (GetLength() == 0)
         {
            data = null;
            return false;
         }

         data = m_DataBlock[ini];
         /*
         if (data.m_Buffer == null)
         {
            return false;
         }
          */

			return true;
		}

      public void Unlock()
      {
         ini++;
         if (ini >= 16)
            ini = 0;

         len--;
      }


      public int GetSamples()
		{
			return Samples;
		}
 
   }


}
