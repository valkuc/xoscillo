using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   class GraphFFT : Graph
   {
      fft f;

      public GraphFFT(Control cntrl, HScrollBar h)
         : base(cntrl, h)
      {
      }

      private void DrawFFTBars(Graphics g, Rectangle r)
      {
         //draw bars
         for (int i = 0; i < 512; i++)
         {
            int x = r.X + 2 * i;
            //g.DrawLine(Pens.Red, x, r.Bottom, x, r.Bottom - (int)(f.Power(i) * power)/10);
            g.DrawLine(Pens.Red, x, r.Bottom, x, r.Bottom - (int)(f.Power(i) / 128));
         }
      }

      Bitmap bmp;

      Color[] shade;

      public void DrawSlidingFFT(Graphics g, Rectangle r, DataBlock db)
      {
         if (bmp == null)
         {
            bmp = new Bitmap(512, 256);
            shade = new Color[256];
            for (int i = 0; i < 256; i++)
            {
               shade[i] = System.Drawing.Color.FromArgb(i, 0, 255 - i);
            }
         }

         for (int i = 0; i < 512; i++)
         {
            double power = f.Power(i) / 64;

            if (power > 255)
               power = 255;
            byte p = (byte)power;

            bmp.SetPixel(i, db.m_sample & 0xff, shade[p]);
         }

         int yy = r.Y + r.Height - 256;
         g.DrawImage(bmp, r.X, yy, 1024, 256);

         int y = yy + (db.m_sample + 1 & 0xff);
         g.DrawLine(Pens.Red, r.X, y, 1024, y);
      }

      public void DrawFFT(Graphics g, DataBlock db)
      {
         Rectangle r = new Rectangle();

         r = m_Bounds;

         g.Clear(Color.Black);

         if (f == null)
         {
            f = new fft(db.GetChannelLength());
         }

         f.SetData(db.m_Buffer, 0, db.GetChannelLength());
         f.FFT(0);

         m_hBar.LargeChange = r.Width;
         m_hBar.Maximum = 1024;

         int maxFreq = db.m_sampleRate / 2;
         int minFreq = 0;

         //margin at the bottom
         r.Height -= 20;
         r.Width = 1024;

         if (m_hBar.Visible)
         {
            r.Height -= m_hBar.Height;
         }

         r.X -= m_hBar.Value;

         if (false)
         {
            DrawFFTBars(g, r);
         }
         else
         {
            DrawSlidingFFT(g, r, db);

            r.Y -= 256;
            DrawFFTBars(g, r);
            r.Y += 256;
         }

         int freqStep;

         for (freqStep = 500; freqStep < maxFreq; freqStep += 500)
         {
            int ft = lerp(0, 512, minFreq, maxFreq, freqStep);
            if (ft > 30)
               break;
         }

         //draw legend
         Point pp = new Point();
         for (int i = 0; i < (int)maxFreq; i += freqStep)
         {
            int x = lerp(0, 512, minFreq, maxFreq, i);

            pp.X = r.X + 2 * x;
            pp.Y = r.Bottom;

            g.DrawLine(Pens.Gray, pp.X, 0, pp.X, pp.Y);
            g.DrawString(string.Format("{0}", i), m_cntrl.Font, Brushes.White, pp);
         }
      }

   }
}
