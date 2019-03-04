using GOLD.Core.Interfaces;
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
        public abstract IComponent GetNextComponent();
        public abstract Task<IComponent> GetNextComponentAsync();
        public abstract void HandleOutcome(Outcome outcome);
    }
}
