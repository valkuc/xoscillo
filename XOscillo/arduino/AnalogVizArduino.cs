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

      Acquirer m_Acq = new Acquirer();

      public AnalogVizArduino()
      {
         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return graphControl.GetScopeData();
      }

      override public bool Init()
      {
         oscillo = new AnalogArduino();

         return m_Acq.Open(oscillo, graphControl);
      }

      private void Form1_Load(object sender, EventArgs e)
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

         AnalogArduinoToolbar aat = new AnalogArduinoToolbar(oscillo, graphControl);
         
         FilteringToolbar ft = new FilteringToolbar(oscillo, graphControl);

         this.toolStripContainer1.TopToolStripPanel.Controls.Add(ft.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(aat.GetToolStrip());
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         m_Acq.Close();
      }


   }
}
