using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class VizForm : BaseForm
   {
      UInt32[] m_sampleRates = null;

      public VizForm()
      {
         InitializeComponent();

         SetSampleRates(new UInt32[15] { 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000 });
         SampleRateIndex = 7;
      }

      public UInt32[] GetSampleRates()
      {
         return m_sampleRates;
      }

      public void SetSampleRates(UInt32[] sampleRates)
      {
         m_sampleRates = sampleRates;

         time.Items.Clear();

         foreach (UInt32 t in sampleRates)
         {
            time.Items.Add(t.ToString());
         }
      }

      public int SampleRateIndex
      {
         get { return this.time.SelectedIndex; }
         set { this.time.SelectedIndex = value; }
      }

      public void CopyFrom(VizForm vf)
      {
         GetDataBlock().Copy(vf.GetDataBlock());
         SetSampleRates(vf.GetSampleRates());
         SampleRateIndex = vf.SampleRateIndex;
      }

      override public DataBlock GetDataBlock()
      {
         return this.graphControl.ScopeData;
      }

      override public void SetDataBlock(DataBlock db)
      {
         graphControl.ScopeData = db;
      }

      virtual public bool Save(string filename)
      {
         return false;
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (time.SelectedIndex >= 0)
         {
            UInt32 rate = m_sampleRates[time.SelectedIndex];
            graphControl.SetSecondsPerDivision((1.0f / (float)rate * 50.0f));
            graphControl.Invalidate();
         }
      }

      private void fftButton_CheckedChanged(object sender, EventArgs e)
      {
         graphControl.m_drawFFT = fftButton.Checked;
         graphControl.Invalidate();
      }

   }
}
