using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace XOscillo
{
   [Serializable]
   public class SerializationHelper
   {
      public DataBlock dataBlock;
      public Graph graph;

      public bool SaveXML(FileStream stream)
      {
         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(SerializationHelper));
         serializer.Serialize(stream, this);

         return true;
      }

      static public SerializationHelper LoadXML(FileStream stream)
      {
         // Convert the object to XML data and put it in the stream.
         XmlSerializer serializer = new XmlSerializer(typeof(SerializationHelper));

         return (SerializationHelper)serializer.Deserialize(stream);         
      }

   }
}

