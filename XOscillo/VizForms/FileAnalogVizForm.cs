using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   class FileAnalogVizForm : AnalogVizForm
   {
      DataBlock dataBlock;

      public FileAnalogVizForm(SerializationHelper sh)
         : base()
      {
         ga = new GraphAnalog(sh.graph);
         dataBlock = sh.dataBlock;
         Init();
         fc.SetDataBlock(dataBlock);
      }

      override public void Form1_Load(object sender, EventArgs e)
      {
         toolStripContainer.TopToolStripPanel.Controls.Add(ft.GetToolStrip());
         toolStripContainer.TopToolStripPanel.Controls.Add(fft.GetToolStrip());

         base.Form1_Load(sender, e);
      }

      override public void UpdateGraph(object sender, EventArgs e)
      {
         fc.SetDataBlock(dataBlock);
         Invalidate();
      }
   }
}
