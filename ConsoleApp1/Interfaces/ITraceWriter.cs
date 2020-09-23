using TracerLibrary;

namespace ConsoleApp1.Interfaces
{
    public interface ITraceWriter
    {
        void Write(TraceResult traceResult, ISerializer serializer);

    }
}
