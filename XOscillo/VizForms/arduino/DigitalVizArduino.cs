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
    public partial class DigitalVizArduino : DigitalVizForm
    {
        DigitalArduino oscillo = new DigitalArduino();

        public override bool Init()
        {
            Autodetection<DigitalArduino> au = new Autodetection<DigitalArduino>();
            oscillo = au.Detection();
            if (oscillo == null)
                return false;
            
            Text = oscillo.GetName();
            
            return base.Init();
        }


        override public void Form_Load(object sender, EventArgs e)
        {
            m_Acq = new Acquirer();
            m_Acq.Open(oscillo, graphControl.GetConsumer());
            SetToolbar(new DigitalArduinoToolStrip(oscillo, graphControl));

            commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl, oscillo);

            float[] divs = { 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 0.0002f, 0.0005f };
            foreach (float t in divs)
            {
                commonToolStrip.time.Items.Add(t);
            }
            commonToolStrip.time.SelectedIndex = 10;

            SetToolbar(commonToolStrip);

            gd.SetVerticalRange(0, 255, (float)(255.0 / 6.5), "Volts");

            graphControl.SetRenderer(gd);
        }

    }
}
