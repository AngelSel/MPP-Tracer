using System;
using System.Threading;
using TracerLibrary;
using TracerLibrary.Interfaces;

namespace ConsoleApp1
{
    class Program
    {
        private static Tracer tracerTest = new Tracer();
        static void Main(string[] args)
        {
            C test = new C(tracerTest);
            test.M0();
            TraceResult traceResult = tracerTest.GetTraceResult();
            ITraceWriter writer = new ConsoleOutput();
            writer.Write(traceResult, new JSONSerializer());

        }
    }
    public class C
    {
        private ITracer tracer;

        public C(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void M0()
        {
            M1();
            M2();
        }

        private void M1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        private void M2()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
    }
}