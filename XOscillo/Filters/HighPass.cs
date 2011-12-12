using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class HiPass : Filter
   {
      double lastX;
      double lastY;

      double alfa;

      public HiPass()
      {
         names = new String[] { "hp cof" };
      }

      override public bool SetValue(int i, double val)
      {
         if (i == 0)
         {
            //cut off freq
            double RC = 1.0 / (val * 2.0 * Math.PI);
            double dt = 1.0 / sampleRate;

            alfa = RC / (RC + dt);
            return true;
         }

         return false;
      }

      override public double DoFilter(double data)
      {
         double x = data;
         double y = (alfa * (lastY + x - lastX));
         lastY = y;
         lastX = x;

         return y;
      }
   }
}
