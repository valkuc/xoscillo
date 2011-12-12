using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text; 

namespace XOscillo
{
   partial class PID : Form
   {
      public PID()
      {
         InitializeComponent();

         GraphAnalog ga = new GraphAnalog();
         graphControl1.SetRenderer(ga);
         ga.SetVerticalRange(255, 0, 32, "Volts");         
      }
      SerialPort serialPort;

      string ReadLine()
      {
         byte[] b = new byte[10];
         int i = 0;
         for (; ; )
         {
            serialPort.Read(b, i, 1);
            if (b[i] == '\r')
            {
               break;
            }
            i++;
         }

         return Encoding.ASCII.GetString(b);
      }


      bool Send(string str)
      {
         str += "\r";
         serialPort.Write(str);
         string s = serialPort.ReadLine();
         if (str == s)
         {
            return true;
         }

         return false;
      }


      private void button1_Click(object sender, EventArgs e)
      {
         serialPort = new SerialPort("COM12", 115200, Parity.None, 8, StopBits.One);

         serialPort.ReadTimeout = 1000;

         serialPort.Open();

         try
         {
            Send("p " + (Kp.Value / 1000.0).ToString());
            Send("i " + (Ki.Value / 1000.0).ToString());
            Send("d " + (Kd.Value / 1000.0).ToString());
            Send("s " + Setpoint.Value.ToString());
            Send("l 500");
            Send("g");
            string s_Kp = serialPort.ReadLine();
            string s_Ki = serialPort.ReadLine();
            string s_Kd = serialPort.ReadLine();
            string s_SetPoint = serialPort.ReadLine();
            string loops = serialPort.ReadLine();

            DataBlock db = new DataBlock();
            db.m_Buffer = new byte[2 * 500];

            for (int i = 0; i < 500; i++)
            {
               string str = serialPort.ReadLine();

               string[] ss = str.Split(' ');

               int val1;
               int val2;
               int.TryParse(ss[0], out val1);
               int.TryParse(ss[1], out val2);

               if (val1 < 0)
                  val1 = 0;

               db.m_Buffer[2 * i] = (byte)(val1 * 128 / Setpoint.Value);
               db.m_Buffer[(2 * i) + 1] = (byte)((val2 / 2) + 128);
            }
            db.m_sampleRate = 25;
            db.m_channels = 2;
            db.m_channelOffset = 1;
            db.m_stride = 2;
            db.m_dataType = DataBlock.DATA_TYPE.ANALOG;


            graphControl1.SetScopeData(db);
         }
         catch
         {
            graphControl1.SetScopeData(null);
         }
         serialPort.Close();
      }

      private void trackBar2_Scroll(object sender, EventArgs e)
      {

      }

   }
}
