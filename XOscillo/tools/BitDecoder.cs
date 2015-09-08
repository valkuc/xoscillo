namespace XOscillo.Tools
{
    public enum BIT_RESULT
    {
        BIT_ZERO = 0,
        BIT_ONE = 1,
        BIT_MORE = 2,
        BIT_ERROR = 3
    }

    class BitDecoder
    {
        byte lastDelta = 0;

        byte clock = 127;
        sbyte MaxErr = 30;

        private byte Error(int reference, int b)
        {
            byte err = (byte)((b * 100) / reference);
            if (err > 100)
                return (byte)(err - 100);
            else
                return (byte)(100 - err);
        }

        public void SetClock(byte clk)
        {
            clock = clk;
        }

        public bool IsClock(byte delta)
        {            
            if (Error(clock, delta) < MaxErr)
            {
                clock = (byte)((clock + delta) / 2);

                return true;
            }
            return false;
        }

        private BIT_RESULT IsOne(byte delta)
        {
            if (delta > clock)
            {
                return BIT_RESULT.BIT_ERROR;
            }

            if ( lastDelta == 0 )
            {
                lastDelta = delta;
                return BIT_RESULT.BIT_MORE;
            }
            else if ( IsClock( (byte)(lastDelta + delta)) )
            {
                lastDelta = 0;
                return BIT_RESULT.BIT_ONE;
            }

            return BIT_RESULT.BIT_ERROR;
        }

        public BIT_RESULT DecodeBit(byte delta)
        {
            if (lastDelta != 0)
            {
                return IsOne(delta);    
            }

            if ( IsClock(delta) )
            {
                return BIT_RESULT.BIT_ZERO;
            }

            return IsOne(delta);                
        }
    }
}
