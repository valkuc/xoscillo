using System;
using XOscillo.Graph;

namespace XOscillo.ToolStrips
{
   public class FftToolStrip : MyToolbar
   {
      private System.Windows.Forms.ToolStripButton fft;

      GraphControl graphControl;

      XOscillo.Graph.Graph oldGraph;

      public FftToolStrip(GraphControl gc, GraphFFT gf)
      {
         graphControl = gc;
         oldGraph = gf;
         
         this.fft = new System.Windows.Forms.ToolStripButton();

         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fft});
         this.toolStrip.Location = new System.Drawing.Point(0, 0);
         this.toolStrip.Name = "toolStrip2";
         this.toolStrip.Size = new System.Drawing.Size(497, 25);
         this.toolStrip.TabIndex = 1;
         this.toolStrip.Text = "toolStrip2";

         // 
         // fft
         // 
         this.fft.CheckOnClick = true;
         this.fft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.fft.Image = null;// ((System.Drawing.Image)(resources.GetObject("fft.Image")));
         this.fft.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fft.Name = "fft";
         this.fft.Size = new System.Drawing.Size(30, 22);
         this.fft.Text = "FFT";
         this.fft.CheckStateChanged += new System.EventHandler(this.fft_CheckStateChanged);
      }

      private void fft_CheckStateChanged(object sender, EventArgs e)
      {
         oldGraph = graphControl.SetRenderer(oldGraph);
         graphControl.Invalidate();
      }


   }
}
