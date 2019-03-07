using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    //[Authorize(Roles = "Admin, pleb")]

    // N.B. This particular component will not be a directly launchable component and so does not carry primary/secondary route attributes.
    // It can however be invoked from a LogicalUnit such as LuPreviewCustomer.
    [ComponentInterface(typeof(IUxPreviewCustomer))]
    [ComponentTitle("Preview Customer")]
    [ComponentPrimaryRoute("Customer/UxPreviewCustomer")]
    [ComponentSecondaryRoute("Customer/UxPreviewCustomer")]
    public class UxPreviewCustomer : UserExperience, IUxPreviewCustomer, ILuPreviewCustomer
    {

        [PropertyIsLaunchInput("Customer ID", "int")]
        [PropertyIsComponentState]
        public EntityContext CustomerContext { get; set; }
        //public int? CustomerId { get; set; }

        [PropertyIsComponentState]
        public bool PreviewDifferentCustomer { get; set; } = false;

        [PropertyIsComponentState]
        public bool ShowBackButton { get; set; } = false;

        [PropertyIsComponentState]
        public string BackButtonText { get; set; } = "Back";

        [PropertyIsComponentState]
        public bool BackButtonAsLink { get; set; } = false;

        [PropertyIsComponentState]
        public string DoneButtonText { get; set; } = "Close";

            // TODO: Customer API
        //public Models.Customer LoadCustomer(int id)
        //{
        //    var db = new CustomerDbContext();
        //    var result = db.Customers.Find(id);

        //    CustomerContext = CtxMan.SetContext(result.Id, "customer", result.FullName) as EntityContext;

        //    return result;
        //}

    }
}