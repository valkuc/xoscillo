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
      SerialArduino oscillo;

      Acquirer m_Acq = new Acquirer();

      public VizArduino()
      {
         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return graphControl.GetScopeData();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         oscillo = new SerialArduino();

         if (m_Acq.Open(oscillo, graphControl) == false)
         {
            this.BeginInvoke(new MethodInvoker(this.Close));
            return;
         }

         time.Items.Add(1.0);
         time.Items.Add(0.5);
         time.Items.Add(0.2);
         time.Items.Add(0.1);
         time.Items.Add(0.05);
         time.Items.Add(0.02);
         time.Items.Add(0.01);
         time.Items.Add(0.005);
         time.Items.Add(0.002);
         time.Items.Add(0.001);
         time.Items.Add(0.0005);
         time.Items.Add(0.0002);
         time.SelectedIndex = 10;

         channels.SelectedItem = "1";
         play.Checked = true;
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         m_Acq.Close();
      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         if (play.Checked)
         {
            m_Acq.Play();

            play.Image = global::XOscillo.Properties.Resources.pause;
         }
         else
         {
            play.Image = global::XOscillo.Properties.Resources.play;

            m_Acq.Stop();
         }
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {
         graphControl.SetSecondsPerDivision( float.Parse(time.SelectedItem.ToString()));
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

      private void fft_CheckStateChanged(object sender, EventArgs e)
      {
         graphControl.DrawFFT( fft.Checked );
      }

      private void clone_Click(object sender, EventArgs e)
      {
         Clone();
      }

      private void cof_Validated(object sender, EventArgs e)
      {
         graphControl.SetLowPassCutOffFrequency(double.Parse(cof.Text));
      }

      private void cof_Validating(object sender, CancelEventArgs e)
      {
         int value;
         if (int.TryParse(cof.Text, out value))
         {
            trigger.BackColor = Color.White;
         }
         else
         {
            trigger.BackColor = Color.Red;
            e.Cancel = true;
         }
      }

   }
}
