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
         PROGRESSIVE,
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
      public uint m_channelsBitField;
      public float m_min;
      public float m_max;
      public int m_sampleRate;
      public int m_samplesPerChannel;
      public int m_triggerVoltage;
      public int m_triggerPos;      
      public int m_index;
      public RESULT m_result;
      public int[] m_Buffer = null;

      public int[] m_Annotations = null;

      public DataBlock()
      {
          m_index = 0;
      }

      public DataBlock(DataBlock db)
      {
         if (db == null)
         {
            return;
         }

         Copy(db);
      }

      public void Copy(DataBlock db)
      {
         this.m_channelsBitField = db.m_channelsBitField;
         this.m_channels = db.m_channels;
         this.m_sample = db.m_sample;
         this.m_start = db.m_start;
         this.m_stop = db.m_stop;
         this.m_min = db.m_min;
         this.m_max = db.m_max;
         this.m_sampleRate = db.m_sampleRate;
         this.m_samplesPerChannel = db.m_samplesPerChannel;
         this.m_triggerVoltage = db.m_triggerVoltage;
         this.m_triggerPos = db.m_triggerPos;
         this.m_result = db.m_result;
         this.m_dataType = db.m_dataType;
         this.m_Annotations = db.m_Annotations;
         this.m_index = db.m_index;

         Alloc();

         System.Array.Copy(db.m_Buffer, 0, m_Buffer, 0, db.m_Buffer.Length);
      }

      public void Alloc()
      {
          int size = m_samplesPerChannel * m_channels;
          if (m_Buffer == null || m_Buffer.Length != size)
          {
              m_Buffer = new int[size];
          }
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

         m_Buffer = new int[parsedData.Count];
         for (int i = 0; i < parsedData.Count; i++)
         {
            m_Buffer[i] = int.Parse(parsedData[i]);
         }
         m_channels = 1;
         m_sampleRate = (115600 / 8);
         m_triggerVoltage = 0;
         //this.m_start = Time.now;
      }
      /*
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
      */

      public void Load(string file)
     {
        //GenerateSin(10);
        LoadCSV(file);
     }
      

      public int GetChannelLength()
      {
         if (m_channels == 0)
              return 0;

         return m_samplesPerChannel;
      }

      public virtual void SetVoltage(int channel, int index, int voltage)
      {
         m_Buffer[index * m_channels + channel] = voltage;
      }
      
      public virtual int GetVoltage(int channel, int index)
      {
          int i = index * m_channels + channel;
          if (i>=0 && i < m_Buffer.Length)
          {
              return m_Buffer[i];
          }

          return (int)0;
      }

      public virtual int GetVoltage(int channel, float time)
      {
         if (time < 0)
              time = 0;

         int i = (int)(GetChannelLength() * time / GetTotalTime());

         return (int)m_Buffer[i * m_channels + channel];
      }

      public float GetTime(int index)
      {
          if (m_sampleRate == 0)
              return 0;
          return (float)index / (float)m_sampleRate;
      }

      public float GetTotalTime()
      {
         return GetTime(GetChannelLength());
      }

      public int GetAverage(int channel)
      {
         int average = 0;
         for (int i = 0; i < GetChannelLength(); i++)
         {
            average += (int)GetVoltage(channel, i);
         }
         average /= GetChannelLength();

         return average;
      }

   };

}
