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

      override public DataBlock GetDataBlock()
      {
         return this.graphControl.GetScopeData();
      }

      public virtual bool Init()
      {
         return true;
      }

      virtual public void Form_Load(object sender, EventArgs e)
      {         
      }

      public void SetToolbar( MyToolbar mt)
      {
         this.toolStripContainer.TopToolStripPanel.Controls.Add(mt.GetToolStrip());
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

      virtual public void UpdateGraph(object sender, EventArgs e)
      {
      }

   }
}
