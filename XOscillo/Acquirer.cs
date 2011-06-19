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
      public GraphControl m_GraphControl = null;

      DataBlockRing m_ring = new DataBlockRing(16);

      Thread m_threadProvider = null;
      Thread m_threadConsumer = null;
      bool m_running = false;

      public bool Open( Oscillo os, GraphControl gc)
      {
         m_Oscillo = os;
         m_GraphControl = gc;

         ManualSerialPortSelection msps = new ManualSerialPortSelection(os);

         if (msps.TryDetection() == false)
         {
            msps.ShowDialog();
         }

         if ( os.IsOpened() == false )
         {
            return false;
         }

         return m_Oscillo.Ping();
      }

      public void Close()
      {
         Stop();

         if (m_Oscillo != null)
         {
            m_Oscillo.Close();
         }
      }

      private void Provider()
      {
         DataBlock db;

         while (m_running)
         {
            m_ring.putLock(out db);

            while (m_running)
            {
               try
               {
                  m_Oscillo.GetDataBlock(ref db);
                  if (db.m_result == DataBlock.RESULT.OK)
                  {
                     break;
                  }
               }
               catch
               {
               }

               m_Oscillo.Reset();

            }

            m_ring.putUnlock();
         }
      }

      private void Consumer()
      {
         while (m_running)
         {
            DataBlock db;
            m_ring.GetFirstElementButDoNotRemoveIfLastOne(out db);
            m_GraphControl.SetScopeData( db ); 
         }
      }

      public void Play()
      {
         m_running = true;
         m_threadProvider = new Thread(new ThreadStart(Provider));
         m_threadProvider.Name = "Provider";
         m_threadProvider.Start();

         m_threadConsumer = new Thread(new ThreadStart(Consumer));
         m_threadConsumer.Name = "Consumer";
         m_threadConsumer.Start();
      }

      public void Stop()
      {
         m_running = false;

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
      }
   }
}
