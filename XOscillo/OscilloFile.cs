using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   class OscilloFile : Oscillo
   {
      DataBlock m_db = new DataBlock();

      public OscilloFile()
      {
         m_sampleRates = new int[15] { 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000 };
      }

      public bool OpenFile( string file)
      {
         m_db.Load(file);
         return true;
      }

      public bool Open(DataBlock db)
      {
         return true;
      }

      override public bool GetDataBlock( ref DataBlock db)
      {
         db.Copy( m_db );
         return true;
      }

      public void SetDataBlock(DataBlock db)
      {
         m_db.Copy(db);
      }


   }
}
