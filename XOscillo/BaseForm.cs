﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XOscillo
{
   public partial class BaseForm : Form
   {
      public BaseForm()
      {
         InitializeComponent();
      }

      virtual public DataBlock GetDataBlock()
      {
         return null;
      }

      virtual public void SetDataBlock(DataBlock db)
      {
      }

   }
}