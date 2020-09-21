using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TracerLibrary
{
    class MethodStructure
    {

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ExecutionTime { get; set; }

        public MethodStructure(MethodBase someMethod)
        {
            ClassName = someMethod.DeclaringType.Name;
            MethodName = someMethod.Name;
            ExecutionTime = 0;

        }
    }
}
