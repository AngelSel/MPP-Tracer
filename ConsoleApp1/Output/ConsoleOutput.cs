using System;
using System.IO;
using ConsoleApp1.Interfaces;
using TracerLibrary;

namespace ConsoleApp1
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
