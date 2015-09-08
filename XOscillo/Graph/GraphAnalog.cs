using System;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo.Graph
{
   public class GraphAnalog : Graph
   {
      Pen[] m_pens = { Pens.Red, Pens.Blue, Pens.Green, Pens.Yellow };

      MouseEventArgs m_mouse = null;

      public bool showValueTicks = false;

      public GraphAnalog()
         : base()
      {
      }

      public GraphAnalog(Graph g)
         : base(g)
      {
      }      

      private void DrawGraph(Graphics g, Pen p, DataBlock db, int channel)
      {
         float yy = 0;
         float xx = 0;
         int i = 0;
         int i0 = (int)Math.Floor( lerp(0, db.GetChannelLength(), 0, db.GetTotalTime(), MinXD) );
         int i1 = (int)Math.Ceiling( lerp(0, db.GetChannelLength(), 0, db.GetTotalTime(), MaxXD) )+1;
         if (i1 > db.GetChannelLength())
         {
            i1 = db.GetChannelLength();
         }

         if (db.m_Annotations != null)
         {
             for (int an = 0; an < db.m_Annotations.Length; an++)
             {
                 float time = db.GetTime(db.m_Annotations[an]);
                 float x = ValueXToRect(time);
                 g.DrawLine(Pens.Green, x, 0, x, ValueYToRect(5));
             }
         }

         try
         {
            for (i = i0; i < i1; i++)
            {
               int rawvolt = db.GetVoltage(channel, i);

               float time = db.GetTime(i);

               float x = ValueXToRect(time);
               float y = ValueYToRect(rawvolt);

               if (i > 0)
               {
                  g.DrawLine(p, xx, yy, x, y);

                  if (showValueTicks)
                  {
                     g.DrawLine(p, x, y-2, x, y+2);
                     g.DrawLine(p, x - 2, y, x + 2, y);
                  }

               }
                 
               yy = y;
               xx = x;
            }
         }
         catch
         {
#if DEBUG
            Console.WriteLine("{0} {1} {2}", db, m_Bounds, i);
#endif
         }
                
         //Cursor.Hide();
         if (m_mouse != null)
         {
            DrawCross(g, Pens.Blue, m_mouse.X, m_mouse.Y);
         }

         {
            float t = (db.m_triggerPos * db.GetTotalTime()) / (float)db.GetChannelLength();
            float x = ValueXToRect(t);
            g.DrawLine(Pens.Green, x, m_Bounds.Y, x, m_Bounds.Y + m_Bounds.Height);
         }

         Point pp = new Point();
         pp.X = 0;
         pp.Y = 32;

        if (m_mouse != null)
        {
            float time = RectToValueX(m_mouse.X);
            float voltage = RectToValueY(m_mouse.Y);

            float voltageI = (voltage > 255 ? 255 : (voltage < 0 ? 0 : (int)voltage));
            float voltageV = voltageI * 5 / 255;

            string info = string.Format("{0} ({1}, {2}/255, {3:0.###}v)", db.m_sample, ToEngineeringNotation(time), (int)voltageI, voltageV);
            g.DrawString(info, parent.Font, Brushes.White, pp);
            pp.Y += 16;

            info = string.Format("({0}s/div, {1}Ks/s)", ToEngineeringNotation(DivX), db.m_sampleRate / 1000);
            g.DrawString(info, parent.Font, Brushes.White, pp);
            pp.Y += 16;
        }


         if (Selected())
         {
            if ((m_selectT0 < db.GetTotalTime()) && (m_selectT1 < db.GetTotalTime()))
            {
               g.DrawString(string.Format("({0}, {1}) - ({2}, {3})", ToEngineeringNotation(m_selectT0), db.GetVoltage(0, m_selectT0), ToEngineeringNotation(m_selectT1), db.GetVoltage(0, m_selectT1)), parent.Font, Brushes.White, pp);
               pp.Y += 16;

               g.DrawString(string.Format("ΔVoltage = {0}", db.GetVoltage(0, m_selectT1) - db.GetVoltage(0, m_selectT0)), parent.Font, Brushes.White, pp);
               pp.Y += 16;
            }
            
            string time = string.Format("ΔTime = {0}", ToEngineeringNotation(m_selectT1 - m_selectT0));
            if (m_selectT1 - m_selectT0 > 0)
            {
               time += string.Format(", {0} Hz", (int)(1.0f / (m_selectT1 - m_selectT0)));
            }
            g.DrawString(time, parent.Font, Brushes.White, pp);
            pp.Y += 16;
         }

      }

      override public void Draw(Graphics g, DataBlock db)
      {          
         DrawSelection(g);
         DrawHorizontalLines(g);
         DrawVerticalLines(g);

         uint bitField = db.m_channelsBitField;
         int index = 0;
         for (int ch = 0; ch < db.m_channels; )
         {
             if (( (bitField>>index) & 1)==1)
             {
                 DrawGraph(g, m_pens[index], db, ch);
                 ch++;
             }
             index++;
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
