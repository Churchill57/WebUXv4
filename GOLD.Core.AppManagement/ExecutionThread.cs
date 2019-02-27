using GOLD.Core.Enums;
using GOLD.Core.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement
{
    public class ExecutionThread
    {
        public int ID { get; internal set; }
        public Dictionary<string,string> LaunchInputs { get; set; } // For root component.
        public LogicalUnitStatusEnum ExecutionStatus { get; set; }
        public string LockUserName { get; set; }
        public string LockUserID { get; set; }
        public Nullable<System.DateTime> LockDateTime { get; set; }
        public string RootComponentTitle { get; set; }
        public string ExecutingComponentTitle { get; set; }
        public int ComponentExecutingID { get; set; }
        public List<ExecutingComponent> ExecutingComponents { get; set; }
        public Outcome PendingOutcome { get; set; }
    }
}
