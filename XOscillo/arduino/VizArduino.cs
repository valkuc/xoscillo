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
   public partial class VizArduino : XOscillo.VizForm
   {
      OscilloArduino oscillo;
      DataRing dr;

      Thread m_t;

      public VizArduino()
      {
         InitializeComponent();
      }

      public void Run()
      {
         DataBlock data;

         while (dr.GetRunning())
         {
            if (dr.Lock(out data))
            {
               graphControl.ScopeData.Copy(data);
               graphControl.Invalidate();

               dr.Unlock();
            }
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
         dr = new DataRing(oscillo.GetDataBlock);

         while (oscillo.Open() == false)
         {
            MessageBox.Show("Arduino with proper firmware not fount, click ok to try again", "Can't connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }

         channels.SelectedItem = "1";
         toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip2);
         play.Checked = true;
      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         dr.SetRunning(play.Checked);
         if (play.Checked)
         {
            m_t = new Thread(new ThreadStart(Run));
            m_t.Start();
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

         childForm.CopyFrom(this);//.SampleRateIndex = SampleRateIndex;

      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         dr.SetRunning(false);
         oscillo.Close();
      }

      private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
      {

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
