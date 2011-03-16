using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XOscillo
{
   class GraphLines2 : GraphLines
   {
      float m_centre;
      float m_window;

      float m_s = 100000000.0f;

      public GraphLines2(Control cntrl, HScrollBar h)
         : base(cntrl, h)
      {
      }

      public void DrawGraph(Graphics g, Rectangle r, DataBlock db)
      {
         m_window = (float)(r.Width * 8.0f * DivX) / (float)r.Height;

         m_centre = ((float)m_h.Value + (float)m_h.LargeChange / 2.0f) / m_s;

         float t0 = m_centre - m_window / 2.0f;
         float t1 = m_centre + m_window / 2.0f;

         if (t0 < 0)
         {
            t1 += -t0;
            t0 = 0;

            m_centre = t1 / 2.0f;

            m_window = t1;
         }

         SetDisplayWindow(t0, t1);
         
         m_h.Maximum = (int)(db.GetTotalTime() * m_s);
         m_h.LargeChange = (int)(m_window * m_s);         
         m_h.Value = (int)(t0 * m_s);
         
         base.DrawGraph(g, r, db);
      }
   }
}
