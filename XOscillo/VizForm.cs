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

   }
}
