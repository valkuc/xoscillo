using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace XOscillo
{
   public partial class VizParallax : VizForm
   {
      SerialParallax oscillo;

      public VizParallax( )
      {
         m_Acq = new Acquirer();
      }

      override public bool Init()
      {
         oscillo = new SerialParallax();

         return m_Acq.Open(oscillo, graphControl);
      }

      override public void Form1_Load(object sender, EventArgs e)
      {
         commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl);

         float[] divs = { 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 0.0005f, 0.0002f, 0.00001f, 0.000005f };

         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         commonToolStrip.selectedIndexChanged += this.time_SelectedIndexChanged;

         commonToolStrip.time.SelectedIndex = 10;

         ParallaxToolStrip pts = new ParallaxToolStrip(oscillo, graphControl);

         //FilteringToolStrip ft = new FilteringToolStrip(oscillo, graphControl);

         //this.toolStripContainer1.TopToolStripPanel.Controls.Add(ft.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(pts.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());

      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {

         int sampleRate = 0;
         switch (commonToolStrip.time.SelectedIndex)
         {
            case 0: sampleRate = 250; break;
            case 1: sampleRate = 500; break;
            case 2: sampleRate = 1000; break;
            case 3: sampleRate = 2500; break;
            case 4: sampleRate = 5000; break;
            case 5: sampleRate = 10000; break;
            case 6: sampleRate = 25000; break;
            case 7: sampleRate = 50000; break; // 0.001
            case 8: sampleRate = 100000; break;
            case 9: sampleRate = 250000; break;
            case 10: sampleRate = 500000; break;
            case 11: sampleRate = 1000000; break;
            case 12: sampleRate = 2500000; break; // 0.0001f
            case 13: sampleRate = 5000000; break;
         }
         oscillo.SetSamplingRate(sampleRate);
      }

   }
}
