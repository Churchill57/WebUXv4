using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.MVC.UserExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.MVC.LogicalUnits
{
    //[Authorize(Roles = "Admin, pleb")]
    [ComponentTitle("Preview Customer")]
    [ComponentDescription("Shows the personal details of a specific customer")]
    [ComponentPrimaryRoute(@"Customer\PreviewCustomer")]
    [ComponentSecondaryRoute(@"Customer\PreviewCustomer")]
    [ComponentSearchTags("Preview", "Customer", "another tag","and another tage")]
    [ComponentDependsUpon(typeof(UxPreviewCustomer))]
    public class LuPreviewCustomer : CustomerBaseLogicalUnit
    {
        [PropertyIsLaunchInput("Customer ID", "int")]
        [PropertyIsContextInput("customer")]
        [PropertyIsComponentState]
        public EntityContext CustomerToPreview { get; set; }

        public override Component GetNextComponent()
        {
            throw new NotImplementedException();
        }

        public override void HandleOutcome(Outcome outcome)
        {
            throw new NotImplementedException();
        }

        public string Test1()
        {
            var result = ExecutionManager.Test1();
            return result;
        }
    }
}