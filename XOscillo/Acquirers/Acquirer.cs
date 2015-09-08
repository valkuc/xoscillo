using System;
using System.Threading;

namespace XOscillo.Acquirers
{
    public class Acquirer
    {
        private int SampleID = 0;

        public Oscillo m_Oscillo = null;
        public Consumer m_GraphControl = null;

        Ring<DataBlock> m_ring = new Ring<DataBlock>(16);

        Thread m_threadProvider = null;
        Thread m_threadConsumer = null;

        private bool running = false;

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

            while(running)
            {
                if (m_ring.PutLock(out db))
                {
                    while(running)
                    {
                        try
                        {
                            //set some standard values
                            db.m_result = DataBlock.RESULT.TIMEOUT;
                            db.m_sample = SampleID++;
                            db.m_start = DateTime.Now;

                            bool res = m_Oscillo.GetDataBlock(ref db);

                            db.m_stop = DateTime.Now;
#if DEBUG
                            //Console.WriteLine("{3} {0} {1}ms  {2}", db.m_sample, (db.m_stop - db.m_start).Milliseconds, m_ring.GetLength(), res);
#endif
                            if (res == true)
                            {
                                break;
                            }

                            m_Oscillo.Reset();
#if DEBUG
                            Console.WriteLine("Reset");
#endif
                        }
                        catch(Exception e)
                        {
#if DEBUG
                            Console.WriteLine("{0} ", e.Message);
#endif
                            m_Oscillo.Reset();
                        }
                    }
                    m_ring.PutUnlock();
                }
            }
        }

        private void Consumer()
        {
            DataBlock db;

            for (; running; )
            {
                if (m_ring.GetLock(out db))
                {
                    m_GraphControl.SetDataBlock(db);
                    m_ring.GetUnlock();
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

            m_Oscillo.Start();

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

            m_Oscillo.Stop();
            DebugConsole.Instance.Add("OK\n");
        }
    }
}
