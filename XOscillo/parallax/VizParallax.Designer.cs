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
         this.comboTime = new System.Windows.Forms.ComboBox();
         this.comboEdge = new System.Windows.Forms.ComboBox();
         this.comboChannelTrigger = new System.Windows.Forms.ComboBox();
         this.play = new System.Windows.Forms.CheckBox();
         this.save = new System.Windows.Forms.Button();
         this.display = new XOscillo.GraphControl();
         this.panel2 = new System.Windows.Forms.Panel();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // comboTime
         // 
         this.comboTime.FormattingEnabled = true;
         this.comboTime.Location = new System.Drawing.Point(3, 3);
         this.comboTime.Name = "comboTime";
         this.comboTime.Size = new System.Drawing.Size(121, 21);
         this.comboTime.TabIndex = 1;
         this.comboTime.SelectedIndexChanged += new System.EventHandler(this.comboTime_SelectedIndexChanged);
         // 
         // comboEdge
         // 
         this.comboEdge.FormattingEnabled = true;
         this.comboEdge.Location = new System.Drawing.Point(130, 3);
         this.comboEdge.Name = "comboEdge";
         this.comboEdge.Size = new System.Drawing.Size(121, 21);
         this.comboEdge.TabIndex = 2;
         this.comboEdge.SelectedIndexChanged += new System.EventHandler(this.comboEdge_SelectedIndexChanged);
         // 
         // comboChannelTrigger
         // 
         this.comboChannelTrigger.FormattingEnabled = true;
         this.comboChannelTrigger.Location = new System.Drawing.Point(257, 3);
         this.comboChannelTrigger.Name = "comboChannelTrigger";
         this.comboChannelTrigger.Size = new System.Drawing.Size(121, 21);
         this.comboChannelTrigger.TabIndex = 3;
         this.comboChannelTrigger.SelectedIndexChanged += new System.EventHandler(this.comboChannelTrigger_SelectedIndexChanged);
         // 
         // play
         // 
         this.play.Appearance = System.Windows.Forms.Appearance.Button;
         this.play.AutoSize = true;
         this.play.Location = new System.Drawing.Point(386, 3);
         this.play.Name = "play";
         this.play.Size = new System.Drawing.Size(36, 23);
         this.play.TabIndex = 4;
         this.play.Text = "play";
         this.play.UseVisualStyleBackColor = true;
         this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
         // 
         // save
         // 
         this.save.Location = new System.Drawing.Point(428, 4);
         this.save.Name = "save";
         this.save.Size = new System.Drawing.Size(75, 23);
         this.save.TabIndex = 6;
         this.save.Text = "save";
         this.save.UseVisualStyleBackColor = true;
         this.save.Click += new System.EventHandler(this.save_Click);
         // 
         // display
         // 
         this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.display.BackColor = System.Drawing.SystemColors.ControlText;
         this.display.Location = new System.Drawing.Point(0, 33);
         this.display.Name = "display";
         this.display.Size = new System.Drawing.Size(761, 384);
         this.display.TabIndex = 0;
         this.display.Load += new System.EventHandler(this.display_Load);
         // 
         // panel2
         // 
         this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.panel2.Controls.Add(this.comboTime);
         this.panel2.Controls.Add(this.comboEdge);
         this.panel2.Controls.Add(this.save);
         this.panel2.Controls.Add(this.comboChannelTrigger);
         this.panel2.Controls.Add(this.play);
         this.panel2.Location = new System.Drawing.Point(0, 3);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(761, 27);
         this.panel2.TabIndex = 0;
         // 
         // VizParallax
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(761, 417);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.display);
         this.Name = "VizParallax";
         this.Text = "Form1";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private GraphControl display;
      private System.Windows.Forms.ComboBox comboTime;
      private System.Windows.Forms.ComboBox comboEdge;
      private System.Windows.Forms.ComboBox comboChannelTrigger;
      private System.Windows.Forms.CheckBox play;
      private System.Windows.Forms.Button save;
      private System.Windows.Forms.Panel panel2;
   }
}