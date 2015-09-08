using System.Threading;

namespace XOscillo
{
   public class Ring <T> where T : new ()
   {
      private int m_read = 0;
      private int m_write = 0;
      private int m_len = 0;
      private T[] m_ring;

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
          lock (this)
          {
              return m_len;
          }
      }
      
      public bool GetLock(out T data)
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
               return false;
            }

            data = m_ring[m_read];

            return true;
         }
         
      }

      public void GetUnlock()
      {
         lock (this)
         {
            m_len--;
            m_read = (m_read + 1) % m_ring.Length;
            Monitor.Pulse(this);
         }
      }

      public bool PutLock( out T data)
      {
         lock (this)
         {
            while ((GetLength() == m_ring.Length)  && (m_stop==false))
            {
               Monitor.Wait(this);
            }

            if (m_stop)
            {
                data = default(T);
                return false;
            }

            data = m_ring[m_write];
            
            return true;
         }        
      }

      public void PutUnlock()
      {
         lock (this)
         {
            m_len++;
            m_write = (m_write + 1) % m_ring.Length;
            Monitor.Pulse(this);
         }
      }
   }
}
