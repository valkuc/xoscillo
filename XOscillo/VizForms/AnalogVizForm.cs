using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace XOscillo
{
   public class AnalogVizForm : VizForm
   {
      protected GraphAnalog ga;
      protected GraphFFT gf;

      protected FilterConsumer fc;
      protected FilteringToolStrip ft;
      protected FftToolStrip fft;

      public AnalogVizForm() : base()
      {
         ga = new GraphAnalog();
         gf = new GraphFFT();
      }

      public AnalogVizForm(AnalogVizForm avf) : base ()
      {
         ga = new GraphAnalog(avf.ga);
         gf = new GraphFFT();
         Init();
      }

      override public bool Init()
      {
         fc = new FilterConsumer(graphControl.GetConsumer());
         ft = new FilteringToolStrip(fc);
         ft.dataChanged += UpdateGraph;
         fft = new FftToolStrip(graphControl, gf);

         graphControl.SetRenderer(ga);

         gf.SetVerticalRange(0, 1024, 32, "power");

         return true;
      }

      virtual public void UpdateGraph(object sender, EventArgs e)
      {
      }


      override public VizForm Clone()
      {
         SerializationHelper sh = new SerializationHelper();
         sh.dataBlock = this.GetDataBlock();
         sh.graph = new Graph(ga);

         FileAnalogVizForm avf = new FileAnalogVizForm(sh);
         avf.MdiParent = MdiParent;
         avf.Text = Text;// +Parent.childFormNumber++;
         avf.Show();
         return avf;
      }

      public override void SaveXML(FileStream stream)
      {
         SerializationHelper sh = new SerializationHelper();
         sh.dataBlock = this.graphControl.GetScopeData();
         sh.graph = new Graph(ga);

         sh.SaveXML(stream);
      }

   }
}
