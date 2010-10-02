using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class VizBuffer : VizForm
   {
      public VizBuffer()
      {
         InitializeComponent();
      }

      override public DataBlock GetDataBlock()
      {
         return graphControl.GetScopeData();
      }

      override public void CopyFrom(VizForm vf)
      {
         DataBlock db = new DataBlock( vf.GetDataBlock() );

         graphControl.SetScopeData(db);
      }

      public void LoadDataFromFile(string filename)
      {
         DataBlock db = new DataBlock();
         db.LoadXML(filename);
         graphControl.SetScopeData(db);
         Invalidate();
      }

      private void fft_Click(object sender, EventArgs e)
      {
         graphControl.DrawFFT( fft.Checked );
         Invalidate();
      }

      private void clone_Click(object sender, EventArgs e)
      {
         VizBuffer childForm = new VizBuffer();
         childForm.MdiParent = MdiParent;
         childForm.Text = Text;// +Parent.childFormNumber++;
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
         childForm.CopyFrom(this);
      }

      private void VizBuffer_Load(object sender, EventArgs e)
      {
         time.Items.Add(1.0);
         time.Items.Add(0.5);
         time.Items.Add(0.2);
         time.Items.Add(0.1);
         time.Items.Add(0.05);
         time.Items.Add(0.02);
         time.Items.Add(0.01);
         time.Items.Add(0.005);
         time.Items.Add(0.002);
         time.Items.Add(0.001);
         time.Items.Add(0.0005);
         time.Items.Add(0.0002);
         time.Items.Add(0.0001);
         time.Items.Add(0.00005);
         time.SelectedIndex = 10;
      }

      private void time_SelectedIndexChanged(object sender, EventArgs e)
      {
         float secondsPerDiv = float.Parse(time.SelectedItem.ToString());
         graphControl.SetSecondsPerDivision(secondsPerDiv);
         Invalidate();
      }
   }
}
