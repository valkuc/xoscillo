using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class LowPass : Filter
   {
      double last;
      double alfa;

      public LowPass()
      {
         names = new String[] { "low pass cof" };
      }

      override public bool SetValue(int i, double val)
      {
         if (i == 0)
         {
            //set cut off freq
            double RC = 1.0 / (val * 2.0 * Math.PI);
            double dt = 1.0 / sampleRate;

            alfa = dt / (RC + dt);
            return true;
         }

         return false;
      }

      override public double DoFilter(double data)
      {
         double y = ((alfa * data) + (1.0 - alfa) * last);
         last = y;

         return y;
      }
   }
}
