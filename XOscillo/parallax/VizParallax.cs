using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace XOscillo
{
   public partial class VizParallax : VizForm
   {
      SerialParallax oscillo;

      Ring<DataBlock> m_ring = new Ring<DataBlock>(16);

      Thread m_threadProvider;
      Thread m_threadConsumer;
      bool m_running = false;

      public VizParallax( )
      {
         oscillo = new SerialParallax();

         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return graphControl.GetScopeData();
      }

      public void Provider()
      {
         DataBlock db;

         while (m_running)
         {
            m_ring.putLock(out db);

            oscillo.GetDataBlock(ref db);
            m_ring.putUnlock();
         }
      }

      public void Consumer()
      {
         while (m_running)
         {
            graphControl.SetScopeData( m_ring.GetFirstElementButDoNotRemoveIfLastOne() ); 
            graphControl.Invalidate();
         }
      }

      private void VizParallax_Load(object sender, EventArgs e)
      {
         oscillo = new SerialParallax();
         while (oscillo.Open() == false)
         {
            DialogResult res = MessageBox.Show("Parallax USB oscilloscope not fount, scanned ports:\n" + string.Join("\n", SerialPort.GetPortNames()) + "\nclick ok to try again", "Can't connect", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            if (res == DialogResult.Cancel)
            {
               this.Close();
            }
         }
         oscillo.Ping();

         time.Items.Add(0.1);
         time.Items.Add(0.05);
         time.Items.Add(0.02);
         time.Items.Add(0.01);
         time.Items.Add(0.005);
         time.Items.Add(0.002);
         time.Items.Add(0.001);
         time.Items.Add(0.0005);
         time.Items.Add(0.0002);
         time.Items.Add(0.0001);
         time.Items.Add(0.00005);
         time.Items.Add(0.00002);
         time.Items.Add(0.00001);
         time.Items.Add(0.000005);
         time.SelectedIndex = 10;

         triggerMode.SelectedIndex = 1;
         play.Checked = true;
      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         m_running = play.Checked;

         if (play.Checked)
         {
            m_threadProvider = new Thread(new ThreadStart(Provider));
            m_threadProvider.Start();

            m_threadConsumer = new Thread(new ThreadStart(Consumer));
            m_threadConsumer.Start();

            play.Image = global::XOscillo.Properties.Resources.pause;
         }
         else
         {
            play.Image = global::XOscillo.Properties.Resources.play;
            m_threadConsumer.Join();
            m_threadProvider.Join();
            oscillo.Reset();
         }
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         m_running = false;
         m_threadConsumer.Join();
         m_threadProvider.Join();
         oscillo.Close();
      }

      private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
      {
         Invalidate();
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {
         
         int sampleRate = 0;
         switch (time.SelectedIndex)
         {
            case 0: sampleRate = 250; break;
            case 1: sampleRate = 500; break;
            case 2: sampleRate = 1000; break;
            case 3: sampleRate = 2500; break;
            case 4: sampleRate = 5000; break;
            case 5: sampleRate = 10000; break;
            case 6: sampleRate = 25000; break;
            case 7: sampleRate = 50000; break; // 0.001
            case 8: sampleRate = 100000; break;
            case 9: sampleRate = 250000; break;
            case 10: sampleRate = 500000; break;
            case 11: sampleRate = 1000000; break;
            case 12: sampleRate = 2500000; break; // 0.0001f
            case 13: sampleRate = 5000000; break;
         }
         oscillo.SetSamplingRate(sampleRate);

         float secondsPerDiv = float.Parse(time.Items[time.SelectedIndex].ToString());
         graphControl.SetSecondsPerDivision(secondsPerDiv);
         
         //oscillo.SetSamplingRate(250000);
         //graphControl.SetSecondsPerDivision(0.0001f);
      }

      private void triggerMode_SelectedIndexChanged(object sender, EventArgs e)
      {
         switch (triggerMode.SelectedIndex)
         {
            case 0:
               oscillo.triggerChannel = TriggerChannel.TC_CH1;
               oscillo.edge = Edge.EDGE_RAISING;
               oscillo.externalTrigger = false;
               break;
            case 1:
               oscillo.triggerChannel = TriggerChannel.TC_CH1;
               oscillo.edge = Edge.EDGE_FALLING;
               oscillo.externalTrigger = false;
               break;
            case 2:
               oscillo.triggerChannel = TriggerChannel.TC_CH2;
               oscillo.edge = Edge.EDGE_RAISING;
               oscillo.externalTrigger = false;
               break;
            case 3:
               oscillo.triggerChannel = TriggerChannel.TC_CH2;
               oscillo.edge = Edge.EDGE_FALLING;
               oscillo.externalTrigger = false;
               break;
            case 4:
               oscillo.externalTrigger = true;
               break;

         }
         
         Invalidate();
      }

      private void trigger_Validated(object sender, EventArgs e)
      {
         oscillo.TriggerVoltage = byte.Parse(trigger.Text);
      }

      private void trigger_Validating(object sender, CancelEventArgs e)
      {
         int value;
         if (int.TryParse(trigger.Text, out value))
         {
            if (value >= 0 && value <= 255)
            {
               trigger.BackColor = Color.White;
               return;
            }
         }

         trigger.BackColor = Color.Red;
         e.Cancel = true;
      }

      private void fft_CheckedChanged(object sender, EventArgs e)
      {
         graphControl.DrawFFT( fft.Checked );
      }

      private void clone_Click(object sender, EventArgs e)
      {
         VizBuffer childForm = new VizBuffer();
         childForm.MdiParent = MdiParent;
         childForm.Text = Text;// +Parent.childFormNumber++;
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
         childForm.CopyFrom(this);
      }
   }
}
