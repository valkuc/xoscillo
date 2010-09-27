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
   public partial class VizArduino : XOscillo.VizForm
   {
      OscilloArduino oscillo;

      Ring<DataBlock> m_ring = new Ring<DataBlock>(16);

      Thread m_threadProvider;
      Thread m_threadConsumer;
      bool m_running = false;

      public VizArduino()
      {
         InitializeComponent();
      }

      public void Provider()
      {
         DataBlock db;

         while (m_running)
         {
            m_ring.putLock( out db);
            oscillo.GetDataBlock( ref db );
            m_ring.putUnlock();
         }
      }

      public void Consumer()
      {
         DataBlock db;

         while (m_running)
         {
            m_ring.getLock( out db );
            graphControl.ScopeData.Copy(db);
            m_ring.getUnlock();
            graphControl.Invalidate();
         }
      }

      private void channels_SelectedIndexChanged(object sender, EventArgs e)
      {
         oscillo.SetNumberOfChannels(int.Parse(channels.SelectedItem.ToString()));
         Invalidate();
      }

      private void trigger_Validated(object sender, EventArgs e)
      {
         oscillo.SetTriggerVoltage(byte.Parse(trigger.Text));

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

      private void Form1_Load(object sender, EventArgs e)
      {
         oscillo = new OscilloArduino();

         while (oscillo.Open() == false)
         {
            DialogResult res = MessageBox.Show("Arduino with proper firmware not fount, scanned ports:\n" + string.Join("\n", SerialPort.GetPortNames()) + "\nclick ok to try again", "Can't connect", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            if (res == DialogResult.Cancel)
            {
               return;
            }
         }

         channels.SelectedItem = "1";
         toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip2);
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
         }
      }

      private void clone_Click(object sender, EventArgs e)
      {
         VizBuffer childForm = new VizBuffer();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = MdiParent;
         childForm.Text = Text;// +Parent.childFormNumber++;
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;

         childForm.CopyFrom(this);
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         m_running = false;
         m_threadConsumer.Join();
         m_threadProvider.Join();
         oscillo.Close();
      }

      private void graphControl_KeyDown(object sender, KeyEventArgs e)
      {
         int s = oscillo.GetSampleRate();
         if (e.KeyCode == Keys.Add)
         {
            s+=10;
         }
         else if (e.KeyCode == Keys.Subtract)
         {
            s-=10;
         }

         oscillo.SetSampleRate(s);

      }
   }
}
