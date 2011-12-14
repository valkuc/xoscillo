using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XOscillo
{
   public partial class VizForm : BaseForm
   {
      protected Acquirer m_Acq = null;

      protected CommonToolStrip commonToolStrip = null;

      public VizForm()
      {
         InitializeComponent();
      }

      virtual public bool Init()
      {
         return true;
      }

      override public DataBlock GetDataBlock()
      {
         return this.graphControl.GetScopeData();
      }

      virtual public void Form1_Load(object sender, EventArgs e)
      {         
         commonToolStrip = new CommonToolStrip(this, null, graphControl);

         float[] divs = { 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 0.0005f, 0.0002f, 0.0005f, 0.00002f };
         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         commonToolStrip.time.SelectedIndex = 10;

         this.toolStripContainer.TopToolStripPanel.Controls.Add(commonToolStrip.GetToolStrip());

      }

      virtual public VizForm Clone()
      {
         return null;
      }

      private void VizForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (m_Acq != null)
         {
            m_Acq.Close();
         }
      }

      public virtual void SaveXML(FileStream stream)
      {
      }
   }
}
