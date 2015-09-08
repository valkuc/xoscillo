using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo.Graph
{
	public partial class GraphControl : UserControl
	{
      private DateTime oldTime = DateTime.Now;

      private DataBlock ScopeData = null;

      public List<int> Lines = new List<int>();
      public List<int> VerticalLines = new List<int>();

      GraphConsumer graphConsumer = null;

      private Graph graph;

      bool waitingForTrigger = true;

      float m_secondsPerDiv = 1.0f;

		public GraphControl()
		{
			InitializeComponent();
        
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

         graphConsumer = new GraphConsumer(this);
         timer.Start();
		}

      public Graph SetRenderer(Graph gr)
      {
         Graph oldRenderer = graph;

         if ( graph != null)
         {
            MouseMove -= graph.GraphControl_MouseMove;
            gr.parent = null;
         }

         graph = gr;

         if (graph != null)
         {
            gr.parent = this;
            MouseMove += gr.GraphControl_MouseMove;
         }

         return oldRenderer;
      }

      public void SetSecondsPerDivision(float secondsPerDiv)
      {
         m_secondsPerDiv = secondsPerDiv;
         Invalidate();
      }

      public void SetMinMaxVoltages(int min, int max)
      {
         graph.SetVerticalRange(min, max, 32, "Volts");
      }

      public void SetScopeData( DataBlock db )
      {
         lock(this)
         {
            ScopeData = db;

            //reset timeout
            waitingForTrigger = false;

            Invalidate();
         }
      }

      public Consumer GetConsumer()
      {
         return graphConsumer;
      }
      
      public DataBlock GetScopeData()
      {
          return ScopeData;
          /*
         lock (this)
         {
            return new DataBlock(ScopeData);
         }
           */
      }
      

      private void UserControl1_Paint(object sender, PaintEventArgs e)
      {
         DateTime currentTime = DateTime.Now;
         TimeSpan duration = currentTime - oldTime;
         oldTime = currentTime;

         if ( Width == 0 || Height == 0 )
         {
            return;
         }

         e.Graphics.Clear(Color.Black);
         Rectangle r = this.Bounds;
         r.Height -= hScrollBar1.Height;

         if (ScopeData != null && ScopeData.m_channels != 0)
         {
            graph.SetHorizontalRange(0, ScopeData.GetTotalTime(), m_secondsPerDiv, "Time");
            graph.SetRectangle(r);
            graph.ResizeToRectangle(hScrollBar1);
            graph.Draw(e.Graphics, ScopeData);
         }

         {
            Point pp = new Point();
            pp.X = 0;
            pp.Y += 16;

            //display message if .5 seconds elapsed without data
            if (waitingForTrigger)
            {
               e.Graphics.DrawString("Scope maybe waiting for trigger... ", this.Font, Brushes.White, pp);
            }
            else
            {
               if (duration.Milliseconds > 0)
               {
                  e.Graphics.DrawString(string.Format("{0} fps", 1000 / duration.Milliseconds), this.Font, Brushes.White, pp);
               }

               //reset waiting for trigger timer
               timer.Stop();
               timer.Start();
            }

         }
      }

		private void UserControl1_Resize(object sender, EventArgs e)
		{
         if (graph!=null)
         {
            graph.ResizeToRectangle(hScrollBar1);
            Invalidate();
         }
		}

      private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
      {
         Invalidate();
      }

      private void timer_Tick(object sender, EventArgs e)
      {
         waitingForTrigger = true;
         Invalidate();
         timer.Stop();
      }

	}
}
