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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VizForm));
         this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
         this.graphControl = new XOscillo.GraphControl();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.time = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
         this.fftButton = new System.Windows.Forms.ToolStripButton();
         this.toolStripContainer.ContentPanel.SuspendLayout();
         this.toolStripContainer.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer
         // 
         // 
         // toolStripContainer.ContentPanel
         // 
         this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
         this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(787, 418);
         this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer.Name = "toolStripContainer";
         this.toolStripContainer.Size = new System.Drawing.Size(787, 443);
         this.toolStripContainer.TabIndex = 0;
         this.toolStripContainer.Text = "toolStripContainer1";
         // 
         // toolStripContainer.TopToolStripPanel
         // 
         this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
         // 
         // graphControl
         // 
         this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.graphControl.Location = new System.Drawing.Point(0, 0);
         this.graphControl.Name = "graphControl";
         this.graphControl.Size = new System.Drawing.Size(787, 418);
         this.graphControl.TabIndex = 0;
         // 
         // toolStrip1
         // 
         this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.time,
            this.toolStripLabel1,
            this.fftButton});
         this.toolStrip1.Location = new System.Drawing.Point(3, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(219, 25);
         this.toolStrip1.TabIndex = 0;
         // 
         // time
         // 
         this.time.Name = "time";
         this.time.Size = new System.Drawing.Size(121, 25);
         this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);
         // 
         // toolStripLabel1
         // 
         this.toolStripLabel1.Name = "toolStripLabel1";
         this.toolStripLabel1.Size = new System.Drawing.Size(23, 22);
         this.toolStripLabel1.Text = "ms";
         // 
         // fftButton
         // 
         this.fftButton.CheckOnClick = true;
         this.fftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.fftButton.Image = ((System.Drawing.Image)(resources.GetObject("fftButton.Image")));
         this.fftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fftButton.Name = "fftButton";
         this.fftButton.Size = new System.Drawing.Size(30, 22);
         this.fftButton.Text = "FFT";
         this.fftButton.CheckedChanged += new System.EventHandler(this.fftButton_CheckedChanged);
         // 
         // VizForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(787, 443);
         this.Controls.Add(this.toolStripContainer);
         this.Name = "VizForm";
         this.Text = "VizForm";
         this.toolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer.TopToolStripPanel.PerformLayout();
         this.toolStripContainer.ResumeLayout(false);
         this.toolStripContainer.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      public System.Windows.Forms.ToolStripContainer toolStripContainer;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripComboBox time;
      private System.Windows.Forms.ToolStripLabel toolStripLabel1;
      protected GraphControl graphControl;
      private System.Windows.Forms.ToolStripButton fftButton;


   }
}