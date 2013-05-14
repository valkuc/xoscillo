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

        private FilterConsumer fc;
        private FilteringToolStrip ft;
        private FftToolStrip fft;

        public AnalogVizForm(GraphAnalog ga, GraphFFT gf)
            : base()
        {
            this.ga = ga;
            this.gf = gf;
        }

        public AnalogVizForm()
            : this(new GraphAnalog(), new GraphFFT())
        {
        }

        public AnalogVizForm(AnalogVizForm avf)
            : this(new GraphAnalog(avf.ga), new GraphFFT())
        {
        }

        override public bool Init()
        {
            fc = new FilterConsumer(graphControl.GetConsumer());
            ft = new FilteringToolStrip(fc);
            fft = new FftToolStrip(graphControl, gf);

            ft.dataChanged += UpdateGraph;

            graphControl.SetRenderer(ga);

            gf.SetVerticalRange(0, 1024, 32, "power");
            return true;
        }

        public FilteringToolStrip GetFilteringToolStrip()
        {
            return ft;
        }

        public FftToolStrip GetFftToolStrip()
        {
            return fft;
        }

        public FilterConsumer GetFirstConsumerInChain()
        {
            return fc;
        }

        override public VizForm Clone()
        {
            SerializationHelper sh = new SerializationHelper();
            sh.dataBlock = this.GetDataBlock();
            sh.graph = new Graph(ga);

            FileAnalogVizForm avf = new FileAnalogVizForm(sh);
            avf.MdiParent = MdiParent;
            avf.Text = Text;// +Parent.childFormNumber++;
            avf.Init();
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
