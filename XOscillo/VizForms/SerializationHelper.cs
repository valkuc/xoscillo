using System;
using System.IO;
using System.Xml.Serialization;

namespace XOscillo.VizForms
{
   [Serializable]
   public class SerializationHelper
   {
      public DataBlock dataBlock;
      public XOscillo.Graph.Graph graph;

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

