namespace XOscillo
{
   partial class VizParallax
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VizParallax));
         this.toolStrip2 = new System.Windows.Forms.ToolStrip();
         this.label3 = new System.Windows.Forms.ToolStripLabel();
         this.time = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
         this.trigger = new System.Windows.Forms.ToolStripTextBox();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
         this.triggerMode = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.play = new System.Windows.Forms.ToolStripButton();
         this.clone = new System.Windows.Forms.ToolStripButton();
         this.fft = new System.Windows.Forms.ToolStripButton();
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
            this.label3,
            this.time,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.trigger,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.triggerMode,
            this.toolStripSeparator3,
            this.play,
            this.clone,
            this.fft});
         this.toolStrip2.Location = new System.Drawing.Point(3, 0);
         this.toolStrip2.Name = "toolStrip2";
         this.toolStrip2.Size = new System.Drawing.Size(525, 25);
         this.toolStrip2.TabIndex = 1;
         this.toolStrip2.Text = "toolStrip2";
         // 
         // label3
         // 
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(31, 22);
         this.label3.Text = "time";
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
         // toolStripLabel2
         // 
         this.toolStripLabel2.Name = "toolStripLabel2";
         this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
         this.toolStripLabel2.Text = "trigger";
         // 
         // trigger
         // 
         this.trigger.Name = "trigger";
         this.trigger.Size = new System.Drawing.Size(50, 25);
         this.trigger.Validating += new System.ComponentModel.CancelEventHandler(this.trigger_Validating);
         this.trigger.Validated += new System.EventHandler(this.trigger_Validated);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripLabel1
         // 
         this.toolStripLabel1.Name = "toolStripLabel1";
         this.toolStripLabel1.Size = new System.Drawing.Size(90, 22);
         this.toolStripLabel1.Text = "trigger  channel";
         // 
         // channels
         // 
         this.triggerMode.Items.AddRange(new object[] {
            "ch1 rising",
            "ch1 falling",
            "ch2 rising",
            "ch2 falling",
            "external rising"});
         this.triggerMode.Name = "channels";
         this.triggerMode.Size = new System.Drawing.Size(75, 25);
         this.triggerMode.SelectedIndexChanged += new System.EventHandler(this.triggerMode_SelectedIndexChanged);
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
         this.play.Image = global::XOscillo.Properties.Resources.pause;
         this.play.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.play.Name = "play";
         this.play.Size = new System.Drawing.Size(23, 22);
         this.play.Text = "play";
         this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
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
         this.fft.CheckedChanged += new System.EventHandler(this.fft_CheckedChanged);
         // 
         // toolStripContainer1
         // 
         // 
         // toolStripContainer1.ContentPanel
         // 
         this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
         this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(761, 392);
         this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer1.Name = "toolStripContainer1";
         this.toolStripContainer1.Size = new System.Drawing.Size(761, 417);
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
         this.graphControl.Size = new System.Drawing.Size(761, 392);
         this.graphControl.TabIndex = 0;
         // 
         // VizParallax
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(761, 417);
         this.Controls.Add(this.toolStripContainer1);
         this.Name = "VizParallax";
         this.Text = "Form1";
         this.Load += new System.EventHandler(this.VizParallax_Load);
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
      private System.Windows.Forms.ToolStripButton play;
      private System.Windows.Forms.ToolStripLabel toolStripLabel1;
      private System.Windows.Forms.ToolStripComboBox triggerMode;
      private System.Windows.Forms.ToolStripLabel toolStripLabel2;
      private System.Windows.Forms.ToolStripTextBox trigger;
      private System.Windows.Forms.ToolStripContainer toolStripContainer1;
      private GraphControl graphControl;
      private System.Windows.Forms.ToolStripLabel label3;
      private System.Windows.Forms.ToolStripComboBox time;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripButton clone;
      private System.Windows.Forms.ToolStripButton fft;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

   }
}