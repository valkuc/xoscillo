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

      private GraphLines2 gr;
      private GraphFFT gf;

      float m_secondsPerDiv = 1.0f;
      double m_lpcf;

		public GraphControl()
		{
			InitializeComponent();

         gr = new GraphLines2(this, hScrollBar1);
         gf = new GraphFFT(this, hScrollBar1);

			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

      public void SetSecondsPerDivision(float secondsPerDiv)
      {
         m_secondsPerDiv = secondsPerDiv;
         Invalidate();
      }

      public void SetMinMaxVoltages(int min, int max)
      {
         gr.SetVerticalRange(min, max, 32, "Volts");
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
               ScopeData.HighPass(m_lpcf);
            }
            else
            {
               ScopeData = db;
            }
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

         if (ScopeData == null || ScopeData.m_channels == 0)
         {
            return;
         }

         lock (this)
         {
            if (m_drawFFT)
            {
               gf.SetVerticalRange(0, 1024, 32, "power");
               gf.SetHorizontalRange(0, (float)(ScopeData.GetTotalTime()/2.0), 1000, "Freq");

               gf.DrawFFT(e.Graphics, this.Bounds, ScopeData);
            }
            else
            {
               //draw channels
               gr.SetVerticalRange(0, 255, 32, "Volts");
               gr.SetHorizontalRange(0, ScopeData.GetTotalTime(), m_secondsPerDiv, "Time");

               gr.DrawGraph(e.Graphics, this.Bounds, ScopeData);
            }
         }

         if (duration.Milliseconds > 0)
         {
            Point pp = new Point();
            pp.X = 0;
            pp.Y += 16;
            if (ScopeData.m_result == DataBlock.RESULT.OK)
            {
               e.Graphics.DrawString(string.Format("{0} fps", 1000 / duration.Milliseconds), this.Font, Brushes.White, pp);
            }
            else
            {
               e.Graphics.DrawString("Timeout!", this.Font, Brushes.White, pp);
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

	}
}
