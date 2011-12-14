using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class Filter
   {
      protected double sampleRate;

      protected String[] names;

      virtual public void SetSampleRate( double sr )
      {
         sampleRate = sr;
      }

      virtual public double DoFilter( double data )
      {
         return data;
      }

      virtual public bool SetValue(int i, double f)
      {
         return false;
      }

      virtual public string GetValueName(int i)
      {
         if (i >= names.Length)
         {
            return null;
         }

         return names[i];
      }

   }

}
