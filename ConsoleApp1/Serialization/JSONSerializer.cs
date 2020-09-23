using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using ConsoleApp1.Interfaces;
using TracerLibrary;

namespace ConsoleApp1
{
    public class JSONSerializer: ISerializer
    {
        private DataContractJsonSerializer jsonSerializer;
        public JSONSerializer()
        {
            jsonSerializer = new DataContractJsonSerializer(typeof(TraceResult));
        }

        public void Serialize(TraceResult traceResult, Stream stream)
        {
            using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: true, indent: true, indentChars: "     "))
            {
                jsonSerializer.WriteObject(jsonWriter, traceResult);
            }

        }
    }
}
