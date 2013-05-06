using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XOscillo
{
   public class Acquirer
   {
      public Oscillo m_Oscillo = null;
      public Consumer m_GraphControl = null;

      Ring<DataBlock> m_ring = new Ring<DataBlock>(16);

      Thread m_threadProvider = null;
      Thread m_threadConsumer = null;

      bool running = false;

      public bool Open(Oscillo os, Consumer gc)
      {
         m_Oscillo = os;
         m_GraphControl = gc;

         return m_Oscillo.Ping();
      }

      public void Close()
      {
         Stop();
         running = false;

         if (m_Oscillo != null)
         {
            m_Oscillo.Close();
         }
      }

      public void SetBuffering(int i)
      {
          if (running)
          {
              Stop();
              m_ring = new Ring<DataBlock>(i);
              Play();
          }
          else
          {
              m_ring = new Ring<DataBlock>(i);
          }
      }

      private void Provider()
      {
         DataBlock db;

         for(;running;)
         {
            if (m_ring.putLock(out db))
            {
                while (true)
                {
                    try
                    {
                        m_Oscillo.GetDataBlock(ref db);
                        break;
                    }
                    catch
                    {
                    }
                }
                m_ring.putUnlock();
                m_Oscillo.Reset();
            }
         }
      }

      private void Consumer()
      {
          DataBlock db;

          for (; running; )
          {
            if (m_ring.getLock(out db))
            {
                m_GraphControl.SetDataBlock(db);
                m_ring.getUnlock();
            }
         }
      }

      public void Play()
      {
          if (running == true)
          {
              return;
          }

          m_ring.Start();
         running = true;

         m_threadProvider = new Thread(new ThreadStart(Provider));
         m_threadProvider.Name = "Provider";
         m_threadProvider.Start();

         m_threadConsumer = new Thread(new ThreadStart(Consumer));
         m_threadConsumer.Name = "Consumer";
         m_threadConsumer.Start();
      }

      public void Stop()
      {
        if (running == false)
        {
            return;
        }

         //so consumer can finish
         m_ring.Stop();
         running = false;
         DebugConsole.Instance.Add("Stopping threads...");

         if (m_threadConsumer != null)
         {
            m_threadConsumer.Join();
            m_threadConsumer = null;
         }

         if (m_threadProvider != null)
         {
            m_threadProvider.Join();
            m_threadProvider = null;
         }
         DebugConsole.Instance.Add("OK\n");
      }
   }
}
