using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class Filter
   {
      protected double sampleRate;

      protected String[] names;

      public void SeSampleRate( double sr )
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

      virtual public String GetValueName(int i)
      {
         if (names != null)
         {
            if (i < names.Length)
            {
               return names[i];
            }
         }
         return null;
      }

   }

}
