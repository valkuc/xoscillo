namespace XOscillo.Tools
{
   partial class DecodeFSK
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
         this.output = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // output
         // 
         this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.output.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.output.Location = new System.Drawing.Point(12, 12);
         this.output.Multiline = true;
         this.output.Name = "output";
         this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.output.Size = new System.Drawing.Size(754, 485);
         this.output.TabIndex = 2;
         // 
         // DecodeFSK
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(778, 560);
         this.Controls.Add(this.output);
         this.Name = "DecodeFSK";
         this.Text = "DecodeFSK";
         this.Load += new System.EventHandler(this.DecodeFSK_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox output;
   }
}