using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace TracerLibrary
{
    [Serializable]
    [DataContract]
    public class ThreadStructure
    {
        private long threadExecutionTime;
        private Stack<MethodStructure> stackOfMethods;

        [DataMember(Name = "id",Order =0)]
        public int TreadID { get; set; }

        [DataMember(Name = "time", Order = 1)]
        public long Time
        {
            get { return threadExecutionTime; }
            private set { }
        }


        [DataMember(Name = "methods",Order =2)]
        public List<MethodStructure> Methods { get; }
        

        public ThreadStructure(int threadID)
        {
            TreadID = threadID;
            threadExecutionTime = 0;
            stackOfMethods = new Stack<MethodStructure>();
            Methods = new List<MethodStructure>();
        }

        public void StartThreadTrace(MethodStructure someMethod)
        {

            if (stackOfMethods.Count == 0)
                Methods.Add(someMethod);
            else
                stackOfMethods.Peek().AddInnerMethod(someMethod);

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
