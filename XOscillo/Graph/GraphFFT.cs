using System.Drawing;
using System.Windows.Forms;

namespace XOscillo.Graph
{
   public class GraphFFT : Graph
   {
      FFT f;

      public bool drawSlidingFFT = false;

      MouseEventArgs m_mouse = null;

      public GraphFFT()
         : base()
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

      override public void ResizeToRectangle(HScrollBar hBar)
      {
         hBar.LargeChange = m_Bounds.Width;
         hBar.Maximum = 1024;

         SetDisplayWindow(hBar.Value, 1024);
      }

      Point pp = new Point();

      override public void Draw(Graphics g, DataBlock db)
      {
         Rectangle r = new Rectangle();

         r = m_Bounds;

         if (f == null)
         {
            f = new FFT(1024);
         }

         if (db.m_result != DataBlock.RESULT.OK)
         {
             pp.X = 0;
             pp.Y = 0;
             g.DrawString(string.Format("Waiting for a chunk of data to analyze"), parent.Font, Brushes.White, pp);
             return;
         }

         if (db.GetChannelLength() < f.GetNumOfSamples())
         {
            pp.X = 0;
            pp.Y = 0;
            g.DrawString(string.Format("FFT needs at least 1024 samples to work, got only {0}, try increasing the measurement time",db.GetChannelLength()) , parent.Font, Brushes.White, pp);
            return;
         }

         for (int i = 0; i < f.GetNumOfSamples(); i++)
         {
             f.x[i] = db.GetVoltage(0, i);
             f.y[i] = 0;
         }
         f.DoFFT(0);

         int maxFreq = db.m_sampleRate / 2;
         int minFreq = 0;

         //margin at the bottom
         r.Height -= 20;
         r.Width = 1024;

         r.X -= (int)MinXD;

         if (drawSlidingFFT)
         {
            DrawSlidingFFT(g, r, db);

            r.Y -= 256;
            DrawFFTBars(g, r);
            r.Y += 256;
         }
         else
         {
            DrawFFTBars(g, r);
         }

         int freqStep;

         for (freqStep = 500; freqStep < maxFreq; freqStep += 500)
         {
             int ft = lerp(0, f.GetNumOfSamples() / 2, minFreq, maxFreq, freqStep);
            if (ft > 30)
               break;
         }

         //draw legend
         for (int i = 0; i < (int)maxFreq; i += freqStep)
         {
            int x = lerp(0, 512, minFreq, maxFreq, i);

            pp.X = r.X + 2 * x;
            pp.Y = r.Bottom;

            g.DrawLine(Pens.Gray, pp.X, 0, pp.X, pp.Y);
            g.DrawString(string.Format("{0}", i), parent.Font, Brushes.White, pp);
         }

         if (m_mouse != null)
         {
             DrawCross(g, Pens.Blue, m_mouse.X, m_mouse.Y);

             pp.X = r.X ;
             pp.Y = r.Y + 40;

             g.DrawString(string.Format("Freq: {0} Hz", ((m_mouse.X / 2) * maxFreq) / f.GetNumOfSamples() / 2), parent.Font, Brushes.White, pp);
         }

      }
      public override void GraphControl_MouseMove(object sender, MouseEventArgs e)
      {
          base.GraphControl_MouseMove(sender, e);
          m_mouse = e;
          Control ctr = sender as Control;
          ctr.Invalidate();
      }

   }
}
