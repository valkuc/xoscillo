namespace XOscillo
{
   partial class VizBuffer
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VizBuffer));
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
         this.time = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.clone = new System.Windows.Forms.ToolStripButton();
         this.fft = new System.Windows.Forms.ToolStripButton();
         this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
         this.graphControl = new XOscillo.GraphControl();
         this.toolStrip1.SuspendLayout();
         this.toolStripContainer1.ContentPanel.SuspendLayout();
         this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStrip1
         // 
         this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.time,
            this.toolStripSeparator1,
            this.clone,
            this.fft});
         this.toolStrip1.Location = new System.Drawing.Point(3, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(229, 25);
         this.toolStrip1.TabIndex = 0;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // toolStripLabel1
         // 
         this.toolStripLabel1.Name = "toolStripLabel1";
         this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
         this.toolStripLabel1.Text = "time";
         // 
         // time
         // 
         this.time.Name = "time";
         this.time.Size = new System.Drawing.Size(75, 25);
         this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // clone
         // 
         this.clone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.clone.Image = ((System.Drawing.Image)(resources.GetObject("clone.Image")));
         this.clone.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.clone.Name = "clone";
         this.clone.Size = new System.Drawing.Size(42, 22);
         this.clone.Text = "Clone";
         this.clone.Click += new System.EventHandler(this.clone_Click);
         // 
         // fft
         // 
         this.fft.CheckOnClick = true;
         this.fft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.fft.Image = ((System.Drawing.Image)(resources.GetObject("fft.Image")));
         this.fft.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fft.Name = "fft";
         this.fft.Size = new System.Drawing.Size(30, 22);
         this.fft.Text = "FFT";
         this.fft.Click += new System.EventHandler(this.fft_Click);
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
         this.toolStripContainer1.TabIndex = 1;
         this.toolStripContainer1.Text = "toolStripContainer1";
         // 
         // toolStripContainer1.TopToolStripPanel
         // 
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
         // 
         // graphControl
         // 
         this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.graphControl.Location = new System.Drawing.Point(0, 0);
         this.graphControl.Name = "graphControl";
         this.graphControl.Size = new System.Drawing.Size(787, 418);
         this.graphControl.TabIndex = 0;
         // 
         // VizBuffer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(787, 443);
         this.Controls.Add(this.toolStripContainer1);
         this.Name = "VizBuffer";
         this.Text = "VizBuffer";
         this.Load += new System.EventHandler(this.VizBuffer_Load);
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.toolStripContainer1.ContentPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.PerformLayout();
         this.toolStripContainer1.ResumeLayout(false);
         this.toolStripContainer1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel1;
      private System.Windows.Forms.ToolStripComboBox time;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripButton clone;
      private System.Windows.Forms.ToolStripButton fft;
      private System.Windows.Forms.ToolStripContainer toolStripContainer1;
      private GraphControl graphControl;
   }
}