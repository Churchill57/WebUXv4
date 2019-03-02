using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppExecution.ApiModels
{
    public class ExecutionThread
    {
        public int ID { get; set; }
        public string LaunchCommandLine { get; set; }
        public Dictionary<string, string> LaunchInputs { get; set; } // For root component.
        public int ExecutionStatus { get; set; }
        public string LockUserName { get; set; }
        public string LockUserID { get; set; }
        public Nullable<System.DateTime> LockDateTime { get; set; }
        public string RootComponentTitle { get; set; }
        public string ExecutingComponentTitle { get; set; }
        public int ComponentExecutingID { get; set; }
        public List<ExecutingComponent> ExecutingComponents { get; set; }
        //JH public Outcome PendingOutcome { get; set; }
    }
}
