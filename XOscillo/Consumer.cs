using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class Consumer
   {
      protected Consumer nextConsumer;

      public Consumer()
      {
         nextConsumer = null;
      }

      public Consumer(Consumer next)
      {
         nextConsumer = next;
      }

      public virtual void SetDataBlock(DataBlock db)
      {
         if (nextConsumer != null)
         {
            nextConsumer.SetDataBlock(db);
         }
      }
   }
}
