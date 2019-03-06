using GOLD.Core.Outcomes;
using GOLD.TestsDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.TestsDomain.MVC.Outcomes
{
    public class GotoUxAOutcome : Outcome<GotoUxAOutcome>
    {
        public int DevilsOwn { get; set; }
    }
}