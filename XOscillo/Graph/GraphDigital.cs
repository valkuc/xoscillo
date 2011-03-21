using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   class GraphDigital : Graph
   {

      public GraphDigital(Control cntrl, HScrollBar h)
         : base(cntrl, h)
      {
      }

      public void DrawGraph(Graphics g, Rectangle r, DataBlock db)
      {
         g.Clear(Color.Black);

         Draw(g, r);

         float xx = 0;
         int i = 0;
         int i0 = (int)(db.GetChannelLength() * MinXD / db.GetTotalTime());
         int i1 = (int)(db.GetChannelLength() * MaxXD / db.GetTotalTime());
         try
         {
            float waveheight = r.Height / 16;

            int last_rv = 0;

            for (i = i0; i <= i1; i++)
            {
               int rawvolt = db.GetVoltage(0, i);

               float time = db.GetTime(i);

               float x = (float)lerp(r.X, r.X + r.Width, MinXD, MaxXD, time);

               for (int ch = 0; ch < db.m_channels; ch++)
               {
                  float y = r.Y + r.Height * (ch + 1) / 8;
                  
                  if ( ((last_rv >>ch)&1) != ((rawvolt>>ch)&1))
                  {
                     g.DrawLine(Pens.Red, xx, y, xx, y - waveheight);
                  }

                  if ( ((rawvolt >> ch)&1) >0)
                  {
                     y -= waveheight;
                  }

                  g.DrawLine(Pens.Red, xx, y, x, y);
               }

               xx = x;
               last_rv = rawvolt;
            }
         }
         catch
         {
            Console.WriteLine("{0} {1} {2}", db, r, i);
         }

      }
   }
}
