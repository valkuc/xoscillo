using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
    class ParallaxToolStrip : MyToolbar
    {
        SerialParallax oscillo;
        GraphControl graphControl;

        private System.Windows.Forms.ToolStripLabel triggerLabel;
        private System.Windows.Forms.ToolStripTextBox trigger;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox triggerMode;

        public ParallaxToolStrip(SerialParallax osc, GraphControl gc)
        {
            oscillo = osc;
            graphControl = gc;

            // 
            // toolStrip2
            // 
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Location = new System.Drawing.Point(3, 0);
            this.toolStrip.Name = "toolStrip2";
            this.toolStrip.Size = new System.Drawing.Size(243, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip2";

            this.triggerLabel = new System.Windows.Forms.ToolStripLabel();
            this.trigger = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.triggerMode = new System.Windows.Forms.ToolStripComboBox();

            // 
            // triggerLabel
            // 
            this.triggerLabel.Name = "triggerLabel";
            this.triggerLabel.Size = new System.Drawing.Size(42, 22);
            this.triggerLabel.Text = "Trigger";
            this.toolStrip.Items.Add(this.triggerLabel);

            // 
            // trigger
            // 
            this.trigger.Name = "trigger";
            this.trigger.Size = new System.Drawing.Size(50, 25);
            this.trigger.Text = "0";
            this.trigger.Validating += new System.ComponentModel.CancelEventHandler(this.trigger_Validating);
            this.trigger.Validated += new System.EventHandler(this.trigger_Validated);
            this.toolStrip.Items.Add(this.trigger);

            // 
            // triggerMode
            // 
            this.triggerMode.Items.AddRange(new object[] {
            "ch1 ˄",
            "ch1 ˅",
            "ch2 ˄",
            "ch2 ˅",
            "ext ˄"});
            this.triggerMode.Name = "triggerMode";
            this.triggerMode.Size = new System.Drawing.Size(20, 25);
            this.triggerMode.SelectedIndexChanged += new System.EventHandler(this.triggerMode_SelectedIndexChanged);
            triggerMode.SelectedIndex = 0;
            this.toolStrip.Items.Add(this.triggerMode);

            //channels.SelectedIndex = 0;
            trigger.Text = "0";
            triggerMode.SelectedIndex = 0;
            oscillo.TriggerVoltage = (float.Parse(trigger.Text));
        }

        private void triggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (triggerMode.SelectedIndex)
            {
                case 0:
                    oscillo.TriggerChannel = SerialParallax.TriggerChannelEnum.TC_CH1;
                    oscillo.Edge = SerialParallax.EdgeEnum.EDGE_RAISING;
                    oscillo.ExternalTrigger = false;
                    break;
                case 1:
                    oscillo.TriggerChannel = SerialParallax.TriggerChannelEnum.TC_CH1;
                    oscillo.Edge = SerialParallax.EdgeEnum.EDGE_FALLING;
                    oscillo.ExternalTrigger = false;
                    break;
                case 2:
                    oscillo.TriggerChannel = SerialParallax.TriggerChannelEnum.TC_CH2;
                    oscillo.Edge = SerialParallax.EdgeEnum.EDGE_RAISING;
                    oscillo.ExternalTrigger = false;
                    break;
                case 3:
                    oscillo.TriggerChannel = SerialParallax.TriggerChannelEnum.TC_CH2;
                    oscillo.Edge = SerialParallax.EdgeEnum.EDGE_FALLING;
                    oscillo.ExternalTrigger = false;
                    break;
                case 4:
                    oscillo.ExternalTrigger = true;
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
