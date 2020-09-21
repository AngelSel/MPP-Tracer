using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer:ITracer
    {
        private TraceResult traceResult;

        public Tracer()
        {
            traceResult = new TraceResult();
        }
        public void StartTrace()
        {
            MethodBase currentMethod = new StackTrace().GetFrame(0).GetMethod();
            traceResult.StartTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }
        public void StopTrace()
        {
            traceResult.StopTrace(Thread.CurrentThread.ManagedThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
