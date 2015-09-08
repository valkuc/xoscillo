using System;
using System.Windows.Forms;

namespace XOscillo.Tools
{
   public partial class DecodeFrequency : Form, DataBlockAware
   {
       DataBlock m_db;
      
      public DecodeFrequency()
      {
         InitializeComponent();
      }

      private double Correlate(DataBlock db, int offset)
      {
         int length = db.GetChannelLength();

         int res = 0;
         int n1 = 0;
         int n2 = 0;

         for (int i = 0; i < length - offset; i++)
         {
            int v1 = db.GetVoltage(0, i);
            int v2 = db.GetVoltage(0, i + offset);

            n1 += v1 * v1;
            n2 += v2 * v2;

            res += v1 * v2;
         }

         return (double)res / (double)(Math.Sqrt(n1) * Math.Sqrt(n2));
      }

      public DataBlock GetDataBlock()
      {
          return m_db;
      }

      public void SetDataBlock(DataBlock db)
      {
         m_db = db;

         int lastValue=0;
         int run = 0;

         int average = m_db.GetAverage( 0 );

         textBox1.Text += string.Format("Average {0}\r\n", average);

         textBox1.Text += string.Format("Sample Rate {0}\r\n", db.m_sampleRate);

         textBox1.Text += string.Format("\r\nFreq from zeroes:\r\n", db.m_sampleRate);

         for (int i = 0; i < db.GetChannelLength(); i++)
         {
            int value = db.GetVoltage(0, i);;

            if (value > average && lastValue < average)
            {
               textBox1.Text += string.Format("{0} {1} {2}\r\n", i, run, 1.0/((float)run/(float)db.m_sampleRate) );
               run = 0;
            }

            run++;

            lastValue = value;
         }

         textBox1.Text += string.Format("\r\nFreq from self correlation:\r\n", db.m_sampleRate);

         double old = 10;
         for (int i = 0; i < db.GetChannelLength() / 2; i++)
         {
            double c = Correlate(db, i);
            if (c > old)
            {
               double waveLength = 2* (double)i / (double)db.m_sampleRate;
               textBox1.Text += string.Format("{0}\r\n", 1 / waveLength);
               break;
            }
            old = c;
         }

      }


   }
}
