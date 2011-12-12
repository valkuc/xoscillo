using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace XOscillo
{
   class FilteringToolStrip
   {
      FilterConsumer fc;
      Filter filter;
      
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel4;
      private System.Windows.Forms.ToolStripTextBox cof;
      private System.Windows.Forms.CheckBox enableFiltering;
      private System.Windows.Forms.ToolStripControlHost enable;

      public FilteringToolStrip(FilterConsumer fc)
      {
         this.fc = fc;
         this.filter = fc.GetFilter();

         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
         this.cof = new System.Windows.Forms.ToolStripTextBox();
         this.enableFiltering = new CheckBox();
         this.enable = new System.Windows.Forms.ToolStripControlHost(this.enableFiltering);

         // 
         // toolStrip1
         // 
         this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enable,
            this.toolStripLabel4,
            this.cof});
         this.toolStrip1.Location = new System.Drawing.Point(252, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(197, 25);
         this.toolStrip1.TabIndex = 2;

         for (int i = 0; ; i++)
         {
            string name = filter.GetValueName(i);
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
            this.cof.Text = "0";
            this.cof.Tag = i;
            this.cof.Enabled = this.enableFiltering.Checked;
            this.cof.Validating += new System.ComponentModel.CancelEventHandler(this.cof_Validating);
            this.cof.Validated += new System.EventHandler(this.cof_Validated);
         }
      }

      public ToolStrip GetToolStrip()
      {
         return this.toolStrip1;
      }

      private void enableFilteringChanged(object sender, EventArgs e)
      {
         cof.Enabled = enableFiltering.Checked;
         fc.Enable(enableFiltering.Checked);
      }

      private void cof_Validated(object sender, EventArgs e)
      {
         ToolStripTextBox textbox = sender as ToolStripTextBox;
         if (textbox != null)
         {
            filter.SetValue((int)(textbox.Tag), double.Parse(cof.Text));
         }
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
