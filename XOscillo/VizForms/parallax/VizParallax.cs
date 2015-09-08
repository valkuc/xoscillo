using System;
using XOscillo.Acquirers;
using XOscillo.Autodetection;

namespace XOscillo.VizForms.Parallax
{
    public partial class VizParallax : AnalogVizForm
    {
        SerialParallax oscillo;

        float[] divs = { 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 500e-6f, 200e-6f, 100e-6f, 50e-6f };
        int[] samplerates = { 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000 };

        override public bool Init()
        {
            Autodetection<SerialParallax> au = new Autodetection<SerialParallax>();
            oscillo = au.Detection();
            if (oscillo == null)
                return false;

            Text = oscillo.GetName();

            return base.Init();
        }


        override public void Form_Load(object sender, EventArgs e)
        {
            m_Acq = new Acquirer();
            m_Acq.Open(oscillo, GetFirstConsumerInChain());
            gf.drawSlidingFFT = true;

            commonToolStrip = new CommonToolStrip(this, m_Acq, graphControl, oscillo);

            foreach (float t in divs)
            {
                commonToolStrip.time.Items.Add(t);
            }
            commonToolStrip.selectedIndexChanged += this.time_SelectedIndexChanged;
            commonToolStrip.time.SelectedIndex = 10;

            SetToolbar(GetFilteringToolStrip());
            SetToolbar(GetFftToolStrip());
            SetToolbar(new ParallaxToolStrip(oscillo, graphControl));
            SetToolbar(commonToolStrip);

            ga.SetVerticalRange(0, 255, 32, "Volts");
        }

        private void time_SelectedIndexChanged(object sender, EventArgs e)
        {
            oscillo.SetSampleRate(samplerates[commonToolStrip.time.SelectedIndex]);
        }
    }
}
