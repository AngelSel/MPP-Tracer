using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TracerLibrary
{
    public class ThreadStructure
    {

        public int TreadID { get; set; }
        public int TreadExecutionTime { get; set; }
        public List<MethodStructure> Methods { get; }
        private long threadExecutionTime;
        public long Time
        {
            get { return threadExecutionTime; }
        }

        private Stack<MethodStructure> stackOfMethods;

        public ThreadStructure(int threadID)
        {
            TreadID = threadID;
            threadExecutionTime = 0;
            stackOfMethods = new Stack<MethodStructure>();
            Methods = new List<MethodStructure>();
        }

        public void StartThreadTrace(MethodStructure someMethod)
        {
            stackOfMethods.Push(someMethod);
            someMethod.StartMethodTrace();

        }

        public void StopThreadTrace()
        {
            MethodStructure lastMethod = stackOfMethods.Peek();
            lastMethod.StopMethodTrace();
            if (stackOfMethods.Count == 1)
                threadExecutionTime += lastMethod.ExecutionTime;
            stackOfMethods.Pop();

        }

      
    }
}
