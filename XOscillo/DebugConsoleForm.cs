using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class DebugConsoleForm : Form
   {
      public DebugConsoleForm()
      {
         InitializeComponent();
      }

      public void Add(string t)
      {
         text.Text += t;
      }

      private void DebugConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         e.Cancel = true;
         Hide();
      }
   }
}
