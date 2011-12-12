using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   public class FilterConsumer : Consumer
   {
      bool enabled;
      Filter filter;

      DataBlock dataBlock = new DataBlock();

      public FilterConsumer(Consumer next)
         : base(next)
      {
         filter = null;
      }


      public void SetFilter(Filter f)
      {
         filter = f;
      }

      public Filter GetFilter()
      {
         return filter;
      }

      public void Enable(bool state)
      {
         enabled = state;
      }

      override public void SetDataBlock(DataBlock db)
      {
         dataBlock.Copy(db);
         if (enabled)
         {

            //filter a few times the first value to initialize filter
            for (int i = 0; i < 4; i++)
            {
               double volt = db.GetVoltage(0, 0);
               filter.DoFilter(volt - 127);
            }

            for (int i = 0; i < db.GetChannelLength(); i++)
            {
               double volt = db.GetVoltage(0, i);
               dataBlock.SetVoltage(0, i, (byte)(filter.DoFilter(volt - 127) + 127));
            }
         }
         base.SetDataBlock(dataBlock);
      }
   }
}
