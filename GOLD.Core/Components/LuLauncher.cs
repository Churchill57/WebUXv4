using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLD.Core.Outcomes;

namespace GOLD.Core.Components
{
    public class LuLauncher : LogicalUnit // or Component???
    {
        public override Component GetNextComponent()
        {
            throw new NotImplementedException();
        }

        public override void HandleOutcome(Outcome outcome)
        {
            throw new NotImplementedException();
        }
    }
}
