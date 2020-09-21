using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace TracerLibrary
{
    public class MethodStructure
    {

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public long ExecutionTime { get; set; }

        private Stopwatch stopwatch;

        public MethodStructure(MethodBase someMethod)
        {
            ClassName = someMethod.DeclaringType.Name;
            MethodName = someMethod.Name;
            ExecutionTime = 0;
            stopwatch = new Stopwatch();

        }

        public void StartMethodTrace()
        {
            stopwatch.Start();
        }

        public void StopMethodTrace()
        {
            stopwatch.Stop();
            ExecutionTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();

        }
    }
}
