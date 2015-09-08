using System;
using System.IO;
using System.Windows.Forms;
using XOscillo.Acquirers;

namespace XOscillo.VizForms
{
    public abstract partial class VizForm : Form, DataBlockAware
   {
      protected Acquirer m_Acq = null;

      protected CommonToolStrip commonToolStrip = null;

      public VizForm()
      {
         InitializeComponent();
      }

      public DataBlock GetDataBlock()
      {
         return this.graphControl.GetScopeData();
      }

      public void SetDataBlock(DataBlock db)
      {
      }

      public abstract bool Init();

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
