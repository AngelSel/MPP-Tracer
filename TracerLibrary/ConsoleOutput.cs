using System;
using System.IO;
using TracerLibrary.Interfaces;

namespace TracerLibrary
{
    public class ConsoleOutput: ITraceWriter
    {
        public void Write(TraceResult traceResult, ISerializer serializer)
        {
            using (Stream consoleStream = Console.OpenStandardOutput())
            {
                serializer.Serialize(traceResult, consoleStream);
            }
        }
    }
}
