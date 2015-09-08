using System;
using XOscillo.Graph;

namespace XOscillo.VizForms
{
   class FileAnalogVizForm : AnalogVizForm
   {
      DataBlock dataBlock;

      public FileAnalogVizForm(SerializationHelper sh)
         : base()
      {
         ga = new GraphAnalog(sh.graph);
         dataBlock = sh.dataBlock;         
      }

      override public void Form_Load(object sender, EventArgs e)
      {
         GetFirstConsumerInChain().SetDataBlock(dataBlock);
         SetToolbar(GetFilteringToolStrip());
         SetToolbar(GetFftToolStrip());

         commonToolStrip = new CommonToolStrip(this, null, graphControl, null);
         float[] divs = { 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f, 0.005f, 0.002f, 0.001f, 0.0002f, 0.0005f, 0.00002f };
         foreach (float t in divs)
         {
            commonToolStrip.time.Items.Add(t);
         }
         commonToolStrip.time.SelectedIndex = 10;
         SetToolbar(commonToolStrip);
      }

      override public void UpdateGraph(object sender, EventArgs e)
      {
         GetFirstConsumerInChain().SetDataBlock(dataBlock);
         Invalidate();
      }
   }
}
