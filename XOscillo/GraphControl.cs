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
      private Rectangle oldBounds = new Rectangle();

      private DateTime oldTime = DateTime.Now;

      public DataBlock ScopeData = null;

      private bool m_drawFFT = false;

      Pen[] m_pens = { Pens.Red, Pens.Blue, Pens.Green, Pens.Yellow };

      public List<int> Lines = new List<int>();

		public GraphControl()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

		}

      public void SetSecondsPerDivision(float secondsPerDiv)
      {
         m_secondsPerDiv = secondsPerDiv;
         Invalidate();
      }

      public void DrawFFT(bool value)
      {
         m_drawFFT = value;
         Invalidate();
      }

      private int lerp(int y0, int y1, int x0, int x1, int x)
      {
         if (x0 == x1)
            return 0;
         return y0 + ((x-x0)*(y1-y0))/(x1-x0);
      }

      private double lerp(double y0, double y1, double x0, double x1, double x)
      {
         return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
      }

      private string GetTime(double f)
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

      private string FormatSeconds(double value)
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

      private void DrawHorizontalLines(Graphics g, Rectangle r)
      {
         float yDelta = (float)Height / 8.0f;

         Pen p = new Pen(Color.Gray );
         p.DashStyle= System.Drawing.Drawing2D.DashStyle.Custom;
         p.DashPattern = new float[] {1,5 };

         for (int y = 0; y < 8; y++)
         {
            g.DrawLine(p, 0, (int)(yDelta * y), r.Width, (int)(yDelta * y));
         }
      }

      private void DrawVerticalLines(Graphics g, Rectangle r)
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

            g.DrawLine(Pens.Gray, t, r.Y, t, r.Y + r.Height);

            pp.X = t;
            pp.Y = 0;
            g.DrawString( FormatSeconds(time), this.Font, Brushes.White, pp );

            if (t > r.Width)
               break;
         }
      }

      private void DrawGraph(Graphics g, Rectangle r, Pen p, int channel)
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
               g.DrawLine(p, xx, yy, x, y);
            }

            yy = y;
            xx = x;

            if (xx > r.Width)
               break;
         }
      }

      public void DrawOscillo(Graphics g, Rectangle r)
      {
         if (oldBounds.Width != Width || oldBounds.Height != Height)
         {
            if (hScrollBar1 != null)
            {
               int i = 256 * r.Width / r.Height;
               int nv = (hScrollBar1.LargeChange / 2) - (i / 2);
               if (hScrollBar1.Value + nv > 0)
                  hScrollBar1.Value += nv;
               hScrollBar1.LargeChange = i;

               hScrollBar1.Maximum = lerp(0, 32, 0, (int)(ScopeData.m_sampleRate * m_secondsPerDiv), ScopeData.GetChannelLength());

               hScrollBar1.Visible = (i <= hScrollBar1.Maximum);
            }

            oldBounds = this.Bounds;
         }

         DrawHorizontalLines(g, r);
         DrawVerticalLines(g, r);

         //draw channels
         lock (ScopeData)
         {
            for (int ch = 0; ch < ScopeData.m_channels; ch++)
            {
               DrawGraph(g,r, m_pens[ch], ch);
            }
         }
      }

      fft f;
      private void DrawFFT(Graphics g, Rectangle r)
      {
         int samplesPerChannel = ScopeData.m_Buffer.Length / ScopeData.m_channels;

         if ( f == null)
         {
            f = new fft(samplesPerChannel);
         }

         f.SetData(ScopeData.m_Buffer, 0, samplesPerChannel);
         f.FFT(0);

         hScrollBar1.Maximum = 1024;
         hScrollBar1.LargeChange = r.Width;
         hScrollBar1.Visible = (r.Width < 1024);

         int maxFreq = ScopeData.m_sampleRate / 2;
         int minFreq = 0;

         double power = 10000.0/f.Power(0);

         //margin at the bottom
         r.Height -= 20;

         int timeoffset = 0;
         if (hScrollBar1.Visible)
         {
            timeoffset = (int)lerp(0, 512, 0, hScrollBar1.Maximum, hScrollBar1.Value);
            r.Height -= hScrollBar1.Height;
         }

         //draw bars
         for (int i = 0 ; i < 512; i++)         
         {
            int x = 2 * (i - timeoffset);
            g.DrawLine(Pens.Red, x, r.Bottom, x, r.Bottom - (int)(f.Power(i) * power));
         }

         int freqStep;

         for (freqStep = 500; freqStep < maxFreq; freqStep+=500)
         {
            int ft = lerp(0, 512, minFreq, maxFreq, freqStep); 
            if (ft > 30)
               break ;
         }

         //draw legend
         Point pp = new Point();
         for (int i = 0; i < (int)maxFreq; i += freqStep)
         {
            int x = lerp(0, 512, minFreq, maxFreq, i );

            pp.X = 2 * (x - timeoffset);
            pp.Y = r.Bottom;

            g.DrawLine(Pens.Gray, pp.X, 0, pp.X, pp.Y);
            g.DrawString(string.Format("{0}", i ), this.Font, Brushes.White, pp);
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

         e.Graphics.Clear(Color.Black);
         if (m_drawFFT)
         {
            DrawFFT(e.Graphics, this.Bounds);
         }
         else
         {
            DrawOscillo(e.Graphics, this.Bounds);
         }

         //draw horizontal lines
         foreach (int i in Lines)
         {
            int y = lerp(0, Bounds.Height, 255, 0, i);
            e.Graphics.DrawLine(m_pens[0], 0, y, Bounds.Width, y);
         }

         Point pp = new Point();
         pp.X = 0;
         pp.Y += 16;
         e.Graphics.DrawString(string.Format("{0} fps", 1000 / duration.Milliseconds), this.Font, Brushes.White, pp);
         
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

         //e.Graphics.DrawImageUnscaled(backBuffer, 0, 0);
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
