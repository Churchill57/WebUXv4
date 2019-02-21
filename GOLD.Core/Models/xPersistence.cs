using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Models
{
    // DTOs

    public enum ExecutionStatus
    {
        Ready = 0,
        Started = 1,
        Completed = 2,
        Aborted = 3
    }
    public class ExecutionThread
    {
        public int ID { get; set; }

        public ExecutionStatus ExecutionStatus { get; set; }

        // Only one user at a time may be running a particular task execution thread.
        // The current user holds an implicit lock for a period of time (perhaps 30mins) until it 'times-out' or the user hands off the task to someone else.
        public string LockUserName { get; set; }

        public Guid LockUserID { get; set; }

        public DateTime LockDateTime { get; set; }

        // Important for when a task has to be resumed.
        // Can go directly to the task rather than through the root launcher and child components.
        //
        // Also important to guard against random access to tasks which perhaps should not be repeated or revisited.
        // Especially if multiple browser windows are spawned by holding down the Ctrl key!!
        public int ExecutionId { get; set; } 

    }

    public class xLuLauncher
    {
        public int TaskId { get; set; }
        public string ClientRef { get; set; }
        public string ComponentName { get; internal set; }

        public int? ComponentTaskId { get; internal set; }

        public string ReturnUrl { get; set; }

        public int? ReturnTaskId { get; set; }

        public int? ResumeTaskId { get; set; }

        public string ReturnTaskRef { get; set; }


    }
    public class xLogicalUnit
    {
        public int TaskId { get; set; }
        public string ClientRef { get; set; }

    }
    public class xUserExperience
    {
        public int TaskId { get; set; }
        public string ClientRef { get; set; }
    }
}
