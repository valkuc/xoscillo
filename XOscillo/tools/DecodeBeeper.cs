using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class DecodeBeeper : BaseForm
   {
      DataBlock m_db;
      Dictionary<int, int> m_histo = new Dictionary<int, int>();

      override public DataBlock GetDataBlock()
      {
         return m_db;
      }
      public DecodeBeeper()
      {
         InitializeComponent();
      }

      override public void SetDataBlock(DataBlock db)
      {
          m_db = new DataBlock();
         m_db.Copy(db);

         int time = 0;

         bool oldBit = false;
         for (int i = 0; i < m_db.GetChannelLength(); i++)
         {
            bool bit = m_db.GetVoltage(0,i)>10;

            if ( oldBit == false && bit == true )
            {
               Process(false, time);
               time=0;
            }
            else if (oldBit == true && bit == false)
            {
               Process(true, time);
               time = 0;
            }
            else
            {
               time++;
            }

            oldBit = bit;
         }


         textBox1.Text += "histo\r\n";

         List<int> keys = new List<int>();
         foreach (int i in m_histo.Keys)
         {
            keys.Add(i);
         }

         keys.Sort();

         foreach (int i in keys)
         {
            int v = m_histo[i];
            textBox1.Text += string.Format("{0:000} {1:000} ", i, v) + "\r\n";
         }

      }


      public void Process( bool bit, int time )
      {
         if (m_histo.ContainsKey( time ) == false)
         {
            m_histo[time] = 0;
         }

         m_histo[time]++;

         //textBox1.Text += bit.ToString() + "   " + time.ToString() + "\r\n";
         textBox1.Text += string.Format("{0:000} ", time);
      }
   }
}
