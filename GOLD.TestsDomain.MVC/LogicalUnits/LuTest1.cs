using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.MVC.UserExperiences;
using GOLD.TestsDomain.Interfaces;
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

        #region iLuTest properties

        [PropertyIsComponentState]
        public int SomeInteger { get; set; }

        [PropertyIsLaunchInput("SomeString", "string")]
        [PropertyIsComponentState]
        public string SomeString { get; set; }

        #endregion

        public async Task<Component> GetNextComponentAsync()
        {
            var interfaceProxy = await UseComponentInterfaceAsync<IUxA>("A");

            return interfaceProxy;

            //return await UseComponentAsync<UxA>("A");
        }

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