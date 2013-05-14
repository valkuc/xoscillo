using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XOscillo
{
    class AnalogTeensy : SerialTeensy
    {

        public AnalogTeensy()
        {
        }

        override public bool GetDataBlock(ref DataBlock db)
        {
            bool result = true;
            lock (thisLock)
            {
                //assume it timed out
                db.m_min = 0;
                db.m_max = 5;
                db.m_channels = GetNumberOfEnabledChannels();
                db.m_channelsBitField = GetChannelBitField();
                db.m_triggerVoltage = 0;
                db.m_triggerPos = 0;
                db.m_sampleRate = GetSampleRate();
                db.m_samplesPerChannel = GetNumberOfSamplesPerChannel();
                db.m_dataType = DataBlock.DATA_TYPE.ANALOG;
                db.Alloc();
            }

            for (int i = 0; i < db.GetChannelLength(); i++)
            {
                /*
                byte header = GetStream().ReadByte();
                if (header < 128)
                {
                    byte b1 = GetStream().ReadByte();
                    byte b2 = GetStream().ReadByte();

                    for (int c = 0; c < db.m_channels; c++)
                    {
                        UInt16 v = (UInt16)(GetStream().ReadByte() * 256 + GetStream().ReadByte());
                        db.SetVoltage(c, i, v);
                    }
                    GetStream().ReadByte();
                }
                else
                {
                    GetResponse(header);

                    //if command received then abort 
                    for (; i < db.GetChannelLength(); i++)
                    {
                        for (int c = 0; c < db.m_channels; c++)
                        {
                            db.SetVoltage(c, i, 0);
                        }
                    }

                }
                 */

                byte[] header = new byte[1];
                byte[] data = new byte[20];
                Read(header, 1);
                if (header[0] < 128)
                {
                    
                    Read(data, 3 + 2 * db.m_channels);
                    Console.WriteLine("{0} {1} {2} {3}", header[0], data[0], data[1], data[2]);
                }
                else
                {
                    Read(data, 3);
                    Console.WriteLine("* {0} {1} {2} {3}",header[0],data[0],data[1],data[2]);
                }
            }

            db.m_result = DataBlock.RESULT.OK;

            return result;
        }


    }
}
