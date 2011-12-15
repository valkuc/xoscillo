using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   class AnalogArduinoToolbar : MyToolbar
   {
      AnalogArduino oscillo;
      GraphControl graphControl;
      
      private System.Windows.Forms.ToolStripLabel triggerLabel;
      private OnlyNumbersToolStripTextBox trigger;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripLabel channelsLabel;
      private System.Windows.Forms.ToolStripComboBox channels;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripLabel sampleCountLabel;
      private OnlyNumbersToolStripTextBox sampleCount;
      

      public AnalogArduinoToolbar(AnalogArduino osc, GraphControl gc)
      {
         oscillo = osc;
         graphControl = gc;

         this.toolStrip = new System.Windows.Forms.ToolStrip();

         this.triggerLabel = new System.Windows.Forms.ToolStripLabel();
         this.trigger = new OnlyNumbersToolStripTextBox();
         
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();

         this.channelsLabel = new System.Windows.Forms.ToolStripLabel();
         this.channels = new System.Windows.Forms.ToolStripComboBox();

         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();

         this.sampleCountLabel = new System.Windows.Forms.ToolStripLabel();
         this.sampleCount = new OnlyNumbersToolStripTextBox();

         // 
         // toolStrip2
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triggerLabel,
            this.trigger,
            this.toolStripSeparator1,
            this.channelsLabel,
            this.channels,
            this.toolStripSeparator2,
            this.sampleCountLabel,
            this.sampleCount
         });
         this.toolStrip.Location = new System.Drawing.Point(3, 0);
         this.toolStrip.Name = "toolStrip2";
         this.toolStrip.Size = new System.Drawing.Size(243, 25);
         this.toolStrip.TabIndex = 1;
         this.toolStrip.Text = "toolStrip2";
         // 
         // triggerLabel
         // 
         this.triggerLabel.Name = "triggerLabel";
         this.triggerLabel.Size = new System.Drawing.Size(42, 22);
         this.triggerLabel.Text = "trigger";
         // 
         // trigger
         // 
         this.trigger.Name = "trigger";
         this.trigger.Size = new System.Drawing.Size(50, 25);
         this.trigger.Text = "0";
         this.trigger.textReady += new EventHandler(trigger_textReady);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // channelsLabel
         // 
         this.channelsLabel.Name = "channelsLabel";
         this.channelsLabel.Size = new System.Drawing.Size(42, 22);
         this.channelsLabel.Text = "channels";
         // 
         // channels
         // 
         this.channels.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
         this.channels.Name = "channels";
         this.channels.Size = new System.Drawing.Size(25, 25);
         this.channels.SelectedItem = "1";
         this.channels.SelectedIndexChanged += new System.EventHandler(this.channels_SelectedIndexChanged);

         // 
         // sampleCountLabel
         // 
         this.sampleCountLabel.Size = new System.Drawing.Size(54, 22);
         this.sampleCountLabel.Text = "samples";

         // 
         // sampleCountLabel
         // 
         this.sampleCount.Size = new System.Drawing.Size(54, 22);
         this.sampleCount.Text = "1024";
         this.sampleCount.textReady += new EventHandler(sampleCount_textReady);

         //set values
         oscillo.SetNumberOfChannels(int.Parse(this.channels.SelectedItem.ToString()));
         oscillo.SetTriggerVoltage(byte.Parse(trigger.Text));
         oscillo.SetNumberOfSamples(int.Parse(sampleCount.Text));
      }

      private void sampleCount_textReady(object sender, EventArgs e)
      {
         ToolStripTextBox textbox = sender as ToolStripTextBox;

         if (textbox != null)
         {
            oscillo.SetNumberOfSamples(int.Parse(textbox.Text));
         }
      }

      private void trigger_textReady(object sender, EventArgs e)
      {
         ToolStripTextBox textbox = sender as ToolStripTextBox;

         if (textbox != null)
         {
            int val = int.Parse(textbox.Text);
            if (val < 256)
            {
               textbox.BackColor = System.Drawing.Color.White;
               oscillo.SetTriggerVoltage((byte)val);
            }
            else
            {
               textbox.BackColor = System.Drawing.Color.Red;
            }
         }
      }

      private void channels_SelectedIndexChanged(object sender, EventArgs e)
      {
         oscillo.SetNumberOfChannels(int.Parse(channels.SelectedItem.ToString()));
         graphControl.Invalidate();
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


   }
}
