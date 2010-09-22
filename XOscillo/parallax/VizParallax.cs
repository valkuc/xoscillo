using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace XOscillo
{
   public partial class VizParallax : VizForm
   {
      OscilloSerial oscillo;
      DataRing dr;

      Thread m_t;

      public VizParallax( )
      {
         oscillo = new OscilloSerial();
         dr = new DataRing( oscillo.GetDataBlock );
         oscillo.Open();
         oscillo.Ping();

         InitializeComponent();

         foreach (UInt32 t in oscillo.GetSampleRates() )
         {
            comboTime.Items.Add(t.ToString());
         }
         comboTime.SelectedIndex = 5;

         comboEdge.Items.Add("rise");
         comboEdge.Items.Add("fall");
         comboEdge.SelectedIndex = 0;

         comboChannelTrigger.Items.Add("tr:CH1");
         comboChannelTrigger.Items.Add("tr:CH2");
         comboChannelTrigger.SelectedIndex = 0;

      }

      override public bool Save(string filename)
      {
         display.ScopeData.Save(filename);
         return true;
      }

      override public DataBlock GetDataBlock()
      {
         return display.ScopeData;
      }

      private void display_Load(object sender, EventArgs e)
      {
         oscillo.Reset();
         oscillo.Ping();
         play.Checked = true;
      }

      public void Run()
      {
         DataBlock data;

         while ( dr.GetRunning() )
         {
            //display.Invalidate();
            //continue;
            if (dr.Lock(out data))
            {
               display.ScopeData.Copy( data );

               display.Invalidate();
               dr.Unlock();
            }
         }
      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         dr.SetRunning(play.Checked);
         if (play.Checked)
         {
            m_t = new Thread(new ThreadStart(Run));
            m_t.Start();
         }
      }

      private void comboTime_SelectedIndexChanged(object sender, EventArgs e)
      {
         int rate = oscillo.GetSampleRates()[comboTime.SelectedIndex];
         display.SetSecondsPerDivision( (1.0f / (float)rate *50.0f) );
         oscillo.SetSampleRate( rate );
         Invalidate();
      }

      private void comboEdge_SelectedIndexChanged(object sender, EventArgs e)
      {
			if ((string)comboEdge.SelectedItem == "rise")
			{
            oscillo.edge = Edge.EDGE_RAISING;
			}
			else
			{
            oscillo.edge = Edge.EDGE_FALLING;
			}
      }

      private void comboChannelTrigger_SelectedIndexChanged(object sender, EventArgs e)
      {
         if ((string)comboChannelTrigger.SelectedItem == "tr:CH1")
         {
            oscillo.triggerChannel = TriggerChannel.TC_CH1;
         }
         else
         {
            oscillo.triggerChannel = TriggerChannel.TC_CH2;
         }

      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         dr.SetRunning(false);
         oscillo.Close();
      }

      private void save_Click(object sender, EventArgs e)
      {
/*
         VizBuffer2 childForm = new VizBuffer2();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = MdiParent;
         childForm.Text = Text;// +Parent.childFormNumber++;
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
         childForm.SetDataBlock(display.ScopeData);
*/         
         //oscillo.Save( "c:\\caca.xml", db );
      }

      private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
      {
         Invalidate();
      }
   }
}
