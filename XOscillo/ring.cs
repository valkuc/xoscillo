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

      protected bool m_stop;

      public Ring(int size)
      {
         m_ring = new T[size];
         for (int i = 0; i < size; i++)
         {
            m_ring[i] = new T();
         }
      }

      public bool IsStopped()
      {
         return m_stop;
      }

      public void Stop()
      {
         lock (this)
         {
            m_stop = true;
            Monitor.PulseAll(this);
         }
      }

      public void Start()
      {
         lock (this)
         {
            m_stop = false;
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
            m_stop = false;

            while ( (GetLength()==0) && (m_stop==false) )
            {
               Monitor.Wait(this);
            }

            if (m_stop)
            {
               data = default(T);
               return;
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
            if ((GetLength() == 0) && (m_stop == false))
            {
               Monitor.Wait(this);
            }

            if (m_stop)
            {
               data = default(T);
               return;
            }


            data = m_ring[m_read];
         }
      }
   }
}
