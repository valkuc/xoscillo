using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   class ParallaxToolStrip
   {
      SerialParallax oscillo;
      GraphControl graphControl;
      
      private System.Windows.Forms.ToolStrip toolStrip;
      private System.Windows.Forms.ToolStripLabel triggerLabel;
      private System.Windows.Forms.ToolStripTextBox trigger;
      private System.Windows.Forms.ToolStripLabel toolStripLabel2;
      private System.Windows.Forms.ToolStripComboBox channels;
      private System.Windows.Forms.ToolStripComboBox triggerMode;

      public ParallaxToolStrip(SerialParallax osc, GraphControl gc)
      {
         oscillo = osc;
         graphControl = gc;

         this.toolStrip = new System.Windows.Forms.ToolStrip();
         this.triggerLabel = new System.Windows.Forms.ToolStripLabel();
         this.trigger = new System.Windows.Forms.ToolStripTextBox();
         this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
         this.channels = new System.Windows.Forms.ToolStripComboBox();
         this.triggerMode = new System.Windows.Forms.ToolStripComboBox();

         // 
         // toolStrip2
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triggerLabel,
            this.trigger,
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
         // toolStripLabel2
         // 
         this.toolStripLabel2.Name = "toolStripLabel2";
         this.toolStripLabel2.Size = new System.Drawing.Size(54, 22);
         this.toolStripLabel2.Text = "channels";

         // 
         // triggerMode
         // 
         this.triggerMode.Items.AddRange(new object[] {
            "ch1 rising",
            "ch1 falling",
            "ch2 rising",
            "ch2 falling",
            "external rising"});
         this.triggerMode.Name = "triggerMode";
         this.triggerMode.Size = new System.Drawing.Size(80, 25);
         this.triggerMode.SelectedIndexChanged += new System.EventHandler(this.triggerMode_SelectedIndexChanged);
         channels.SelectedItem = "1";
         trigger.Text = "0";
         triggerMode.SelectedIndex = 1;
         oscillo.TriggerVoltage = (byte.Parse(trigger.Text));
      }

      public ToolStrip GetToolStrip()
      {
         return toolStrip;
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
         
         graphControl.Invalidate();
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

   }
}
