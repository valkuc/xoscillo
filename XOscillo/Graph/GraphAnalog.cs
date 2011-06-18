using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   class GraphAnalog : Graph
   {
      Pen[] m_pens = { Pens.Red, Pens.Blue, Pens.Green, Pens.Yellow };

      MouseEventArgs m_mouse = null;

      public GraphAnalog(Control cntrl, HScrollBar h)
         : base(cntrl, h)
      {         
      }

      private void DrawGraph(Graphics g, Pen p, DataBlock db, int channel)
      {
         float yy = 0;
         float xx = 0;
         int i = 0;
         int i0 = (int)(db.GetChannelLength() * MinXD / db.GetTotalTime());

         int i1 = (int)(db.GetChannelLength() * MaxXD / db.GetTotalTime());
         if (i1 > db.GetChannelLength())
         {
            i1 = db.GetChannelLength();
         }

         try
         {
            for (i = i0; i <= i1; i++)
            {
               int rawvolt = db.GetVoltage(channel, i);

               float time = db.GetTime(i);

               float x = ValueXToRect(time);
               float y = ValueYToRect(rawvolt);

               if (i > 0)
               {
                  g.DrawLine(p, xx, yy, x, y);
               }
                 
               yy = y;
               xx = x;
            }
         }
         catch
         {
            Console.WriteLine("{0} {1} {2}", db, m_Bounds, i);
         }
         
         {
            int rawvolt = db.GetAverate(0);
            float y = ValueYToRect(rawvolt);
            g.DrawLine(p, 0, y, 1000, y);
         }
         

         //Cursor.Hide();
         if (m_mouse != null)
         {
            DrawCross(g, Pens.Blue, m_mouse.X, m_mouse.Y);
         }

         {
            float t = (512 * db.GetTotalTime()) / (float)db.GetChannelLength();
            float x = ValueXToRect(t);
            g.DrawLine(Pens.LightGreen, x, m_Bounds.Y, x, m_Bounds.Y + m_Bounds.Height);
         }

         if (Selected())
         {
            Point pp = new Point();
            pp.X = 0;
            pp.Y = 32;

            g.DrawString(string.Format("({0}, {1}) - ({2}, {3})", ToEngineeringNotation(m_selectT0), db.GetVoltage(0, m_selectT0), ToEngineeringNotation(m_selectT1), db.GetVoltage(0, m_selectT1)), m_cntrl.Font, Brushes.White, pp);
         }

      }

      public void DrawGraph(Graphics g, DataBlock db)
      {
         g.Clear(Color.Black);

         Draw( g);

         for (int ch = 0; ch < db.m_channels; ch++)
         {
            DrawGraph(g, m_pens[ch], db, ch);
         }
      }

      public override void GraphControl_MouseMove(object sender, MouseEventArgs e)
      {
         base.GraphControl_MouseMove(sender, e);
         m_mouse = e;
         m_cntrl.Invalidate();
      }

   }
}
