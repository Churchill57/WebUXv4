using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOLD.Core.Attributes;
using GOLD.Core.Interfaces;
using GOLD.Core.Outcomes;

namespace GOLD.Core.Components
{
    public class LuRedirect : LogicalUnit // or Component???
    {
        //[PropertyIsComponentState]
        //public string ComponentName { get; internal set; }

        //[PropertyIsComponentState]
        //public int? ComponentTaskId { get; internal set; }

        [PropertyIsComponentState]
        public string ReturnUrl { get; set; }

        //[PropertyIsComponentState]
        //public int? ReturnTaskId { get; set; }

        //[PropertyIsComponentState]
        //public int? ResumeTaskId { get; set; }

        //[PropertyIsComponentState]
        //public string ReturnTaskRef { get; set; }

        //public LuLauncher(string componentName, string userName, string returnUrl)
        //{
        //    ComponentName = componentName;
        //    UserName = userName;
        //    ReturnUrl = returnUrl;
        //}

        public override IComponent GetNextComponent()
        {
            throw new NotImplementedException();
        }

        public override Task<IComponent> GetNextComponentAsync()
        {
            throw new NotImplementedException();
        }

        public override void HandleOutcome(Outcome outcome)
        {
            throw new NotImplementedException();
        }
    }
}
