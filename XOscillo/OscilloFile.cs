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
        }

        public override bool Open(string file)
        {
            m_db.Load(file);
            return true;
        }

        public override bool Close()
        {            
            return true;
        }

        public override bool IsOpened()
        {
            return false;
        }

        public override string GetName()
        {
            return "file";
        }


        public bool Open(DataBlock db)
        {
            return true;
        }

        override public bool GetDataBlock(ref DataBlock db)
        {
            db.Copy(m_db);
            return true;
        }

        public void SetDataBlock(DataBlock db)
        {
            m_db.Copy(db);
        }


    }
}
