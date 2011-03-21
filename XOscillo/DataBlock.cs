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
      public int m_channelOffset;
      public int m_sampleRate;
      public int m_trigger;
      public byte[] m_Buffer = null;
      public RESULT m_result;

      public DataBlock()
      {
      }

      public DataBlock(DataBlock db)
      {
         this.m_Buffer = new byte[db.m_Buffer.Length];
         System.Array.Copy(db.m_Buffer, 0, this.m_Buffer, 0, db.m_Buffer.Length);
         this.m_channels = db.m_channels;
         this.m_sample = db.m_sample;
         this.m_start = db.m_start;
         this.m_stop = db.m_stop;
         this.m_stride = db.m_stride;
         this.m_channelOffset = db.m_channelOffset;
         this.m_sampleRate = db.m_sampleRate;
         this.m_trigger = db.m_trigger;
         this.m_result = db.m_result;
         this.m_dataType = db.m_dataType;
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
         this.m_stride = db.m_stride;
         this.m_channelOffset = db.m_channelOffset;
         this.m_sampleRate = db.m_sampleRate;
         this.m_trigger = db.m_trigger;
         this.m_result = db.m_result;
         this.m_dataType = db.m_dataType;
      }
      public void Alloc(int size)
      {
         m_Buffer = new byte[size];
      }

      public void Save(string file)
      {
         // Open the file, creating it if necessary.
         FileStream stream = File.Open(file, FileMode.Create);

         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(DataBlock));
         serializer.Serialize(stream, this);

         // Close the file.
         stream.Close();
      }

      public void LoadXML(string file)
      {
         // Open the file, creating it if necessary.
         FileStream stream = File.Open(file, FileMode.Open);

         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(DataBlock));

         DataBlock db = (DataBlock)serializer.Deserialize(stream);
         Copy(db);


         // Close the file.
         stream.Close();
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
         m_trigger = 0;
         //this.m_start = Time.now;
      }

      public void GenerateSin(double freq)
      {
         DataBlock db = new DataBlock();
         m_trigger = 1500 * 0;
         m_channels = 1;
         m_sampleRate = 1024;

         m_Buffer = new byte[3000];

         for (int i = 0; i < m_Buffer.Length; i++)
         {
            m_Buffer[i] = (byte)(128 + 64.0 * Math.Sin((double)2 * 3.1415 * (i - m_trigger) * freq / (double)m_sampleRate));
         }
      }

      public void LowPass( double cutOffFreq )
      {
         double RC = 1.0 / (cutOffFreq * 2.0 * Math.PI);
         double dt = 1.0 / (float)m_sampleRate;

         double alfa = dt / (RC + dt);
         double last = 0;
         for (int i = 1; i < m_Buffer.Length; i++)
         {
            double  y = ((alfa * (double)m_Buffer[i]) + (1.0 - alfa) * last);
            last = y;
            m_Buffer[i] = (byte)y;
         }
      }

      public void HighPass(double cutOffFreq)
      {
         double RC = 1.0 / (cutOffFreq * 2.0 * Math.PI);
         double dt = 1.0 / (float)m_sampleRate;
         double alfa = RC / (RC + dt);

         double lastY = m_Buffer[0];
         double lastX = m_Buffer[0];
         for (int i = 1; i < m_Buffer.Length; i++)
         {
            double x = m_Buffer[i];
            double y = (alfa * (lastY + x - lastX));
            lastY = y;
            lastX = x;

            m_Buffer[i] = (byte)(y+127);
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

      public virtual byte GetVoltage(int channel, int index)
      {
         return m_Buffer[index * m_stride + m_channelOffset * channel];
      }

      public float GetTime(int index)
      {
         return (float)index / (float)m_sampleRate;
      }

      public float GetTotalTime()
      {
         return (float)GetChannelLength() / (float)m_sampleRate;
      }

      public int GetAverate(int channel)
      {
         int average = 0;
         for (int i = 0; i < GetChannelLength(); i++)
         {
            average += GetVoltage(channel, i);
         }
         average /= GetChannelLength();

         return average;
      }

   };

}
