using System.IO;
using TracerLibrary.Interfaces;

namespace TracerLibrary
{
    public class FileOutput: ITraceWriter
    {
        private string fileName;

        public FileOutput(string someFileName)
        {
            this.fileName = someFileName;
        }

        public void Write(TraceResult result, ISerializer serializer)
        {
            using (FileStream fStream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(result, fStream);
            }
        }
    }
}
