namespace XOscillo
{
   partial class VizArduino
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VizArduino));
         this.toolStrip2 = new System.Windows.Forms.ToolStrip();
         this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
         this.trigger = new System.Windows.Forms.ToolStripTextBox();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
         this.channels = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.clone = new System.Windows.Forms.ToolStripButton();
         this.play = new System.Windows.Forms.ToolStripButton();
         this.toolStripContainer.ContentPanel.SuspendLayout();
         this.toolStripContainer.SuspendLayout();
         this.toolStrip2.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer
         // 
         // 
         // toolStripContainer.ContentPanel
         // 
         this.toolStripContainer.ContentPanel.Controls.Add(this.toolStrip2);
         this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(787, 443);
         // 
         // graphControl
         // 
         this.graphControl.Location = new System.Drawing.Point(0, 25);
         this.graphControl.Size = new System.Drawing.Size(787, 418);
         this.graphControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.graphControl_KeyDown);
         // 
         // toolStrip2
         // 
         this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.trigger,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.channels,
            this.toolStripSeparator2,
            this.clone,
            this.toolStripSeparator3,
            this.play});
         this.toolStrip2.Location = new System.Drawing.Point(0, 0);
         this.toolStrip2.Name = "toolStrip2";
         this.toolStrip2.Size = new System.Drawing.Size(787, 25);
         this.toolStrip2.TabIndex = 1;
         this.toolStrip2.Text = "toolStrip2";
         // 
         // toolStripLabel1
         // 
         this.toolStripLabel1.Name = "toolStripLabel1";
         this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
         this.toolStripLabel1.Text = "trigger";
         // 
         // trigger
         // 
         this.trigger.Name = "trigger";
         this.trigger.Size = new System.Drawing.Size(100, 25);
         this.trigger.Text = "127";
         this.trigger.Validating += new System.ComponentModel.CancelEventHandler(this.trigger_Validating);
         this.trigger.Validated += new System.EventHandler(this.trigger_Validated);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripLabel2
         // 
         this.toolStripLabel2.Name = "toolStripLabel2";
         this.toolStripLabel2.Size = new System.Drawing.Size(54, 22);
         this.toolStripLabel2.Text = "channels";
         // 
         // channels
         // 
         this.channels.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
         this.channels.Name = "channels";
         this.channels.Size = new System.Drawing.Size(121, 25);
         this.channels.SelectedIndexChanged += new System.EventHandler(this.channels_SelectedIndexChanged);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
         // VizArduino
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.ClientSize = new System.Drawing.Size(787, 443);
         this.Name = "VizArduino";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.toolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolStripContainer.ContentPanel.PerformLayout();
         this.toolStripContainer.ResumeLayout(false);
         this.toolStripContainer.PerformLayout();
         this.toolStrip2.ResumeLayout(false);
         this.toolStrip2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStrip toolStrip2;
      private System.Windows.Forms.ToolStripLabel toolStripLabel1;
      private System.Windows.Forms.ToolStripTextBox trigger;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel2;
      private System.Windows.Forms.ToolStripComboBox channels;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripButton clone;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripButton play;
   }
}
