using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class MyToolbar
   {
      protected System.Windows.Forms.ToolStrip toolStrip;

      public MyToolbar()
      {
         this.toolStrip = new System.Windows.Forms.ToolStrip();
      }

      public System.Windows.Forms.ToolStrip GetToolStrip()
      {
         return toolStrip;
      }

      virtual public MyToolbar GetCopy(MyToolbar mt)
      {
         return null;
      }

   }
}
