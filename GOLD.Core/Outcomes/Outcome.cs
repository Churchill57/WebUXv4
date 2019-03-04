using GOLD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Outcomes
{
    public abstract class Outcome : IOutcome
    {
        public Outcome(string typeName)
        {
            TypeName = typeName;
            Data = new Dictionary<string, object>();
        }
        //public Outcome(Type type)
        //{
        //    Type = type;
        //}
        //public Type Type { get; }
        public string TypeName { get; set; }
        public int SourceExecutionID { get; set; }
        public int TargetExecutionID { get; set; } // Parent component.
        public Dictionary<string, object> Data { get; set; }


        //public int ExecutionThreadID { get; set; }
        //public int SourceComponentID { get; set; } // Within the execution thread.
        //public string OutcomeData { get; set; }
    }

    public abstract class Outcome<T> : Outcome
    {
        public Outcome() : base(typeof(T).FullName)
        {
        }
    }
}
