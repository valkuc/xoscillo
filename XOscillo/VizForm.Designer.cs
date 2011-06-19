namespace XOscillo
{
   partial class VizForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
         this.graphControl = new XOscillo.GraphControl();
         this.toolStripContainer1.ContentPanel.SuspendLayout();
         this.toolStripContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer1
         // 
         // 
         // toolStripContainer1.ContentPanel
         // 
         this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
         this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(787, 418);
         this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer1.Name = "toolStripContainer1";
         this.toolStripContainer1.Size = new System.Drawing.Size(787, 443);
         this.toolStripContainer1.TabIndex = 0;
         this.toolStripContainer1.Text = "toolStripContainer1";
         // 
         // graphControl
         // 
         this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.graphControl.Location = new System.Drawing.Point(0, 0);
         this.graphControl.Name = "graphControl";
         this.graphControl.Size = new System.Drawing.Size(787, 418);
         this.graphControl.TabIndex = 0;
         // 
         // VizForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(787, 443);
         this.Controls.Add(this.toolStripContainer1);
         this.Name = "VizForm";
         this.Text = "VizForm";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VizForm_FormClosing);
         this.toolStripContainer1.ContentPanel.ResumeLayout(false);
         this.toolStripContainer1.ResumeLayout(false);
         this.toolStripContainer1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      protected System.Windows.Forms.ToolStripContainer toolStripContainer1;
      protected GraphControl graphControl;

   }
}