using System.Collections.Generic;
using System.Threading;
using TracerLibrary;
using TracerLibrary.Interfaces;

namespace ConsoleApp1
{
    class Program
    {
        private static Tracer tracerTest = new Tracer();
        private readonly List<Thread> threads = new List<Thread>();
        public static int ThreadsCount = 3;
        public static int sleepMiliseconds = 100;
        static void Main(string[] args)
        {
           
           C test = new C(tracerTest);
           test.M0();
           TraceResult traceResult = tracerTest.GetTraceResult();
            /*
                       List<Thread> testThreads = new List<Thread>();
                       for (int i = 0; i < ThreadsCount; i++)
                       {
                           Thread thread = new Thread(MultipleThreads);
                           testThreads.Add(thread);
                           thread.Start();

                       }
                       foreach (var thread in testThreads)
                           thread.Join();

                       TraceResult traceResult = tracerTest.GetTraceResult();*/


            ITraceWriter writer = new ConsoleOutput();
            writer.Write(traceResult, new JSONSerializer());
            writer.Write(traceResult, new XMLSerializer());

        }
       public static  void CallMethod()
        {
            tracerTest.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            tracerTest.StopTrace();
        }

        public static void MultipleThreads()
        {
            tracerTest.StartTrace();
            List<Thread> threadsInMethod = new List<Thread>();
            for (int i = 0; i < ThreadsCount; i++)
            {
                Thread thread = new Thread(CallMethod);
                threadsInMethod.Add(thread);
                thread.Start();
            }
            foreach (var thread in threadsInMethod)
            {
                thread.Join();
            }
            CallMethod();
            Thread.Sleep(sleepMiliseconds);
            tracerTest.StopTrace();

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