using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using TracerLibrary;

namespace TracerUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public Tracer tracer;
        private readonly List<Thread> threads = new List<Thread>();
        int ThreadsCount = 3;
        int methodCount = 5;
        int sleepMiliseconds = 100;

        private void CallMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            tracer.StopTrace();
        }

        private void CallMethodSecond()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds*3);
            tracer.StopTrace();
        }

        public void InnerMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            CallMethod();
            tracer.StopTrace();
        }

        public void NestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            InnerMethod();
            tracer.StopTrace();

        }

        //ѕроверка структуры ThreadStructure и работы класса TraceResult
        [TestMethod]
        public void ThreadCount()
        {
            tracer = new Tracer();

            for (int i = 0; i< ThreadsCount;i++ )
            {
                threads.Add(new Thread(CallMethod));
            }

            foreach(Thread threa in threads)
            {
                threa.Start();
                threa.Join();
            }

            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(ThreadsCount, traceResult.threads.Count);
        }

        //ѕроверка структуры MethodStructure и работы класса TraceResult
        [TestMethod]
        public void MethodCount()
        {
            tracer = new Tracer();
            for (int i =0; i < methodCount; i++)
            {
                CallMethod();
            }
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(methodCount, traceResult.threads[0].Methods.Count);
        }

        // ѕроверка работы программы при данных: один метод в одном потоке
        [TestMethod]
        public void SingleMethodSingleThread()
        {
            tracer = new Tracer();
            CallMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.IsTrue(traceResult.threads[0].Time >= sleepMiliseconds);
            Assert.AreEqual(1, traceResult.threads.Count);
            Assert.AreEqual(1, traceResult.threads[0].Methods.Count);
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, traceResult.threads[0].TreadID);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[0].ClassName);
            Assert.AreEqual(nameof(CallMethod), traceResult.threads[0].Methods[0].MethodName);
        }

        // ѕроверка работы программы при данных: два метода в одном потоке
        [TestMethod]
        public void TwoMethodsSingleThread()
        {

            tracer = new Tracer();
            CallMethod();
            CallMethodSecond();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(1, traceResult.threads.Count);
            Assert.AreEqual(2, traceResult.threads[0].Methods.Count);
            Assert.IsTrue(traceResult.threads[0].Time >= sleepMiliseconds*4);
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, traceResult.threads[0].TreadID);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[0].ClassName);
            Assert.AreEqual(nameof(CallMethod), traceResult.threads[0].Methods[0].MethodName);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[1].ClassName);
            Assert.AreEqual(nameof(CallMethodSecond), traceResult.threads[0].Methods[1].MethodName);
        }

        // ѕроверка работы программы при данных: несколько вложенных методов в одном потоке
        [TestMethod]
        public void NestedMethodsSingleThread()
        {
            tracer = new Tracer();
            NestedMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(1, traceResult.threads.Count);
            Assert.AreEqual(1, traceResult.threads[0].Methods.Count);
            Assert.AreEqual(1, traceResult.threads[0].Methods[0].InnerMethods.Count);
            Assert.AreEqual(1, traceResult.threads[0].Methods[0].InnerMethods[0].InnerMethods.Count);
            Assert.AreEqual(0, traceResult.threads[0].Methods[0].InnerMethods[0].InnerMethods[0].InnerMethods.Count);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[0].ClassName);
            Assert.AreEqual(nameof(NestedMethod), traceResult.threads[0].Methods[0].MethodName);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[0].InnerMethods[0].ClassName);
            Assert.AreEqual(nameof(InnerMethod), traceResult.threads[0].Methods[0].InnerMethods[0].MethodName);
            Assert.AreEqual(nameof(UnitTest1), traceResult.threads[0].Methods[0].InnerMethods[0].InnerMethods[0].ClassName);
            Assert.AreEqual(nameof(CallMethod), traceResult.threads[0].Methods[0].InnerMethods[0].InnerMethods[0].MethodName);

        }


    }
}
