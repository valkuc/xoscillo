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
   public partial class AnalogVizArduino : AnalogVizForm
   {
      override public void Form_Load(object sender, EventArgs e)
      {
         Autodetection<AnalogArduino> au = new Autodetection<AnalogArduino>();            
         AnalogArduino oscillo = au.Detection();

         m_Acq = new Acquirer();
         m_Acq.Open(oscillo, GetFirstConsumerInChain());
         gf.drawSlidingFFT = true;
         
         commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl);

         float [] divs = {1.0f, 0.5f,0.2f,0.1f,0.05f,0.02f,0.01f,0.005f,0.002f,0.001f,0.0005f,0.0002f};

         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         
         commonToolStrip.time.SelectedIndex = 10;

         AnalogArduinoToolbar aat = new AnalogArduinoToolbar(oscillo, graphControl);
         SetToolbar(GetFilteringToolStrip());
         SetToolbar(aat);
         SetToolbar(GetFftToolStrip());
         SetToolbar(commonToolStrip);

         ga.SetVerticalRange(255, 0, 32, "Volts");         
      }

      override public void UpdateGraph(object sender, EventArgs e)
      {
         //fc.SetDataBlock(m_Acq.);
         Invalidate();
      }

   }
}
