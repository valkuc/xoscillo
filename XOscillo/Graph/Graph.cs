using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   public class Graph
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

      public Control parent;

      float m_centre;
      float m_window;
      float m_s = 100000000.0f;

      // selection
      bool m_selecting = false;
      protected float m_selectT0 = 0;
      protected float m_selectT1 = 0;

      protected Rectangle m_Bounds;

      public Graph()
      {
      }

      public void SetRectangle(Rectangle Bounds)
      {
         m_Bounds = Bounds;
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

      protected float ValueXToRect( float x )
      {
         return (float)lerp(m_Bounds.X, m_Bounds.X + m_Bounds.Width, MinXD, MaxXD, x);
      }

      protected float ValueYToRect( float y)
      {
         return (float)lerp(m_Bounds.Y, m_Bounds.Y + m_Bounds.Height, MinY, MaxY, y);
      }

      protected float RectToValueX( int x )
      {
         return (float)lerp(MinXD, MaxXD, m_Bounds.X, m_Bounds.X + m_Bounds.Width, x);
      }

      protected float RectToValueY( int y)
      {
         return (float)lerp(MinY, MaxY, m_Bounds.Y, m_Bounds.Y + m_Bounds.Height, y);
      }


      virtual public void ResizeToRectangle(HScrollBar hBar)
      {
         m_window = (float)(m_Bounds.Width * 8.0f * DivX) / (float)m_Bounds.Height;

         m_centre = ((float)hBar.Value + (float)hBar.LargeChange / 2.0f) / m_s;

         float t0 = m_centre - m_window / 2.0f;
         float t1 = m_centre + m_window / 2.0f;

         if (t0 < 0)
         {
            t1 += -t0;
            t0 = 0;

            m_centre = t1 / 2.0f;
            m_window = t1;
         }

         if (t0 > MaxX)
         {

            t0 = MaxX - m_window;
            t1 = MaxX;

            m_centre = (t1 + t0) / 2.0f;
         }


         SetDisplayWindow(t0, t1);


         hBar.Maximum = (int)(MaxX * m_s);
         hBar.LargeChange = (int)(m_window * m_s);
         int i = hBar.Maximum - hBar.LargeChange;
         hBar.Value = (int)(t0 * m_s);
      }

      protected void DrawHorizontalLines(Graphics g)
      {
         Pen p = new Pen(Color.Gray);
         p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
         p.DashPattern = new float[] { 1, 5 };

         int min = (int)(MinY / DivY);
         int max = (int)(MaxY / DivY);

         if ( min > max )
         {
            int tmp = min;
            min = max;
            max = tmp;
         }

         for (int i = min; i <= max; i++)
         {
            float y = ValueYToRect( DivY * i);

            g.DrawLine(p, 0, y, m_Bounds.Width, y);
         }
      }

      public void DrawVerticalLines(Graphics g)
      {
         float dist = ValueXToRect( MinXD + DivX);

         for (int i = (int)(MinXD / DivX); i <= (int)(MaxXD / DivX); i++)
         {
            float x = ValueXToRect( DivX * i);

            g.DrawLine(Pens.Gray, x, m_Bounds.Y, x, m_Bounds.Y + m_Bounds.Height);

            if (dist > 40)
            {
               string str = ToEngineeringNotation(DivX * i);
               g.DrawString(str, parent.Font, Brushes.White, x, 0);
            }
         }        
      }
      
      public void DrawCross(Graphics g, Pen p, int x, int y)
      {
         Pen pe = Pens.DarkGray;
         g.DrawLine(pe, m_Bounds.X, y, m_Bounds.X + m_Bounds.Width, y);
         g.DrawLine(pe, x, m_Bounds.Y, x, m_Bounds.Y + m_Bounds.Height);
      }

      public void DrawValues(Graphics g, int x, int y)
      {
         Point pp = new Point();
         pp.X = x + 16;
         pp.Y = y + 16;
         g.DrawString(string.Format("({0}, {1})", ToEngineeringNotation(RectToValueX(x)), RectToValueY(y) ), parent.Font, Brushes.White, pp);
      }

      public void DrawSelection(Graphics g)
      {
         float s0, s1;

         if (m_selectT0 < m_selectT1)
         {
            s0 = m_selectT0;
            s1 = m_selectT1;
         }
         else
         {
            s1 = m_selectT0;
            s0 = m_selectT1;
         }

         g.FillRectangle(Brushes.DarkBlue, ValueXToRect(s0), m_Bounds.Y, ValueXToRect(s1) - ValueXToRect(s0), m_Bounds.Height);
      }

      virtual public void Draw(Graphics g, DataBlock db)
      {
      }

      public bool Selected()
      {
         return m_selectT0 != m_selectT1;
      }

      public virtual void GraphControl_MouseMove(object sender, MouseEventArgs e)
      {
         if ( m_selecting )
         {
            if (e.Button == MouseButtons.Left)
            {
               m_selectT1 = RectToValueX(e.X);
            }
            else
            {
               m_selecting = false;
            }
         }
         else
         {
            if (e.Button == MouseButtons.Left)
            {
               m_selectT0 = RectToValueX(e.X);
               m_selectT1 = m_selectT0;
               m_selecting = true;
            }
         }

      }


   }
}
