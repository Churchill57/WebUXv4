using GOLD.Core.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Components
{
    public abstract class LogicalUnit : Component
    {
        public abstract Component GetNextComponent();
        public abstract void HandleOutcome(Outcome outcome);
    }
}
