using System;
using System.Drawing;

namespace XOscillo.Graph
{
   public class GraphDigital : Graph
   {

      public GraphDigital()
         : base()
      {
      }

      public GraphDigital(Graph g)
         : base( g)
      {
      }


      override public void Draw(Graphics g, DataBlock db)
      {
         DrawSelection(g);
         DrawHorizontalLines(g);
         DrawVerticalLines(g);

         float xx = 0;
         int i = 0;
         int i0 = (int)(db.GetChannelLength() * MinXD / db.GetTotalTime());
         int i1 = (int)(db.GetChannelLength() * MaxXD / db.GetTotalTime());
         try
         {
            float waveheight = m_Bounds.Height / 16;

            int last_rv = 0;

            for (i = i0; i <= i1; i++)
            {
               int rawvolt = db.GetVoltage(0, i);

               float time = db.GetTime(i);

               float x = ValueXToRect( time);

               for (int ch = 0; ch < db.m_channels; ch++)
               {
                   //skip unselected channels
                   if ((db.m_channelsBitField & (1 << ch)) == 0)
                       continue;

                  float y = ValueYToRect( (ch +1) * DivY);
                  
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
#if DEBUG
            Console.WriteLine("{0} {1} {2}", db, m_Bounds, i);
#endif
         }

      }
   }
}
