using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   class DataBlockRing : Ring<DataBlock>
   {
      int lastSample = -1;

      public DataBlockRing(int size)
         : base(size)
      {
         
      }

      public DataBlock GetFirstElementButDoNotRemoveIfLastOne()
      {
         lock (this)
         {
            if (GetLength() == 0)
            {
               Monitor.Wait(this);
            }

            if (lastSample == m_ring[this.m_read].m_sample)
            {
               Monitor.Wait(this);
            }

            lastSample = m_ring[m_read].m_sample;

            DataBlock data = m_ring[m_read];

            m_read = (m_read + 1) % m_ring.Length;
            m_len--;
            Monitor.Pulse(this);

            return data;
         }
      }
 
   }


}
