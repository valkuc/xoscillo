using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   public class Ring <T> where T : new ()
   {
      protected int m_read = 0;
      int m_write = 0;
      protected int m_len = 0;
      protected T[] m_ring;

      public Ring(int size)
      {
         m_ring = new T[size];
         for (int i = 0; i < size; i++)
         {
            m_ring[i] = new T();
         }
      }

      public int GetLength()
      {
         return m_len;
      }
      
      public virtual void getLock(out T data)
      {
         lock (this)
         {
            while (GetLength() == 0)
            {
               Monitor.Wait(this);
            }

            data = m_ring[m_read];

            m_read = (m_read + 1) % m_ring.Length;
         }
      }

      public virtual void getUnlock()
      {
         lock (this)
         {
            m_len--;
            Monitor.Pulse(this);
         }
      }

      public virtual void putLock( out T data)
      {
         lock (this)
         {
            while (GetLength() == m_ring.Length)
            {
               Monitor.Wait(this);
            }

            data = m_ring[m_write];

            m_write = (m_write + 1) % m_ring.Length;
         }        
      }

      public virtual void putUnlock()
      {
         lock (this)
         {
            m_len++;
            Monitor.Pulse(this);
         }
      }


      public virtual void Peek(out T data)
      {
         lock (this)
         {
            if (GetLength() == 0)
            {
               Monitor.Wait(this);
            }

            data = m_ring[m_read];
         }
      }
   }
}
