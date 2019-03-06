using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.TestsDomain.Interfaces;
using GOLD.TestsDomain.MVC.Outcomes;
using GOLD.TestsDomain.MVC.UserExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOLD.TestsDomain.MVC.LogicalUnits
{
    //[Authorize(Roles = "Admin, pleb")]
    [ComponentInterface(typeof(ILuTest1))]
    [ComponentTitle("Test1")]
    [ComponentDescription("Initial GOLD framework Test1")]
    [ComponentPrimaryRoute("Tests/LuTest1Primary")]
    [ComponentSecondaryRoute("Tests/LuTest1Secondary")]
    [ComponentSearchTags("Test1")]
    [ComponentDependsUpon(typeof(UxA), typeof(UxB))]
    public class LuTest1 : LogicalUnit, ILuTest1
    {
        [PropertyIsLaunchInput("Some Customer ID", "int")]
        [PropertyIsContextInput("customer")]
        [PropertyIsComponentState]
        public EntityContext SomeCustomer { get; set; } // N.B. NOT part of iLuTest1!!!

        [PropertyIsComponentState]
        public string nextUx { get; set; } = "UxA";

        #region iLuTest properties

        [PropertyIsComponentState]
        public int SomeInteger { get; set; }

        [PropertyIsLaunchInput("SomeString", "string")]
        [PropertyIsComponentState]
        public string SomeString { get; set; }

        #endregion

        public override async Task<IComponent> GetNextComponentAsync()
        {
            if (nextUx == "UxA")
            {
                var proxyIUxA = await UseComponentInterfaceAsync<IUxA>("A1");
                proxyIUxA.SomeInterfacePropertyA = $"The time is {DateTime.Now}";
                nextUx = "UxB";
                return (IComponent)proxyIUxA;
            }

            if (nextUx == "UxB")
            {
                var proxyIUxB = await UseComponentInterfaceAsync<IUxB>("B1");
                proxyIUxB.SomeInterfacePropertyB = $"The time is {DateTime.Now}";
                nextUx = null;
                return (IComponent)proxyIUxB;
            }

            // Done
            return null;

        }

        //public override IComponent GetNextComponent()
        //{
        //    var obj = UseComponentAsync<UxA>("A").Result;

        //    var interfaceProxy = UseComponentInterfaceAsync<IUxA>("A");

        //    return (IComponent)interfaceProxy;
        //}


        public override void HandleOutcome(Outcome outcome)
        {
            if (outcome as GotoUxAOutcome != null)
            {
                nextUx = "UxA";
            }

            if (outcome as GotoUxBOutcome != null)
            {
                nextUx = "UxB";
            }

            if (outcome as ComponentDoneOutcome != null)
            {
                nextUx = "Done";
            }

        }

    }
}