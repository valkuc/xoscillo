using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class GraphConsumer : Consumer
   {      
      GraphControl graphControl;

      public GraphConsumer(GraphControl gc)
      {
         graphControl = gc;
      }

      override public void SetDataBlock(DataBlock db)
      {
         graphControl.SetScopeData(db);
      }
   }
}
