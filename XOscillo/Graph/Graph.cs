using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   class Graph
   {
      protected float MinY;
      protected float MaxY;
      protected float DivY;
      string unitsX = "";

      protected float MinX;
      protected float MaxX;
      protected float DivX;
      string unitsY = "";

      protected float MinXD;
      protected float MaxXD;

      protected Control m_cntrl;
      protected HScrollBar m_h;

      public Graph(Control cntrl, HScrollBar h)
      {
         m_h = h;
         m_cntrl = cntrl;
      }

      public void SetHorizontalRange( float min, float max, float div, string units)
      {
         MinX = min;
         MaxX = max;
         DivX = div;
         unitsX = units;
      }

      public void SetVerticalRange( float min, float max, float div, string units)
      {
         MinY = min;
         MaxY = max;
         DivY = div;
         unitsY = units;
      }

      public void SetDisplayWindow(float min, float max)
      {
         MinXD = min;
         MaxXD = max;         
      }

      protected string ToEngineeringNotation(double d)
      {
         double exponent = Math.Log10(Math.Abs(d));
         if (Math.Abs(d) >= 1)
         {
            switch ((int)Math.Floor(exponent))
            {
               case 0:
               case 1:
               case 2:
                  return d.ToString("0.###");
               case 3:
               case 4:
               case 5:
                  return (d / 1e3).ToString("0.###") + "k";
               case 6:
               case 7:
               case 8:
                  return (d / 1e6).ToString("0.###") + "M";
               case 9:
               case 10:
               case 11:
                  return (d / 1e9).ToString() + "G";
               case 12:
               case 13:
               case 14:
                  return (d / 1e12).ToString() + "T";
               case 15:
               case 16:
               case 17:
                  return (d / 1e15).ToString() + "P";
               case 18:
               case 19:
               case 20:
                  return (d / 1e18).ToString() + "E";
               case 21:
               case 22:
               case 23:
                  return (d / 1e21).ToString() + "Z";
               default:
                  return (d / 1e24).ToString() + "Y";
            }
         }
         else if (Math.Abs(d) > 0)
         {
            switch ((int)Math.Floor(exponent))
            {
               case -1:
               case -2:
               case -3:
                  return (d * 1e3).ToString("0.###") + "m";
               case -4:
               case -5:
               case -6:
                  return (d * 1e6).ToString("0.###") + "μ";
               case -7:
               case -8:
               case -9:
                  return (d * 1e9).ToString("0.###") + "n";
               case -10:
               case -11:
               case -12:
                  return (d * 1e12).ToString("0.###") + "p";
               case -13:
               case -14:
               case -15:
                  return (d * 1e15).ToString() + "f";
               case -16:
               case -17:
               case -18:
                  return (d * 1e15).ToString() + "a";
               case -19:
               case -20:
               case -21:
                  return (d * 1e15).ToString() + "z";
               default:
                  return (d * 1e15).ToString() + "y";
            }
         }
         else
         {
            return "0";
         }
      }

      protected int lerp(int y0, int y1, int x0, int x1, int x)
      {
         if (x0 == x1)
            return 0;
         return y0 + ((x - x0) * (y1 - y0)) / (x1 - x0);
      }

      protected double lerp(double y0, double y1, double x0, double x1, double x)
      {
         return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
      }

      private void DrawHorizontalLines(Graphics g, Rectangle r)
      {
         Pen p = new Pen(Color.Gray);
         p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
         p.DashPattern = new float[] { 1, 5 };

         float yy = (float)(r.Height *DivY)/ MaxY;

         for (int y = 0; y < 8; y++)
         {
            g.DrawLine(p, 0, y*yy, r.Width, y*yy);
         }
      }

      public void DrawVerticalLines(Graphics g, Rectangle r)
      {
         float dist = (float)lerp(r.X, r.X + r.Width, 0, MaxXD - MinXD, DivX);

         for (int i = (int)(MinXD / DivX); i <= (int)(MaxXD / DivX); i++)
         {
            float x = (float)lerp(r.X, r.X + r.Width, MinXD, MaxXD , DivX *i);

            g.DrawLine(Pens.Gray, x, r.Y, x, r.Y + r.Height);

            if (dist > 40)
            {
               string str = ToEngineeringNotation(DivX * i);
               g.DrawString(str, m_cntrl.Font, Brushes.White, x, 0);
            }
         }
        
      }

      public void Draw( Graphics g, Rectangle r)
      {
         DrawHorizontalLines( g, r);
         DrawVerticalLines( g, r);
      }


   }
}
