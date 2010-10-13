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
      public VizForm()
      {
         InitializeComponent();
      }

      virtual public void CopyFrom(VizForm vf)
      {
      }

      virtual public bool Save(string filename)
      {
         return false;
      }

      public void Clone()
      {
         VizBuffer childForm = new VizBuffer();
         childForm.MdiParent = MdiParent;
         childForm.Text = Text;// +Parent.childFormNumber++;
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
         childForm.CopyFrom(this);
      }
   }
}
