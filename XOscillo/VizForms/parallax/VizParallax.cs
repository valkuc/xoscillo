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
   public partial class VizParallax : AnalogVizForm
   {
      SerialParallax oscillo;

      float[] divs = { 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 0.0005f, 0.0002f, 0.00001f, 0.000005f };
      int[] samplerates = { 50, 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000,500000,1000000 };

      override public void Form_Load(object sender, EventArgs e)
      {
         Autodetection<SerialParallax> au = new Autodetection<SerialParallax>();
         oscillo = au.Detection();

         m_Acq = new Acquirer();
         m_Acq.Open(oscillo, GetFirstConsumerInChain());
         gf.drawSlidingFFT = true;

         commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl);         

         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         commonToolStrip.selectedIndexChanged += this.time_SelectedIndexChanged;
         commonToolStrip.time.SelectedIndex = 10;

         SetToolbar(GetFilteringToolStrip());
         SetToolbar( new ParallaxToolStrip(oscillo, graphControl));
         SetToolbar(GetFftToolStrip());
         SetToolbar(commonToolStrip);

         ga.SetVerticalRange(0, 255, 32, "Volts");         
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {
         oscillo.SetSamplingRate(samplerates[commonToolStrip.time.SelectedIndex] );
      }
   }
}
