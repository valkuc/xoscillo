using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   class FilteringToolbar
   {
      AnalogArduino oscillo;
      GraphControl graphControl;

      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel4;
      private System.Windows.Forms.ToolStripTextBox cof;

      public FilteringToolbar(AnalogArduino osc, GraphControl gc)
      {
         oscillo = osc;
         graphControl = gc;

         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
         this.cof = new System.Windows.Forms.ToolStripTextBox();

         // 
         // toolStrip1
         // 
         this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.cof});
         this.toolStrip1.Location = new System.Drawing.Point(252, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(197, 25);
         this.toolStrip1.TabIndex = 2;
         // 
         // toolStripLabel4
         // 
         this.toolStripLabel4.Name = "toolStripLabel4";
         this.toolStripLabel4.Size = new System.Drawing.Size(52, 22);
         this.toolStripLabel4.Text = "LowPass";
         // 
         // cof
         // 
         this.cof.Name = "cof";
         this.cof.Size = new System.Drawing.Size(100, 25);
         this.cof.Text = "0";
      }

      public ToolStrip GetToolStrip()
      {
         return this.toolStrip1;
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
            cof.BackColor = Color.White;
         }
         else
         {
            cof.BackColor = Color.Red;
            e.Cancel = true;
         }
      }
   }
}
