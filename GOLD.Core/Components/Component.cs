using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Components
{
    public abstract class Component
    {
        public int RegisteredComponentID { get; set; }
        public int ExecutionThreadID { get; set; } // The App execution thread (from when the root component was launched until it is done or suspended).
        public int ComponentExecutionID { get; set; } // Equivalend to TaskId in WebUXv2.
        public string ComponentExecutionUrl { get; set; }
        public string ClientRef { get; set; }
        public Dictionary<string, object> State { get; set; }
    }
}
