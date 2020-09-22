using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TracerLibrary
{
    [Serializable]
    [DataContract]
    public class MethodStructure
    {
        private Stopwatch stopwatch;


        [DataMember(Name = "name", Order = 0)]
        public string MethodName { get; set; }

        [DataMember(Name = "class", Order = 1)]
        public string ClassName { get; set; }

        [DataMember(Name = "time", Order = 2)]
        public long ExecutionTime { get; set; }

        [DataMember(Name = "methods", Order = 3)]
        public List<MethodStructure> InnerMethods { get; set; }


        public MethodStructure(MethodBase someMethod)
        {
            InnerMethods = new List<MethodStructure>();
            stopwatch = new Stopwatch();
            MethodName = someMethod.Name;
            ClassName = someMethod.DeclaringType.Name;
            ExecutionTime = 0;
       

        }
        public void AddInnerMethod(MethodStructure method)
        {
            InnerMethods.Add(method);
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
