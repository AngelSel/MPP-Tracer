using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TracerLibrary
{
    class ThreadStructure
    {

        public int TreadID { get; set; }
        public int TreadExecutionTime { get; set; }

        private Stack<MethodInfo> stackOfMethods;

        public ThreadStructure(int threadID)
        {
            TreadID = threadID;
            TreadExecutionTime = 0;
            stackOfMethods = new Stack<MethodInfo>();
        }

       



    }
}
