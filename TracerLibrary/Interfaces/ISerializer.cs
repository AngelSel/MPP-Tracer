using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerLibrary.Interfaces
{
    public interface ISerializer
    {
        void Serialize(TraceResult trace, Stream stream);
    }
}
