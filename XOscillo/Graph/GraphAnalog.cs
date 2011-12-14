using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
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
            Console.WriteLine("{0} {1} {2}", db, m_Bounds, i);
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

         try
         {
            float time = RectToValueX(m_mouse.X);
            float voltage = RectToValueY(m_mouse.Y);

            string info = string.Format("({0}, {1})", ToEngineeringNotation(time), voltage);
            g.DrawString(info, parent.Font, Brushes.White, pp);
            pp.Y += 16;

            info = string.Format("({0}s/div, {1}Ks/s)", ToEngineeringNotation(DivX), db.m_sampleRate/1000);
            g.DrawString(info, parent.Font, Brushes.White, pp);
            pp.Y += 16;
         }
         catch
         {
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

         for (int ch = 0; ch < db.m_channels; ch++)
         {
            DrawGraph(g, m_pens[ch], db, ch);
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
