using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace TracerLibrary
{
    [Serializable]
    [DataContract]
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadStructure> listOfThreads;
        [DataMember(Name = "threads")]
        public List<ThreadStructure> threads
        {
            get
            {
                SortedDictionary<int, ThreadStructure> sorteddictionary = new SortedDictionary<int, ThreadStructure>(listOfThreads);
                return new List<ThreadStructure>(sorteddictionary.Values);
            }
            private set { }
        }
      
        public TraceResult()
        {
            listOfThreads = new ConcurrentDictionary<int, ThreadStructure>();
        }

        public void StartTrace(int treadID, MethodBase someMethod)
        {
            ThreadStructure treadInfo = listOfThreads.GetOrAdd(treadID, new ThreadStructure(treadID));
            treadInfo.StartThreadTrace(new MethodStructure(someMethod));
        }

        public void StopTrace(int threadID)
        {
            ThreadStructure currentTreadInfo;
            if (!listOfThreads.TryGetValue(threadID, out currentTreadInfo))
            {
                throw new ArgumentException("Invalid thread ID");
            }
            currentTreadInfo.StopThreadTrace();
        }
    }
}
