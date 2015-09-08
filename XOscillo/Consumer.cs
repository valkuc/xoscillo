namespace XOscillo
{
   public class Consumer
   {
      protected readonly Consumer NextConsumer;

      public Consumer()
      {
         NextConsumer = null;
      }

      public Consumer(Consumer next)
      {
         NextConsumer = next;
      }

      public virtual void SetDataBlock(DataBlock db)
      {
         if (NextConsumer != null)
         {
            NextConsumer.SetDataBlock(db);
         }
      }
   }
}
