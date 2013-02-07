using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
    class PeakFinder
    {
        static byte extreme = 0;
        static byte extreme_time = 0;

        static bool find_max = true;
        static byte threshold = 5;

        static byte delta_time = 0;
        static byte fine = 0;

        static byte zero = 0;
        static DataBlock m_db;
        static int adc_ptr;

        public static void InitLoopThoughWave(DataBlock db)
        {
            find_max = true;
            delta_time = 0;
            fine = 0;
            extreme = 0;
            extreme_time = 0;

            m_db = db;
            adc_ptr = 0;

            zero = m_db.GetAverate(0);
        }

        static byte adcRead()
        {
            return m_db.GetVoltage(0, adc_ptr++);
        }

        public static byte LoopThoughWave()
        {
            for (int timeout = 0; timeout < 65535; timeout++)
            {
                byte v;
                if (adc_ptr >= m_db.GetChannelLength())
                    v = 0;
                else
                    v = adcRead();

                if (extreme == v)
                {
                    fine++;
                }

                if (
                    ((find_max == true) && (v > extreme)) ||
                    ((find_max == false) && (v < extreme))
                  )
                {
                    extreme = v;
                    extreme_time = delta_time;
                    fine = 0;
                }


                if (find_max == true)
                {
                    if (v < (zero - threshold))
                    {
                        find_max = false;

                        extreme_time += (byte)(fine / 2);

                        delta_time -= extreme_time;

                        delta_time++;
                        return extreme_time;
                    }
                }
                else
                {
                    if (v > (zero + threshold))
                    {
                        find_max = true;

                        extreme_time += (byte)(fine / 2);

                        delta_time -= extreme_time;

                        delta_time++;
                        return extreme_time;
                    }
                }

                delta_time++;

            }

            return 255;
        }
    
    }
}
