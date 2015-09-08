namespace XOscillo.Graph
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
