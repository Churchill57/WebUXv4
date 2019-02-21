using GOLD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{// Provides in-memory component services and caals WebApi to persist execution threads
    public class ExecutionManager : IExecutionManager // Equivalent to TaskManager in WebUXv2.
    {
        public string Test1()
        {
            return "Test1";
        }
    }
}
