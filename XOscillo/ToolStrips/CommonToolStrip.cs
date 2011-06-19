using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public class CommonToolStrip
   {
      VizForm vizForm;
      private Acquirer m_acquirer = null;

      private System.Windows.Forms.ToolStrip toolStrip;
      private System.Windows.Forms.ToolStripButton clone;
      private System.Windows.Forms.ToolStripButton play = null;
      private System.Windows.Forms.ToolStripLabel timeLabel;
      public System.Windows.Forms.ToolStripComboBox time;
      private System.Windows.Forms.ToolStripButton fft;
      private GraphControl graphControl;

      public System.EventHandler selectedIndexChanged;

      public CommonToolStrip(VizForm vf, Acquirer acq, GraphControl gc)
      {
         vizForm = vf;
         m_acquirer = acq;
         graphControl = gc;

         this.toolStrip = new System.Windows.Forms.ToolStrip();
         this.timeLabel = new System.Windows.Forms.ToolStripLabel();
         this.clone = new System.Windows.Forms.ToolStripButton();
         this.play = new System.Windows.Forms.ToolStripButton();
         this.fft = new System.Windows.Forms.ToolStripButton();
         this.time = new System.Windows.Forms.ToolStripComboBox();


         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeLabel,
            this.time,
            this.clone,
            this.play,
            this.fft});
         this.toolStrip.Location = new System.Drawing.Point(0, 0);
         this.toolStrip.Name = "toolStrip2";
         this.toolStrip.Size = new System.Drawing.Size(497, 25);
         this.toolStrip.TabIndex = 1;
         this.toolStrip.Text = "toolStrip2";

         // 
         // time
         // 
         this.time.Name = "time";
         this.time.Size = new System.Drawing.Size(75, 25);
         this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);
         
         // 
         // clone
         // 
         this.clone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.clone.Image = null;// ((System.Drawing.Image)(resources.GetObject("clone.Image")));
         this.clone.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.clone.Name = "clone";
         this.clone.Size = new System.Drawing.Size(42, 22);
         this.clone.Text = "Clone";
         this.clone.Click += new System.EventHandler(this.clone_Click);

         // 
         // play
         // 
         if (m_acquirer != null)
         {
            this.play.CheckOnClick = true;
            this.play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.play.Image = global::XOscillo.Properties.Resources.play;
            this.play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.play.Margin = new System.Windows.Forms.Padding(1);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(23, 23);
            this.play.Text = "toolStripButton2";
            this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
            this.play.Checked = true;
         }
         // 
         // fft
         // 
         this.fft.CheckOnClick = true;
         this.fft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.fft.Image = null;// ((System.Drawing.Image)(resources.GetObject("fft.Image")));
         this.fft.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fft.Name = "fft";
         this.fft.Size = new System.Drawing.Size(30, 22);
         this.fft.Text = "FFT";
         this.fft.CheckStateChanged += new System.EventHandler(this.fft_CheckStateChanged);

         
      }

      public ToolStrip GetToolStrip()
      {
         return toolStrip;
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {         
         if (selectedIndexChanged != null)
         {
            selectedIndexChanged(sender, e);
         }

         graphControl.SetSecondsPerDivision(float.Parse(time.SelectedItem.ToString()));
      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         if (play.Checked)
         {
            m_acquirer.Play();

            play.Image = global::XOscillo.Properties.Resources.pause;
         }
         else
         {
            play.Image = global::XOscillo.Properties.Resources.play;

            m_acquirer.Stop();
         }
      }

      private void fft_CheckStateChanged(object sender, EventArgs e)
      {
         graphControl.DrawFFT(fft.Checked);
      }

      private void clone_Click(object sender, EventArgs e)
      {
         vizForm.Clone();
      }


   }
}
