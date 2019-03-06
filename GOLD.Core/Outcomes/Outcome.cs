using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Outcomes
{
    public class Outcome
    {
        private Outcome()
        {

        }
        public Outcome(Type type)
        {
            if (type != null)
            {
                TypeName = type.FullName;
            }
            Data = new Dictionary<string, object>();
        }
        public string TypeName { get; set; }
        public int SourceExecutionID { get; set; }
        public int TargetExecutionID { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    public abstract class Outcome<T> : Outcome
    {
        public Outcome() : base(typeof(T))
        {
        }
    }

}
