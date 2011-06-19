using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace XOscillo
{
	public partial class GraphControl : UserControl
	{
      private DateTime oldTime = DateTime.Now;

      private DataBlock ScopeData = null;

      private bool m_drawFFT = false;

      public List<int> Lines = new List<int>();
      public List<int> VerticalLines = new List<int>();

      private GraphAnalog ga;
      private GraphFFT gf;
      private GraphDigital gd;

      bool waitingForTrigger = true;

      float m_secondsPerDiv = 1.0f;
      double m_lpcf;

		public GraphControl()
		{
			InitializeComponent();

         ga = new GraphAnalog(this, hScrollBar1);
         gf = new GraphFFT(this, hScrollBar1);
         gd = new GraphDigital(this, hScrollBar1);

			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

         timer.Start();
		}

      public void SetSecondsPerDivision(float secondsPerDiv)
      {
         m_secondsPerDiv = secondsPerDiv;
         Invalidate();
      }

      public void SetMinMaxVoltages(int min, int max)
      {
         ga.SetVerticalRange(min, max, 32, "Volts");
      }

      public void DrawFFT(bool value)
      {
         m_drawFFT = value;
         Invalidate();
      }

      public void SetLowPassCutOffFrequency(double lpcf)
      {
         m_lpcf = lpcf;
         Invalidate();
      }

      public void SetScopeData( DataBlock db )
      {
         lock(this)
         {
            if (m_lpcf > 0)
            {
               ScopeData.Copy(db);
               ScopeData.LowPass(m_lpcf);
            }
            else
            {
               ScopeData = db;
            }

            //reset timeout
            waitingForTrigger = false;

            Invalidate();
         }
      }
      
      public DataBlock GetScopeData()
      {
         lock (this)
         {
            return new DataBlock(ScopeData);
         }
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
            if ( ScopeData.m_dataType == DataBlock.DATA_TYPE.ANALOG)
            {
               if (m_drawFFT)
               {
                  gf.SetVerticalRange(0, 1024, 32, "power");
                  gf.SetHorizontalRange(0, (float)(ScopeData.GetTotalTime() / 2.0), 1000, "Freq");
                  gf.SetRectangle(r);
                  gf.DrawFFT(e.Graphics, ScopeData);
               }
               else
               {
                  //draw channels
                  ga.SetVerticalRange(255, 0, 32, "Volts");
                  ga.SetHorizontalRange(0, ScopeData.GetTotalTime(), m_secondsPerDiv, "Time");
                  ga.SetRectangle(r);
                  ga.ResizeToRectangle();
                  
                  ga.DrawGraph(e.Graphics, ScopeData);
               }
            }
            else if (ScopeData.m_dataType == DataBlock.DATA_TYPE.DIGITAL)
            {
               gd.SetVerticalRange(0, 255, (float)(255.0/6.5), "Volts");
               gd.SetHorizontalRange(0, ScopeData.GetTotalTime(), m_secondsPerDiv, "Time");
               gd.SetRectangle(r);
               gd.ResizeToRectangle();
               gd.DrawGraph(e.Graphics, ScopeData);
            }
         }

         {
            Point pp = new Point();
            pp.X = 0;
            pp.Y += 16;


            //display message if .5 seconds elapsed without data
            if (waitingForTrigger)
            {
               e.Graphics.DrawString("Waiting for trigger... ", this.Font, Brushes.White, pp);
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
         Invalidate();
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
