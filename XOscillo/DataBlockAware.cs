using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XOscillo
{
    interface DataBlockAware
    {
        DataBlock GetDataBlock();
        void SetDataBlock(DataBlock dataBlock);
    }
}
