using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class LowPass : Filter
   {
      double last = 0;
      double alfa = 0;

      double cutOffFreq;

      public LowPass()
      {
         names = new String[] { "low pass cof" };
      }

      override public bool SetValue(int i, double val)
      {
         if (i == 0)
         {
            cutOffFreq = val;
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

      override public void SetSampleRate( double sr )
      {
         base.SetSampleRate(sr);

         //set cut off freq
         double RC = 1.0 / (cutOffFreq * 2.0 * Math.PI);
         double dt = 1.0 / sampleRate;

         alfa = dt / (RC + dt);
      }
   }
}
