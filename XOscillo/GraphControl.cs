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
      Rectangle rDataBounds = new Rectangle();
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

      public string FormatSeconds( float value )
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
         float toltaTime = (float)(ScopeData.m_Buffer.Length) / (float)ScopeData.m_sampleRate;

         float time = 0;
         float timeoffset = 0;
         if (hScrollBar1.Visible)
         {
            timeoffset = (float)lerp((double)0, (double)toltaTime, (double)0, (double)hScrollBar1.Maximum, (double)hScrollBar1.Value);
         }

         Point pp = new Point();
         for (int i=0; ; i++)
         {
            double t = lerp(0, (r.Height * 1000.0) / 8.0, 0, (float)(m_secondsPerDiv * 1000), (time - timeoffset )* 1000) / 1000.0;

            drawingArea.DrawLine(Pens.Gray, (int)t, r.Y, (int)t, r.Y + r.Height);

            pp.X = (int)t;
            pp.Y = 0;
            drawingArea.DrawString(FormatSeconds(time), this.Font, m_brushWhite, pp);

            time += m_secondsPerDiv;

            if (t > r.Width)
               break;
         }
      }

      public void DrawWave(Rectangle r)
      {
         {
            int ii = lerp(0, 32, 0, (int)(ScopeData.m_sampleRate * m_secondsPerDiv), ScopeData.m_trigger);
            int x = lerp(0, backBuffer.Width, rDataBounds.Left, rDataBounds.Right, ii);

            if (x >= 0 && x < backBuffer.Width)
            {
               drawingArea.DrawLine(new Pen(Color.LightGreen), x, r.Y, x, r.Y + r.Height);
            }
         }

         for (int ch = 0; ch < ScopeData.m_channels; ch++)
         {
            DrawGraph(r, m_pens[ch], ch);
         }
         /*
         if (ScopeData.m_stride == 1)
         {
            
            for (int ch = 0; ch < ScopeData.m_channels; ch++)
            {
               DrawGraph(r, m_pens[ch], ScopeData.m_Buffer, ScopeData.m_Buffer.Length * ch / ScopeData.m_channels, 0, ScopeData.m_Buffer.Length / ScopeData.m_channels);
            }
         }
         else
         {
            for (int ch = 0; ch < ScopeData.m_channels; ch++)
            {
               DrawGraph(r, m_pens[ch], ScopeData.m_Buffer, ch, ScopeData.m_channels, ScopeData.m_Buffer.Length / ScopeData.m_channels);
            }
         }
          */
      }

      void DrawGraph2(Rectangle r, Pen p, byte[] buffer, int offset, int stride, int length)
      {
         int yy = 0;
         int xx = 0;

         float toltaTime = ScopeData.GetTotalTime();

         float time = 0;
         float timeoffset = 0;
         if (hScrollBar1.Visible)
         {
            timeoffset = (float)lerp((double)0, (double)toltaTime, (double)0, (double)hScrollBar1.Maximum, (double)hScrollBar1.Value);
         }

         for (int i = 0; i<buffer.Length; i++)
         {
            int rawvolt = (int)buffer[i * stride + offset];

            time = (float)i / ScopeData.m_sampleRate;

            int x = (int)(lerp(0, (r.Height * 1000.0) / 8.0, 0, (float)(m_secondsPerDiv * 1000), (time - timeoffset) * 1000) / 1000.0);
            int y = lerp(0, r.Height, 255, 0, rawvolt);

            if (i > 0)
            {
               drawingArea.DrawLine(p, xx, yy, x, y);
            }

            yy = y;
            xx = x;

            if (time > toltaTime)
               break;
         }
      }

      void DrawGraph(Rectangle r, Pen p, int channel )
      {
         float toltaTime = ScopeData.GetTotalTime();

         float time = 0;
         float timeoffset = 0;

         if (hScrollBar1.Visible)
         {
            timeoffset = (float)lerp((double)0, (double)toltaTime, (double)0, (double)hScrollBar1.Maximum, (double)hScrollBar1.Value);
         }

         int yy = 0;
         int xx = 0;
         int length = ScopeData.GetDataSize();

         for (int i = 0; i < length; i++)
         {
            int rawvolt = ScopeData.GetVoltage( channel, i );

            time = ScopeData.GetTime(i);

            int x = (int)(lerp(0, (r.Height * 1000.0) / 8.0, 0, (float)(m_secondsPerDiv * 1000), (time - timeoffset) * 1000) / 1000.0);
            int y = lerp(0, r.Height, 255, 0, rawvolt);

            if (i > 0)
            {
               drawingArea.DrawLine(p, xx, yy, x, y);
            }

            yy = y;
            xx = x;

            if (time > toltaTime)
               break;
         }
      
      }

      public void DrawOscillo(Rectangle r)
      {
         DrawHorizontalLines(r);
         DrawVerticalLines(r);
         DrawWave(r);
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

         float length = 1024;

         float maxFreq = (float)ScopeData.m_sampleRate / 2.0f;
         float minFreq = (float)ScopeData.m_sampleRate/ length;

         double power = 10000.0/f.Power(0);

         //draw bars
         for (int i = 1; i < 512; i++)         
         {
            drawingArea.DrawLine(Pens.Red, 2 * i, r.Bottom-30, 2 * i, r.Bottom - (int)(f.Power(i) * power)-30);
         }

         //draw legend
         for (int i = 0; i < (int)maxFreq; i += 500)
         {
            float x = (float)lerp(0.0, 512.0 * 2, minFreq, maxFreq, i);

            pp.X = (int)x;
            pp.Y = r.Bottom-30;

            drawingArea.DrawLine(Pens.Gray, x, 0, x, pp.Y );
            drawingArea.DrawString(string.Format("{0}", i ), this.Font, m_brushWhite, pp);
         }
      }


		private void UserControl1_Paint(object sender, PaintEventArgs e)
      {
         if (e.ClipRectangle.Width == 0 || e.ClipRectangle.Height == 0 )
         {
            return;
         }
         
         DateTime currentTime = DateTime.Now;
         TimeSpan duration = currentTime - oldTime;
         oldTime = currentTime;
         
         drawingArea.Clear(Color.Black);

         if (ScopeData == null || ScopeData.m_channels == 0)
         {
            return;
         }

         int samplesPerChannel = ScopeData.m_Buffer.Length / ScopeData.m_channels;

         hScrollBar1.Maximum = lerp(0, 32, 0, (int)(ScopeData.m_sampleRate * m_secondsPerDiv), samplesPerChannel);

         int s1 = (hScrollBar1.Value);
         int s2 = (hScrollBar1.Value + hScrollBar1.LargeChange);

         int x2 = backBuffer.Width * 256 / backBuffer.Height;

         if (x2 > hScrollBar1.Maximum)
         {
            s1 = 0;
            s2 = x2;

            hScrollBar1.Hide();
         }
         else
         {
            hScrollBar1.Show();
         }

         rDataBounds = Rectangle.FromLTRB( s1, 255, s2, 0 );

         ts = (ScopeData.m_stop - ScopeData.m_start).TotalMilliseconds;


         if (m_drawFFT)
         {
            DrawFFT(e.ClipRectangle);
         }
         else
         {
            //Rectangle r = e.ClipRectangle;
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

      private void displayControl_Load(object sender, EventArgs e)
      {

      }
	}
}
