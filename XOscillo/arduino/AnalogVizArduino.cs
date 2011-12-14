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
      AnalogArduino oscillo = new AnalogArduino();

      override public bool Init()
      {
         base.Init();
         m_Acq = new Acquirer();
         m_Acq.Open(oscillo, fc);
         gf.drawSlidingFFT = true;
         return true;
      }

      override public void Form1_Load(object sender, EventArgs e)
      {        
         commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl);

         float [] divs = {1.0f, 0.5f,0.2f,0.1f,0.05f,0.02f,0.01f,0.005f,0.002f,0.001f,0.0005f,0.0002f};

         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         
         commonToolStrip.time.SelectedIndex = 10;

         AnalogArduinoToolbar aat = new AnalogArduinoToolbar(oscillo, graphControl);
         this.toolStripContainer.TopToolStripPanel.Controls.Add(ft.GetToolStrip());
         this.toolStripContainer.TopToolStripPanel.Controls.Add(aat.GetToolStrip());
         this.toolStripContainer.TopToolStripPanel.Controls.Add(fft.GetToolStrip());
         this.toolStripContainer.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());

         ga.SetVerticalRange(255, 0, 32, "Volts");         
      }

      override public void UpdateGraph(object sender, EventArgs e)
      {
         //fc.SetDataBlock(m_Acq.);
         Invalidate();
      }

   }
}
