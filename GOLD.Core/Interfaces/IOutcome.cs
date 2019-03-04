using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{
    public interface IOutcome
    {
        //Type Type { get; }
        string TypeName { get; set; }
        int SourceExecutionID { get; set; }
        int TargetExecutionID { get; set; } // Parent component.
        Dictionary<string, object> Data { get; set; } // Parent component.

    }
}
