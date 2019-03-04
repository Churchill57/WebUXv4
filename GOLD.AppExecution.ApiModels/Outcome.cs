using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppExecution.ApiModels
{
    public class Outcome
    {
        public string TypeName { get; set; }
        public int SourceExecutionID { get; set; }
        public int TargetExecutionID { get; set; } // Parent component.
        public Dictionary<string, object> Data { get; set; } // Parent component.

    }
}
