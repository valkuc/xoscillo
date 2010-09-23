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
		Graphics drawingArea;
		Bitmap backBuffer;
      
		DateTime oldTime = DateTime.Now;

      SolidBrush m_brushWhite = new SolidBrush(Color.White);

      double ts = 1;

      public DataBlock ScopeData;

      public bool m_drawFFT = false;

      Pen[] m_pens = { new Pen(Color.Red), new Pen(Color.Blue), new Pen(Color.Green) };

      public List<int> Lines = new List<int>();

		public GraphControl()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			backBuffer = new Bitmap(this.Width, this.Height);
			drawingArea = Graphics.FromImage(backBuffer);

         ScopeData = new DataBlock();
		}

      public int lerp( int y0, int y1, int x0, int x1, int x )
      {
         if (x0 == x1)
            return 0;
         return y0 + ((x-x0)*(y1-y0))/(x1-x0);
      }

      public double lerp(double y0, double y1, double x0, double x1, double x)
      {
         return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
      }

      string GetTime(double f)
      {
         double z = Math.Log10(f);
         if (z < -2)
         {
            return string.Format("{0}ms", f * 1000);
         }
         else if (z < -6)
         {
            return string.Format("{0}us", f * 1000000);
         }

         return string.Format("{0}us", f );
      }

      float m_secondsPerDiv=1.0f;

      public void SetSecondsPerDivision( float secondsPerDiv )
      {
         m_secondsPerDiv = secondsPerDiv;
      }

      public string FormatSeconds( double value )
      {
         if (Math.Abs(value) < .0009)
         {
            return string.Format("{0:0.###}us", value * 1000000);
         }
         else if (Math.Abs(value) < .09)
         {
            return string.Format("{0:0.###}ms", value * 1000);
         }
         else
         {
            return string.Format("{0:0.###}s", value);
         }
      }

      public void DrawHorizontalLines(Rectangle r)
      {
         float yCentre = (float)r.Height / 2.0f;

         float yDelta = (float)yCentre / 4.0f;
         float yFineDelta = (float)yDelta / 5.0f;

         for (int y = 0; y < 4; y++)
         {
            for (int i = 0; ; i++)
            {
               int x = (int)(yFineDelta * i);
               if (x < backBuffer.Width)
               {
                  backBuffer.SetPixel(x, (int)(yCentre + yDelta * y), Color.Gray);
                  backBuffer.SetPixel(x, (int)(yCentre - yDelta * y), Color.Gray);
               }
               else
               {
                  break;
               }
            }
         }
      }

      public void DrawVerticalLines(Rectangle r)
      {
         double timeoffset = 0;
         if (hScrollBar1.Visible)
         {
            timeoffset = lerp( 0.0, ScopeData.GetTotalTime(), 0.0, hScrollBar1.Maximum, hScrollBar1.Value);
         }

         Point pp = new Point();
         for (int i=0; ; i++)
         {
            double time = m_secondsPerDiv * i;

            int t = (int)lerp(0, r.Height / 8.0, 0, m_secondsPerDiv, (time - timeoffset) );

            drawingArea.DrawLine(Pens.Gray, t, r.Y, t, r.Y + r.Height);

            pp.X = t;
            pp.Y = 0;
            drawingArea.DrawString( FormatSeconds(time), this.Font, m_brushWhite, pp );

            if (t > r.Width)
               break;
         }
      }

      void DrawGraph(Rectangle r, Pen p, int channel )
      {
         float timeoffset = 0;

         if (hScrollBar1.Visible)
         {
            timeoffset = (float)lerp((double)0, ScopeData.GetTotalTime(), (double)0, (double)hScrollBar1.Maximum, (double)hScrollBar1.Value);
         }

         int yy = 0;
         int xx = 0;
         int length = ScopeData.GetChannelLength();

         for (int i = 0; i < length; i++)
         {
            int rawvolt = ScopeData.GetVoltage( channel, i );

            float time = ScopeData.GetTime(i);

            int x = (int)lerp(0, r.Height / 8.0, 0, m_secondsPerDiv , (time - timeoffset) );
            int y = lerp(0, r.Height, 255, 0, rawvolt);

            if (i > 0)
            {
               drawingArea.DrawLine(p, xx, yy, x, y);
            }

            yy = y;
            xx = x;

            if (xx > r.Width)
               break;
         }
      
      }

      public void DrawOscillo(Rectangle r)
      {
         hScrollBar1.Maximum = lerp(0, 32, 0, (int)(ScopeData.m_sampleRate * m_secondsPerDiv), ScopeData.GetChannelLength());
         int x2 = backBuffer.Width * 256 / backBuffer.Height;
         hScrollBar1.Visible = (x2 <= hScrollBar1.Maximum);

         DrawHorizontalLines(r);
         DrawVerticalLines(r);

         //draw channels
         for (int ch = 0; ch < ScopeData.m_channels; ch++)
         {
            DrawGraph(r, m_pens[ch], ch);
         }
      }

      fft f;
      public void DrawFFT( Rectangle r )
      {
         int samplesPerChannel = ScopeData.m_Buffer.Length / ScopeData.m_channels;

         if ( f == null)
         {
            f = new fft(samplesPerChannel);
         }

         f.SetData(ScopeData.m_Buffer, 0, samplesPerChannel);
         f.FFT(0);

         Point pp = new Point();

         hScrollBar1.Maximum = 1024;
         hScrollBar1.LargeChange = r.Width;
         hScrollBar1.Visible = r.Width < 1024;
         

         int maxFreq = ScopeData.m_sampleRate / 2;
         int minFreq = 0;// (float)ScopeData.m_sampleRate / length;

         double power = 10000.0/f.Power(0);

         //margin at the bottom
         r.Height -= 20;

         int timeoffset = 0;
         if (hScrollBar1.Visible)
         {
            timeoffset = (int)lerp(0, 512, 0, hScrollBar1.Maximum, hScrollBar1.Value);
         }

         //draw bars
         for (int i = 0 ; i < 512; i++)         
         {
            int x = 2 * (i - timeoffset);
            drawingArea.DrawLine(Pens.Red, x, r.Bottom, x, r.Bottom - (int)(f.Power(i) * power));
         }

         //draw legend
         for (int i = 0; i < (int)maxFreq; i += 500)
         {
            int x = lerp(0, 512, minFreq, maxFreq, i );

            pp.X = 2 * (x - timeoffset);
            pp.Y = r.Bottom;

            drawingArea.DrawLine(Pens.Gray, pp.X, 0, pp.X, pp.Y);
            drawingArea.DrawString(string.Format("{0}", i ), this.Font, m_brushWhite, pp);
         }
      }


		private void UserControl1_Paint(object sender, PaintEventArgs e)
      {
         DateTime currentTime = DateTime.Now;
         TimeSpan duration = currentTime - oldTime;
         oldTime = currentTime;

         if ( e.ClipRectangle.Width == 0 || e.ClipRectangle.Height == 0 )
         {
            return;
         }

         if (ScopeData == null || ScopeData.m_channels == 0)
         {
            return;
         }

         drawingArea.Clear(Color.Black);

         if (m_drawFFT)
         {
            DrawFFT(e.ClipRectangle);
         }
         else
         {
            DrawOscillo(e.ClipRectangle);
         }

         Pen[] pens = { new Pen(Color.Red), new Pen(Color.Blue) };
         foreach (int i in Lines)
         {
            int y = lerp(0, backBuffer.Height, 255, 0, i);
            drawingArea.DrawLine(pens[0], 0, y, backBuffer.Width, y);
         }
         /*
         pp.X = 0;
         pp.Y += 16;
         drawingArea.DrawString(string.Format("{0} fps", 1000/duration.Milliseconds), this.Font, m_brushWhite, pp);
          */
         /*
         pp.Y += 16;
         ts = (ScopeData.m_stop - ScopeData.m_start).TotalMilliseconds; 
         drawingArea.DrawString(string.Format("{0}", ts), this.Font, m_brushWhite, pp);
         */
         /*
         if (hScrollBar1 != null)
         {
            pp.Y += 16;
            drawingArea.DrawString(string.Format("{0}x{1} {2}-{3}", Width, Height, hScrollBar1.Value, hScrollBar1.LargeChange), this.Font, m_brushWhite, pp);
         }
         */

         e.Graphics.DrawImageUnscaled(backBuffer, 0, 0);
      }


		private void UserControl1_Resize(object sender, EventArgs e)
		{
         if (Height == 0 || Width == 0)
            return;

			backBuffer = new Bitmap(this.Width, this.Height);
			drawingArea = Graphics.FromImage(backBuffer);

			if (hScrollBar1 != null)
			{
				int i = 256 * backBuffer.Width / backBuffer.Height;
				int nv = (hScrollBar1.LargeChange / 2) - (i / 2);
				if (hScrollBar1.Value + nv > 0)
					hScrollBar1.Value += nv;
				hScrollBar1.LargeChange = i;
			}
         Invalidate();
		}

      private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
      {
         Invalidate();
      }

	}
}
