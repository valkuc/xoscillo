using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   class OnlyNumbersToolStripTextBox : ToolStripTextBox
   {
      public event EventHandler textReady;

      protected override void OnKeyPress(KeyPressEventArgs e)
      {
         base.OnKeyPress(e);

         if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
         {
            e.Handled = true;
         }
      }

      protected override void OnLostFocus(EventArgs e)
      {
         if (textReady != null)
         {
            textReady(this, e);
         }
      }

      protected override void OnKeyDown( KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            System.Windows.Forms.SendKeys.Send("{TAB}");            
         }
      }
   }
}
