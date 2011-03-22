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

      public GraphAnalog(Control cntrl, HScrollBar h)
         : base(cntrl, h)
      {         
      }

      private void DrawGraph(Graphics g, Rectangle r, Pen p, DataBlock db, int channel)
      {
         float yy = 0;
         float xx = 0;
         int i = 0;
         int i0 = (int)(db.GetChannelLength() * MinXD / db.GetTotalTime());
         int i1 = (int)(db.GetChannelLength() * MaxXD / db.GetTotalTime());
         try
         {
            for (i = i0; i <= i1; i++)
            {
               int rawvolt = db.GetVoltage(channel, i);

               float time = db.GetTime(i);

               float x = (float)lerp(r.X, r.X + r.Width, MinXD, MaxXD, time);
               float y = (float)lerp(r.Y, r.Y + r.Height, MaxY, MinY, rawvolt);

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
            Console.WriteLine("{0} {1} {2}", db, r, i);
         }
      }

      public void DrawGraph(Graphics g, Rectangle r, DataBlock db)
      {
         g.Clear(Color.Black);

         Draw( g, r);

         for (int ch = 0; ch < db.m_channels; ch++)
         {
            DrawGraph(g, r, m_pens[ch], db, ch);
         }
      }


   }
}
