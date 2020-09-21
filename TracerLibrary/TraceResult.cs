using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TracerLibrary
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadStructure> listOfThreads;
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
            if(listOfThreads.TryGetValue(threadID, out currentTreadInfo))
            {
                currentTreadInfo.StopThreadTrace();
            }
        }
    }
}
