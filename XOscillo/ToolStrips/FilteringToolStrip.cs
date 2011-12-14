using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   public class FilteringToolStrip : MyToolbar
   {
      FilterConsumer filterConsumer;
      Filter lowPass = new LowPass();
      
      private System.Windows.Forms.ToolStripLabel toolStripLabel4;
      private OnlyNumbersToolStripTextBox cof;
      private System.Windows.Forms.CheckBox enableFiltering;
      private System.Windows.Forms.ToolStripControlHost enable;

      public event EventHandler dataChanged = null;

      public FilteringToolStrip(FilterConsumer fc)
      {
         this.filterConsumer = fc;
         fc.SetFilter(lowPass);

         this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
         this.cof = new OnlyNumbersToolStripTextBox();
         this.enableFiltering = new CheckBox();
         this.enable = new System.Windows.Forms.ToolStripControlHost(this.enableFiltering);

         // 
         // toolStrip1
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enable,
            this.toolStripLabel4,
            this.cof});
         this.toolStrip.Location = new System.Drawing.Point(252, 0);
         this.toolStrip.Name = "toolStrip1";
         this.toolStrip.Size = new System.Drawing.Size(197, 25);
         this.toolStrip.TabIndex = 2;

         for (int i = 0; ; i++)
         {
            string name = lowPass.GetValueName(i);
            if (name == null)
            {
               break;
            }

            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel4.Text = name;

            
            //this.enable.DisplayStyle = ToolStripItemDisplayStyle.
            this.enableFiltering.CheckedChanged += new System.EventHandler(this.enableFilteringChanged);
            this.enableFiltering.Checked = false;

            // cof
            this.cof.Name = name;
            this.cof.Size = new System.Drawing.Size(50, 25);
            this.cof.Text = "100";
            this.cof.Tag = i;
            this.cof.Enabled = this.enableFiltering.Checked;
            this.cof.textReady += new EventHandler(cof_textReady);
         }
      }

      private void enableFilteringChanged(object sender, EventArgs e)
      {
         cof.Enabled = enableFiltering.Checked;
         filterConsumer.Enable(enableFiltering.Checked);
         if (dataChanged != null)
         {
            lowPass.SetValue((int)(cof.Tag), double.Parse(cof.Text));
            dataChanged(sender, e);
         }
      }

      private void cof_textReady(object sender, EventArgs e)
      {
         lowPass.SetValue((int)(cof.Tag), double.Parse(cof.Text));
         dataChanged(sender, e);
      }

   }
}
