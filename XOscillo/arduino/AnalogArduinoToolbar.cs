using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   class AnalogArduinoToolbar
   {
      AnalogArduino oscillo;
      GraphControl graphControl;
      
      private System.Windows.Forms.ToolStrip toolStrip;
      private System.Windows.Forms.ToolStripLabel triggerLabel;
      private System.Windows.Forms.ToolStripTextBox trigger;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel2;
      private System.Windows.Forms.ToolStripComboBox channels;

      public AnalogArduinoToolbar(AnalogArduino osc, GraphControl gc)
      {
         oscillo = osc;
         graphControl = gc;

         this.toolStrip = new System.Windows.Forms.ToolStrip();
         this.triggerLabel = new System.Windows.Forms.ToolStripLabel();
         this.trigger = new System.Windows.Forms.ToolStripTextBox();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
         this.channels = new System.Windows.Forms.ToolStripComboBox();

         // 
         // toolStrip2
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triggerLabel,
            this.trigger,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.channels});
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
         this.trigger.Validating += new System.ComponentModel.CancelEventHandler(this.trigger_Validating);
         this.trigger.Validated += new System.EventHandler(this.trigger_Validated);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripLabel2
         // 
         this.toolStripLabel2.Name = "toolStripLabel2";
         this.toolStripLabel2.Size = new System.Drawing.Size(54, 22);
         this.toolStripLabel2.Text = "channels";
         // 
         // channels
         // 
         this.channels.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
         this.channels.Name = "channels";
         this.channels.Size = new System.Drawing.Size(75, 25);
         this.channels.SelectedIndexChanged += new System.EventHandler(this.channels_SelectedIndexChanged);

         channels.SelectedItem = "1";
         trigger.Text = "0";
         oscillo.SetTriggerVoltage(byte.Parse(trigger.Text));
      }

      public ToolStrip GetToolStrip()
      {
         return toolStrip;
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
