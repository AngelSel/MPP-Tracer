using System.IO;
using TracerLibrary;

namespace ConsoleApp1.Interfaces
{
    public interface ISerializer
    {
        void Serialize(TraceResult trace, Stream stream);
    }
}
