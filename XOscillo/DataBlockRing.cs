using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
   class DataBlockRing : Ring<DataBlock>
   {
      int lastSample = -1;

      public DataBlockRing(int size)
         : base(size)
      {
         
      }

      public void GetFirstElementButDoNotRemoveIfLastOne(out DataBlock data)
      {
         lock (this)
         {
            DataBlock db;
            Peek(out db);


            if ( this.GetLength()>1)
            {
               getLock(out data);
               getUnlock();
            }
            else
            {
               //already drawn? Then wait for a new one
               if (lastSample == db.m_sample)
               {
                  Monitor.Wait(this);
               }

               data = db;
            }

            lastSample = data.m_sample;

         }
      }
 
   }


}
