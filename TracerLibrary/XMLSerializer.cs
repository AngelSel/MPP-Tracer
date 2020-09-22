using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using TracerLibrary.Interfaces;

namespace TracerLibrary
{
    public class XMLSerializer:ISerializer
    {
        private DataContractSerializer xmlSerializer;
        private XmlWriterSettings xmlWriterSettings;

        public XMLSerializer()
        {
            xmlSerializer = new DataContractSerializer(typeof(TraceResult));
            xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            };
        }

        public void Serialize(TraceResult traceresult, Stream stream)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
            {
                xmlSerializer.WriteObject(xmlWriter, traceresult);
            }
        }
    }
}
