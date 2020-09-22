using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLibrary.Interfaces
{
    public interface ITraceWriter
    {
        void Write(TraceResult traceResult, ISerializer serializer);

    }
}
