using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class DecodeFSK : BaseForm
   {
      DataBlock m_db;

      public DecodeFSK()
      {
         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return m_db;
      }

      public int Error(int a, int b)
      {
         int e = 0;
         if (a > b)
         {
            e = a - b;
            return (e * 100 / a);
         }
         else
         {
            e = b - a;
            return (e * 100 / b);
         }
      }

      public string ReverseString(string s)
      {
         char[] arr = s.ToCharArray();
         Array.Reverse(arr);
         return new string(arr);
      }

      public bool IsNoise(byte b)
      {
         return (b < 140) && (b > 120);
      }

      override public void SetDataBlock(DataBlock db)
      {
         m_db = new DataBlock();
         m_db.Copy(db);

         byte max, min;

         int length = m_db.m_Buffer.Length / m_db.m_channels;
         int head = (m_db.m_channels-1)*length;

         // trim head
         int i = 0;
         for (;i<length; i++)
         {
            if ( IsNoise(m_db.m_Buffer[head + i]) == false )
            {
               head += i;
               length -= i;
               break;
            }
         }
         
         //trim tail
         for (; head < length; length--)
         {
            if ( IsNoise(m_db.m_Buffer[head + length-1]) == false )
            {
               break;
            }
         }
          

         ComputeMaxMin(m_db.m_Buffer, head, length, out max, out min);

         output.Text += string.Format("Range ({0}, {1})\r\n",  max, min);

         List<int> peaks;
         ComputeMaxMin(m_db.m_Buffer, head, length, out peaks);

         // compute deltas
         List<int> deltas = new List<int>();
         for (int p = 1; p < peaks.Count; p++)
         {
            deltas.Add(peaks[p] - peaks[p - 1]);
         }

         int delta_old = 0;

         int v = -1;
         int l = 0;
         int ee = 0;
         int zero = -1;

         string s = "";

         for (int p=1; p < peaks.Count;p++ )
         {
            int delta = peaks[p] - peaks[p - 1];

            int err = Error(delta, delta_old);

            if (v == -1)
            {
               if (err < 20)
               {
                  l++;
               }
               else
               {
                  l = 0;
               }
               if (l > 10)
               {
                  v = 0;
                  zero = delta;
               }
            }
            else
            {
               /*
               if (err > 40)
               {
                  if (v == 0) v = 1; else v = 0;
               }
                */

               //ee = Error(delta, zero) - Error(delta * 2, zero);

               if ( delta >= 10 )
               {
                  v = 0;
               }
               else
               {
                  v = 1;
               }

               s += v.ToString();
            }

            output.Text += string.Format("{0:0000}: {1:00}  {3:0} {4}%\r\n", peaks[p], delta, err, v, ee);

            delta_old = delta;
         }

         output.Text += "String\r\n";
         output.Text += s + "\r\n";
         output.Text += "Reversed string\r\n";
         output.Text += ReverseString(s) + "\r\n";

         
      }

      public void ComputeMaxMin(byte [] data, int offset, int size, out byte max, out byte min)
      {
         max = 0;
         min = 255;
         for (int i = 0; i < size; i++)
         {
            byte value = data[i + offset];

            if (value > max)
               max = value;

            if (value < min)
               min = value;
         }
      }

      public void ComputeMaxMin(byte [] data, int offset, int size, out List<int> maxmin)
      {
         maxmin = new List<int>();

         byte old = data[ offset ];

         for (int i = 0; i < size; i++)
         {

            while( data[i + offset]<=old )
            {
               old = data[i + offset];
               i++;
               if (i >= size)
                  return;
            }
            maxmin.Add(i);

            while( data[i + offset]>=old )
            {
               old = data[i + offset];
               i++;
               if (i >= size)
                  return;
            }
            maxmin.Add(i);
         }
         
      }

      //public void ComputeMaxMin(byte [] data, int offset, int size, out byte max, out byte min)



      private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void DecodeFSK_Load(object sender, EventArgs e)
      {

      }
   }
}
