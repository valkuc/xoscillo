using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class DecodeFSK : Form, DataBlockAware
   {
       DataBlock m_db;


      BitDecoder bitDecoder = new BitDecoder();

      public DecodeFSK()
      {
         InitializeComponent();
      }

      public string ReverseString(string s)
      {
         char[] arr = s.ToCharArray();
         Array.Reverse(arr);
         return new string(arr);
      }


      public int FindClockedSignal(List<int> deltas, int offset)
      {
         int run = 0;

         for (; offset < deltas.Count; offset++)
         {
             if (deltas[offset] != 0)
             {
                 bitDecoder.SetClock((byte)deltas[offset]);

                 for (int j = offset + 1; j < deltas.Count; j++)
                 {
                     if (bitDecoder.IsClock((byte)deltas[j]))
                     {
                         run++;
                     }
                     else
                     {
                         break;
                     }
                 }

                 return run;
             }
         }

         return 0;
      }

      public int ScanForClockedSignal(List<int> Zeroes, int ConsecutiveOnes )
      {
         for( int i=0;i<Zeroes.Count;i++)
         {
            int offset = FindClockedSignal(Zeroes, i);
            if ( offset > ConsecutiveOnes )
            {
               return i;
            }
         }
         return -1;
      }

      public string ExtractBits(List<int> delta)
      {   
         string bits = "";

         int offset = 0;

         for (; offset < delta.Count; offset++)
         {
             int consecutive = FindClockedSignal(delta, offset);
             if (consecutive > 10)
             {
                 bitDecoder.SetClock((byte)delta[offset]);
                 for (; offset < delta.Count; offset++)
                 {
                     BIT_RESULT res = bitDecoder.DecodeBit((byte)delta[offset]);

                     if (res == BIT_RESULT.BIT_ZERO)
                        bits += "0";
                     else if (res == BIT_RESULT.BIT_ONE)
                         bits += "1";
                     else if (res == BIT_RESULT.BIT_ERROR)
                     {
                         bits += "--";
                         break;
                     }
                 }
             }
         }
         return bits;
      }

      public string Decode4BitsToString(string data, bool parityCheck)
      {
         string lut = "0123456789:;<=>?";
         string parlut = "01";

         string str="";

         int i=0;

         //look for sentinel
         {
            int val = 0;
            for(i=0;i<data.Length; i++)
            {
               val = (val<<1) & 0x1f;

               char c = data[i];

               if (c=='1')
                  val |= 1;

               if (val== 26)
               {
                  str += ";";
                  i++;
                  break;
               }
            }
            
         }

         {
            for(;i<data.Length; )
            {

               //get value & parity bit
               uint val = 0;
               int par = 1;
               for(int j=0;j<4;j++,i++)
               {
                  if (i >= data.Length)
                     return str;

                  val>>=1;

                  char c = data[i];

                  if (c == '1')
                  {
                     val |= (1 << 3);
                     par = (par+1)&1;
                  }
               }

               //translate value into character
               char cc = (char)lut[(int)(val & 0xf)];               

               str += cc;
               
               //check parity
               if (parityCheck)
               {
                  char b = data[i];
                  if (data[i] != parlut[par])
                        str += '*';
                  i++;
               }
               
               //end?
               //if (cc == '?')
               //   break;

            }
         }

         return str;
      }

      public DataBlock GetDataBlock()
      {
          return m_db;
      }

      public void SetDataBlock(DataBlock db)
      {
          m_db = new DataBlock();
         m_db.Copy(db);

         //List<int> PeakOffsets = GetMinMax(m_db);

         List<int> PeakOffsets = new List<int>();
         List<int> deltas = new List<int>();

         PeakFinder.InitLoopThoughWave(db);
         int t = 0;        
         for (; ; )
         {
             int v = PeakFinder.LoopThoughWave();
             if (v == 255)
                 break;
             t = t + v;
             PeakOffsets.Add(t);
             deltas.Add(v);
         }
        
         db.m_Annotations = PeakOffsets.ToArray();

         int offset = 0;

         for (; offset < deltas.Count; offset++)
         {
             if (deltas[offset] != 0)
                 break;
         }

         if (offset >= 0)
         {
            //output.Text += string.Format("Clocked signal found: {0}\r\n", offset);

            string bitstream = ExtractBits(deltas);

            output.Text += bitstream + "\r\n";

            //output.Text += string.Format("Errors as FSK: {0}\r\n", DetectFSK(bitstream));
            output.Text += "\r\nText:\r\n";
            output.Text += Decode4BitsToString(bitstream, false) + "\r\n";
            //output.Text += string.Format("Alternating: \r\n");
            //output.Text += GetBitStreamFromAlternating(bitstream) + "\r\n";
            
         }
         else
         {
            output.Text += string.Format("Clocked signal not found, dumping timing values\r\n");

            int last = 0;
            foreach (int i in PeakOffsets)
            {
               output.Text += string.Format("{0:0000}: {1}", i, i-last) + "\r\n";
               last = i;
            }
         }
      }


      private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void DecodeFSK_Load(object sender, EventArgs e)
      {

      }
   }
}
