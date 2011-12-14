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
         this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
         this.graphControl = new XOscillo.GraphControl();
         this.toolStripContainer.ContentPanel.SuspendLayout();
         this.toolStripContainer.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer1
         // 
         // 
         // toolStripContainer1.ContentPanel
         // 
         this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
         this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(787, 418);
         this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer.Name = "toolStripContainer1";
         this.toolStripContainer.Size = new System.Drawing.Size(787, 443);
         this.toolStripContainer.TabIndex = 0;
         this.toolStripContainer.Text = "toolStripContainer1";
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
         this.Controls.Add(this.toolStripContainer);
         this.Name = "VizForm";
         this.Text = "VizForm";
         this.Load += new System.EventHandler(this.Form_Load);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VizForm_FormClosing);
         this.toolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolStripContainer.ResumeLayout(false);
         this.toolStripContainer.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStripContainer toolStripContainer;
      protected GraphControl graphControl;

   }
}