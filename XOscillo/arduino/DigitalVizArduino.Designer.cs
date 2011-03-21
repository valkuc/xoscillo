namespace XOscillo
{
   partial class DigitalVizArduino
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DigitalVizArduino));
         this.toolStrip2 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
         this.time = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.clone = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.play = new System.Windows.Forms.ToolStripButton();
         this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
         this.graphControl = new XOscillo.GraphControl();
         this.toolStrip2.SuspendLayout();
         this.toolStripContainer1.ContentPanel.SuspendLayout();
         this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStrip2
         // 
         this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.time,
            this.toolStripSeparator4,
            this.clone,
            this.toolStripSeparator3,
            this.play});
         this.toolStrip2.Location = new System.Drawing.Point(3, 0);
         this.toolStrip2.Name = "toolStrip2";
         this.toolStrip2.Size = new System.Drawing.Size(230, 25);
         this.toolStrip2.TabIndex = 1;
         this.toolStrip2.Text = "toolStrip2";
         // 
         // toolStripLabel3
         // 
         this.toolStripLabel3.Name = "toolStripLabel3";
         this.toolStripLabel3.Size = new System.Drawing.Size(31, 22);
         this.toolStripLabel3.Text = "time";
         // 
         // time
         // 
         this.time.Name = "time";
         this.time.Size = new System.Drawing.Size(75, 25);
         this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);
         // 
         // toolStripSeparator4
         // 
         this.toolStripSeparator4.Name = "toolStripSeparator4";
         this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
         // 
         // play
         // 
         this.play.CheckOnClick = true;
         this.play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.play.Image = global::XOscillo.Properties.Resources.play;
         this.play.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.play.Margin = new System.Windows.Forms.Padding(1);
         this.play.Name = "play";
         this.play.Size = new System.Drawing.Size(23, 23);
         this.play.Text = "toolStripButton2";
         this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
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
         // toolStripContainer1.TopToolStripPanel
         // 
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
         // 
         // graphControl
         // 
         this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.graphControl.Location = new System.Drawing.Point(0, 0);
         this.graphControl.Name = "graphControl";
         this.graphControl.Size = new System.Drawing.Size(787, 418);
         this.graphControl.TabIndex = 0;
         // 
         // DigitalVizArduino
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.ClientSize = new System.Drawing.Size(787, 443);
         this.Controls.Add(this.toolStripContainer1);
         this.Name = "DigitalVizArduino";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.toolStrip2.ResumeLayout(false);
         this.toolStrip2.PerformLayout();
         this.toolStripContainer1.ContentPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.PerformLayout();
         this.toolStripContainer1.ResumeLayout(false);
         this.toolStripContainer1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStrip toolStrip2;
      private System.Windows.Forms.ToolStripButton clone;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripButton play;
      private System.Windows.Forms.ToolStripLabel toolStripLabel3;
      private System.Windows.Forms.ToolStripComboBox time;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      private System.Windows.Forms.ToolStripContainer toolStripContainer1;
      private GraphControl graphControl;
   }
}
