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
        public string LaunchCommandLineJson { get; set; }
        public string LaunchInputsJson { get; set; }
        public int ExecutionStatus { get; set; }
        public string LockUserName { get; set; }
        public string LockUserID { get; set; }
        public Nullable<System.DateTime> LockDateTime { get; set; }
        public string RootComponentTitle { get; set; }
        public string ExecutingComponentTitle { get; set; }
        public int ComponentExecutingID { get; set; }
        public string ExecutingComponentsJson { get; set; }
        public string PendingOutcomeJson { get; set; }
    }
}
