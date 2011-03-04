using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XOscillo
{
   class Acquirer
   {
      Oscillo m_Oscillo = null;
      GraphControl m_GraphControl = null;

      Ring<DataBlock> m_ring = new Ring<DataBlock>(16);

      Thread m_threadProvider = null;
      Thread m_threadConsumer = null;
      bool m_running = false;

      public bool Open( Oscillo os, GraphControl gc)
      {
         m_Oscillo = os;
         m_GraphControl = gc;

         ManualSerialPortSelection msps = new ManualSerialPortSelection(os);

         msps.ShowDialog();

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

            bool gotdata = false;
            do
            {
               try
               {
                  gotdata = m_Oscillo.GetDataBlock(ref db);
               }
               catch
               {
                  m_Oscillo.Reset();
               }
            } while ( (gotdata == false) & (m_running == true) );

            m_ring.putUnlock();
         }
      }

      private void Consumer()
      {
         while (m_running)
         {
            m_GraphControl.SetScopeData( m_ring.GetFirstElementButDoNotRemoveIfLastOne() ); 
            m_GraphControl.Invalidate();
         }
      }

      public void Play()
      {
         m_running = true;
         m_threadProvider = new Thread(new ThreadStart(Provider));
         m_threadProvider.Start();

         m_threadConsumer = new Thread(new ThreadStart(Consumer));
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
