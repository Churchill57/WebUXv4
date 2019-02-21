using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Outcomes
{
    public abstract class Outcome
    {
        public int ExecutionThreadID { get; set; }
        public int SourceComponentID { get; set; } // Within the execution thread.
        public string OutcomeName { get; set; }
        public string OutcomeData { get; set; }
    }
}
