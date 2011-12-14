namespace XOscillo
{
   partial class ManualSerialPortSelection
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
         this.label1 = new System.Windows.Forms.Label();
         this.comboBox1 = new System.Windows.Forms.ComboBox();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(31, 19);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(238, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Couln\'t autodetect port, please select manually....";
         this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         // 
         // comboBox1
         // 
         this.comboBox1.FormattingEnabled = true;
         this.comboBox1.Location = new System.Drawing.Point(34, 49);
         this.comboBox1.Name = "comboBox1";
         this.comboBox1.Size = new System.Drawing.Size(214, 21);
         this.comboBox1.TabIndex = 1;
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(100, 92);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 2;
         this.button1.Text = "Go";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // ManualSerialPortSelection
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(285, 127);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.comboBox1);
         this.Controls.Add(this.label1);
         this.Name = "ManualSerialPortSelection";
         this.Text = "ManualSerialPortSelection";
         this.Load += new System.EventHandler(this.ManualSerialPortSelection_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox comboBox1;
      private System.Windows.Forms.Button button1;
   }
}