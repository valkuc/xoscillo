using System;
using System.Windows.Forms;
using XOscillo.Graph;

namespace XOscillo.VizForms.Teensy
{
   class AnalogTeensyToolbar : MyToolbar
   {
      AnalogTeensy oscillo;
      GraphControl graphControl;
      
      private System.Windows.Forms.ToolStripLabel sampleCountLabel;
      private OnlyNumbersToolStripTextBox sampleCount;

      public AnalogTeensyToolbar(AnalogTeensy osc, GraphControl gc)
      {
         oscillo = osc;
         graphControl = gc;

         this.toolStrip = new System.Windows.Forms.ToolStrip();

         this.sampleCountLabel = new System.Windows.Forms.ToolStripLabel();
         this.sampleCount = new OnlyNumbersToolStripTextBox();

         // 
         // toolStrip2
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleCountLabel,
            this.sampleCount
         });
         this.toolStrip.Location = new System.Drawing.Point(3, 0);
         this.toolStrip.Name = "toolStrip2";
         this.toolStrip.Size = new System.Drawing.Size(243, 25);
         this.toolStrip.TabIndex = 1;
         this.toolStrip.Text = "toolStrip2";

         // 
         // sampleCountLabel
         // 
         this.sampleCountLabel.Size = new System.Drawing.Size(54, 22);
         this.sampleCountLabel.Text = "time(ms)";

         // 
         // sampleCountLabel
         // 
         this.sampleCount.Size = new System.Drawing.Size(54, 22);
         this.sampleCount.Text = "100";
         this.sampleCount.textReady += new EventHandler(sampleCount_textReady);

         //set values
         oscillo.SetMeasuringTime(int.Parse(sampleCount.Text));
      }

      private void sampleCount_textReady(object sender, EventArgs e)
      {
         ToolStripTextBox textbox = sender as ToolStripTextBox;

         if (textbox != null)
         {
             oscillo.SetMeasuringTime(int.Parse(textbox.Text));
         }
      }

   }
}
