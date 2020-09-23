using System.Collections.Generic;
using System.Threading;
using ConsoleApp1.Interfaces;
using TracerLibrary;

namespace ConsoleApp1
{
    class Program
    {
        private static Tracer tracerTest = new Tracer();
        public static int ThreadsCount = 3;
        public static int sleepMiliseconds = 100;
        static void Main(string[] args)
        {
            
             List<Thread> testThreads = new List<Thread>();
             for (int i = 0; i < ThreadsCount; i++)
             {
                Thread thread = new Thread(MultipleThreads);
                testThreads.Add(thread);
                thread.Start();

             }
             foreach (var thread in testThreads)
                thread.Join();

            TraceResult traceResult = tracerTest.GetTraceResult();

            ITraceWriter writer = new ConsoleOutput();
            writer.Write(traceResult, new JSONSerializer());
            writer.Write(traceResult, new XMLSerializer());

            ITraceWriter fileOutputJSON = new FileOutput("jsonRez.json");
            fileOutputJSON.Write(traceResult, new JSONSerializer());

            ITraceWriter fileOutputXML = new FileOutput("XMLrez.xml");
            fileOutputXML.Write(traceResult, new XMLSerializer());



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

}