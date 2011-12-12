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
   public partial class DigitalVizArduino : XOscillo.VizForm
   {
      DigitalArduino oscillo;
      GraphDigital gd = new GraphDigital();

      public DigitalVizArduino()
      {
         m_Acq = new Acquirer();
      }

      override public bool Init()
      {
         oscillo = new DigitalArduino();

         return m_Acq.Open(oscillo, graphControl.GetConsumer());
      }

      override public void Form1_Load(object sender, EventArgs e)
      {
         commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl);

         commonToolStrip.time.Items.Add(1.0);
         commonToolStrip.time.Items.Add(0.5);
         commonToolStrip.time.Items.Add(0.2);
         commonToolStrip.time.Items.Add(0.1);
         commonToolStrip.time.Items.Add(0.05);
         commonToolStrip.time.Items.Add(0.02);
         commonToolStrip.time.Items.Add(0.01);
         commonToolStrip.time.Items.Add(0.005);
         commonToolStrip.time.Items.Add(0.002);
         commonToolStrip.time.Items.Add(0.001);
         commonToolStrip.time.Items.Add(0.0005);
         commonToolStrip.time.Items.Add(0.0002);
         commonToolStrip.time.SelectedIndex = 10;

         this.toolStripContainer1.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());

         gd.SetVerticalRange(0, 255, (float)(255.0 / 6.5), "Volts");

         graphControl.SetRenderer(gd);
      }

   }
}
