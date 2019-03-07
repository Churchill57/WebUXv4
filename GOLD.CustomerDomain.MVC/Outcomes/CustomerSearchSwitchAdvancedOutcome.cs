using GOLD.Core.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.Outcomes
{
    public class CustomerSearchSwitchAdvancedOutcome : Outcome<CustomerSearchSwitchAdvancedOutcome>
    {
        public bool AdvancedSearch { get; set; }
    }
}