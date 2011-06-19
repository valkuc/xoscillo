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
   public partial class AnalogVizArduino : XOscillo.VizForm
   {
      AnalogArduino oscillo;

      public AnalogVizArduino()
      {
         m_Acq = new Acquirer();
      }

      override public bool Init()
      {
         oscillo = new AnalogArduino();

         return m_Acq.Open(oscillo, graphControl);
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
         
         FilteringToolStrip ft = new FilteringToolStrip(oscillo, graphControl);

         this.toolStripContainer1.TopToolStripPanel.Controls.Add(ft.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(aat.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());
      }

   }
}
