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
         if (a < b)
         {
            int c = a;
            a = b;
            b = c;
         }
         return ((b - a) * 100 ) / b;
      }

      public string ReverseString(string s)
      {
         char[] arr = s.ToCharArray();
         Array.Reverse(arr);
         return new string(arr);
      }

      public bool IsNoise(byte b)
      {
         int average = m_db.GetAverate(0);

         return ( b < ( average + 5 ) ) && ( b > (average - 5) );
      }


      public List<int> GetMinMax()
      {
         int average = m_db.GetAverate(0);

         List<int> MinMax = new List<int>();

         byte lastValue = m_db.GetVoltage(0, 0);

         int Tip = average;
         int Run = 0;

         for (int i = 1; i < m_db.GetChannelLength(); i++)
         {
            byte value = m_db.GetVoltage(0, i);

            // up
            if (value > average  && lastValue <= average)
            {
               if ( value - Tip > 10 )
                  MinMax.Add(Run);
               Tip = value;
            }

            //down
            if (value < average && lastValue >= average)
            {
               if ( Tip - value  > 10)
                  MinMax.Add(Run);
               Tip = value;
            }

            if ( value > average )
            {
               //find max
               if ( value > Tip )
               {
                  Tip = value;
                  Run = i;
               }
            }

            if ( value < average )
            {
               if ( value < Tip )
               {
                  Tip = value;
                  Run = i;
               }
            }            

            lastValue = value;
         }

         return MinMax;
      }

      public int FindClockedSignal(List<int> deltas, int offset)
      {
         int run = 0;
         int v = deltas[offset];
         for (int j = offset + 1; j < deltas.Count; j++)
         {
            int val = deltas[j];
            if (Error(v, val*2) < 30)
            {
               run++;
               v = val*2;
            }
            else if (Error(v, val) < 30)
            {
               run++;
               v = val;
            }
            else
            {
               break;
            }
         }

         return run;
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

      public List<int> ComputeDeltas(List<int> Peaks)
      {
         List<int> deltas = new List<int>();
         for (int i = 1; i < Peaks.Count; i++)
         {
            deltas.Add(Peaks[i] - Peaks[i - 1]);
         }

         return deltas;
      }


      public string ExtractBits(List<int> delta, int offset, int longDuration)
      {   
         int zeroDuration = longDuration;
         int oneDuration = zeroDuration / 2;

         string bits = "";

         for (int i = offset; i < delta.Count; i++)
         {
            int average = (zeroDuration + oneDuration) /2;

            int duration = delta[i];

            if (duration > average * 4)
            {
               bits += "p";
               continue;
            }

            if (duration > average)
            {
               if (Error(duration, zeroDuration) < 40)
               {
                  bits += "0";
                  zeroDuration = (zeroDuration + duration)/2;
               }
               else
               {
                  bits += "*";
               }
            }
            else
            {
               if (Error(duration, oneDuration) < 40)
               {
                  bits += "1";
                  oneDuration = (oneDuration + duration) / 2;
               }
               else
               {
                  bits += "*";
               }
            }

          
         }

         return bits;
      }

      public int DetectFSK(string bitstream)
      {
         int err = 0;

         bool wasOne = false;

         for(int i=0;i<bitstream.Length;i++)
         {
            if ( wasOne )
            {   
               if ( bitstream[i] != '1' )
               {                  
                  err++;
               }
               wasOne = false;
            }
            else
            {
               wasOne = (bitstream[i] == '1' );
            }
         }

         return err;
      }

      string GetBitStreamFromFSK(string bitstream)
      {
         string outdata = "";

         bool wasOne = false;

         for (int i = 0; i < bitstream.Length; i++)
         {
            if (wasOne)
            {
               if (bitstream[i] == '1')
               {
                  outdata += "1";
               }
               else
               {
                  outdata += "*";
               }
               wasOne = false;
            }
            else
            {               
               if (bitstream[i] == '1')
               {
                  wasOne = true;
               }
               else
               {
                  outdata += "0";
               }
            }
         }
         return outdata;
      }

      public string GetBitStreamFromAlternating(string bitstream)
      {
         string outdata = "";

         for (int i = 1; i < bitstream.Length; i+=2)
         {
            char last = bitstream[i-1];
            char cur = bitstream[i];

            if (last=='0' && cur == '1')
            {
               outdata += "1";
            }
            else if (last == '1' && cur == '0')
            {
               outdata += "0";
            }
            else
            {
               outdata += "*";
            }
         }

         return outdata;
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

      override public void SetDataBlock(DataBlock db)
      {
         m_db = new DataBlock();
         m_db.Copy(db);

         List<int> PeakOffsets = GetMinMax();

         List<int> deltas = ComputeDeltas(PeakOffsets);

         int offset = ScanForClockedSignal(deltas, 5);
         if (offset >= 0)
         {
            output.Text += string.Format("Clocked signal found: {0}\r\n", offset);

            string bitstream = ExtractBits(deltas, offset, deltas[offset]);

            output.Text += bitstream + "\r\n";

            output.Text += string.Format("Errors as FSK: {0}\r\n", DetectFSK(bitstream));
            output.Text += GetBitStreamFromFSK(bitstream) + "\r\n";
            output.Text += "\r\nText:\r\n";
            output.Text += Decode4BitsToString(GetBitStreamFromFSK(bitstream), true) + "\r\n";
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
