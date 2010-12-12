using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class DecodeFrequency : XOscillo.BaseForm
   {
      DataBlock m_db;
      
      public DecodeFrequency()
      {
         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return m_db;
      }

      override public void SetDataBlock(DataBlock db)
      {
         m_db = db;

         int lastValue=0;
         int run = 0;

         int average = m_db.GetAverate( 0 );

         textBox1.Text += string.Format("Average {0}\r\n", average);

         textBox1.Text += string.Format("Sample Rate {0}\r\n", db.m_sampleRate);

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

      }


   }
}
