using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace XOscillo
{
   [Serializable]
   public class DataBlock
   {
      public enum RESULT
      {
         OK,
         ERROR,
         TIMEOUT,
      };

      public enum DATA_TYPE
      {
         UNDEFINED,
         ANALOG,
         DIGITAL,
      };


      public int m_sample;
      public DateTime m_start = new DateTime();
      public DateTime m_stop = new DateTime();
      public DATA_TYPE m_dataType;
      public int m_channels;
      public int m_stride;
      public float m_00;
      public float m_FF;
      public int m_channelOffset;
      public int m_sampleRate;
      public int m_triggerVoltage;
      public int m_triggerPos;
      public byte[] m_Buffer = null;
      public RESULT m_result;

      public int[] m_Annotations = null;

      public DataBlock()
      {
      }

      public DataBlock(DataBlock db)
      {
         if (db == null)
         {
            return;
         }

         this.m_Buffer = new byte[db.m_Buffer.Length];
         System.Array.Copy(db.m_Buffer, 0, this.m_Buffer, 0, db.m_Buffer.Length);
         this.m_channels = db.m_channels;
         this.m_sample = db.m_sample;
         this.m_start = db.m_start;
         this.m_stop = db.m_stop;
         this.m_00 = db.m_00;
         this.m_FF = db.m_FF;
         this.m_stride = db.m_stride;
         this.m_channelOffset = db.m_channelOffset;
         this.m_sampleRate = db.m_sampleRate;
         this.m_triggerVoltage = db.m_triggerVoltage;
         this.m_triggerPos = db.m_triggerPos;
         this.m_result = db.m_result;
         this.m_dataType = db.m_dataType;
         this.m_Annotations = db.m_Annotations;
      }

      public void Copy(DataBlock db)
      {
         if (m_Buffer == null || db.m_Buffer.Length != m_Buffer.Length)
         {
            m_Buffer = new byte[db.m_Buffer.Length];
         }

         System.Array.Copy(db.m_Buffer, 0, m_Buffer, 0, db.m_Buffer.Length);
         this.m_channels = db.m_channels;
         this.m_sample = db.m_sample;
         this.m_start = db.m_start;
         this.m_stop = db.m_stop;
         this.m_00 = db.m_00;
         this.m_FF = db.m_FF;
         this.m_stride = db.m_stride;
         this.m_channelOffset = db.m_channelOffset;
         this.m_sampleRate = db.m_sampleRate;
         this.m_triggerVoltage = db.m_triggerVoltage;
         this.m_triggerPos = db.m_triggerPos;
         this.m_result = db.m_result;
         this.m_dataType = db.m_dataType;
      }

      public void Alloc(int size)
      {
         m_Buffer = new byte[size];
      }

      public void SaveXML(FileStream stream)
      {
         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(DataBlock));
         serializer.Serialize(stream, this);
      }

      static public DataBlock LoadXML(FileStream stream)
      {
         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(DataBlock));

         return (DataBlock)serializer.Deserialize(stream);
      }

      public void LoadCSV(string file)
      {
         List<string> parsedData = new List<string>();

         // Open the file, creating it if necessary.
         FileStream stream = File.Open(file, FileMode.Open);
         StreamReader reader = new StreamReader(stream);

         string line;
         while ((line = reader.ReadLine()) != null)
         {
            string[] row = line.Split(',');
            foreach (string s in row)
            {
               if (s != "")
                  parsedData.Add(s);
            }
         }

         // Close the file.
         stream.Close();

         DataBlock db = new DataBlock();

         m_Buffer = new byte[parsedData.Count];
         for (int i = 0; i < parsedData.Count; i++)
         {
            m_Buffer[i] = byte.Parse(parsedData[i]);
         }
         m_channels = 1;
         m_sampleRate = (115600 / 8);
         m_triggerVoltage = 0;
         //this.m_start = Time.now;
      }

      public void GenerateSin(double freq)
      {
         DataBlock db = new DataBlock();
         m_triggerVoltage = 1500 * 0;
         m_channels = 1;
         m_sampleRate = 1024;

         m_Buffer = new byte[3000];

         for (int i = 0; i < m_Buffer.Length; i++)
         {
            m_Buffer[i] = (byte)(128 + 64.0 * Math.Sin((double)2 * 3.1415 * (i - m_triggerVoltage) * freq / (double)m_sampleRate));
         }
      }


      public void Load(string file)
      {
         GenerateSin(10);
         //LoadCSV(file);
      }

      public int GetChannelLength()
      {
         if (m_dataType == DATA_TYPE.ANALOG)
         {
            //every byte is a value
            return m_Buffer.Length / m_channels;
         }
         else if (m_dataType == DATA_TYPE.DIGITAL)
         {
            // every byte are 8 channels
            return m_Buffer.Length;
         }

         return m_Buffer.Length;
      }

      public virtual void SetVoltage(int channel, int index, byte voltage)
      {
         m_Buffer[index * m_stride + m_channelOffset * channel] = voltage;
      }
      
      public virtual byte GetVoltage(int channel, int index)
      {
          int i = index * m_stride + m_channelOffset * channel;
          if (i < m_Buffer.Length)
          {
              return m_Buffer[i];
          }

          return 0;
      }

      public virtual byte GetVoltage(int channel, float time)
      {
         int i = (int)(GetChannelLength() * time / GetTotalTime());

         return m_Buffer[i * m_stride + m_channelOffset * channel];
      }

      public float GetTime(int index)
      {
         return (float)index / (float)m_sampleRate;
      }

      public float GetTotalTime()
      {
         return (float)GetChannelLength() / (float)m_sampleRate;
      }

      public byte GetAverate(int channel)
      {
         int average = 0;
         for (int i = 0; i < GetChannelLength(); i++)
         {
            average += GetVoltage(channel, i);
         }
         average /= GetChannelLength();

         return (byte)average;
      }

   };

}
