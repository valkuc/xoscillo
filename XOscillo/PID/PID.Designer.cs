namespace XOscillo
{
   partial class PID
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
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
         this.button1 = new System.Windows.Forms.Button();
         this.Kp = new System.Windows.Forms.TrackBar();
         this.Ki = new System.Windows.Forms.TrackBar();
         this.Kd = new System.Windows.Forms.TrackBar();
         this.Setpoint = new System.Windows.Forms.TrackBar();
         this.graphControl1 = new XOscillo.GraphControl();
         ((System.ComponentModel.ISupportInitialize)(this.Kp)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Ki)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Kd)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Setpoint)).BeginInit();
         this.SuspendLayout();
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(12, 499);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(54, 46);
         this.button1.TabIndex = 1;
         this.button1.Text = "button1";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // Kp
         // 
         this.Kp.Location = new System.Drawing.Point(12, 302);
         this.Kp.Maximum = 1000;
         this.Kp.Name = "Kp";
         this.Kp.Size = new System.Drawing.Size(557, 45);
         this.Kp.TabIndex = 2;
         this.Kp.Value = 1000;
         // 
         // Ki
         // 
         this.Ki.Location = new System.Drawing.Point(12, 341);
         this.Ki.Maximum = 1000;
         this.Ki.Name = "Ki";
         this.Ki.Size = new System.Drawing.Size(557, 45);
         this.Ki.TabIndex = 3;
         this.Ki.Value = 55;
         this.Ki.Scroll += new System.EventHandler(this.trackBar2_Scroll);
         // 
         // Kd
         // 
         this.Kd.Location = new System.Drawing.Point(12, 392);
         this.Kd.Maximum = 100;
         this.Kd.Name = "Kd";
         this.Kd.Size = new System.Drawing.Size(557, 45);
         this.Kd.TabIndex = 4;
         this.Kd.Value = 13;
         // 
         // Setpoint
         // 
         this.Setpoint.Location = new System.Drawing.Point(12, 448);
         this.Setpoint.Maximum = 10000;
         this.Setpoint.Name = "Setpoint";
         this.Setpoint.Size = new System.Drawing.Size(557, 45);
         this.Setpoint.TabIndex = 5;
         this.Setpoint.Value = 1000;
         // 
         // graphControl1
         // 
         this.graphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.graphControl1.Location = new System.Drawing.Point(12, 12);
         this.graphControl1.Name = "graphControl1";
         this.graphControl1.Size = new System.Drawing.Size(557, 284);
         this.graphControl1.TabIndex = 0;
         // 
         // PID
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(581, 601);
         this.Controls.Add(this.Setpoint);
         this.Controls.Add(this.Kd);
         this.Controls.Add(this.Ki);
         this.Controls.Add(this.Kp);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.graphControl1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "PID";
         this.Padding = new System.Windows.Forms.Padding(9);
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "PID";
         ((System.ComponentModel.ISupportInitialize)(this.Kp)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Ki)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Kd)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Setpoint)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private GraphControl graphControl1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.TrackBar Kp;
      private System.Windows.Forms.TrackBar Ki;
      private System.Windows.Forms.TrackBar Kd;
      private System.Windows.Forms.TrackBar Setpoint;

   }
}
