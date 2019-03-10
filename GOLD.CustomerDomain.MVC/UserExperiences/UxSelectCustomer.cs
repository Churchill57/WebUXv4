using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Models;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    [ComponentInterface(typeof(IUxSelectCustomer))]
    [ComponentTitle("Select Customer")]
    [ComponentPrimaryRoute("Customer/UxSelectCustomer")]
    [ComponentSecondaryRoute("Customer/UxSelectCustomer")]
    public class UxSelectCustomer : UserExperience, IUxSelectCustomer
    {

        [PropertyIsComponentState]
        public int? SearchTaskId { get; set; }

        [PropertyIsComponentState]
        internal EntityContext SelectedCustomerContext { get; set; }

        [PropertyIsComponentState]
        public bool ShowBackButton { get; set; } = true;

        [PropertyIsComponentState]
        public bool BackButtonAsLink { get; set; } = false;

        [PropertyIsComponentState]
        public string BackButtonText { get; set; } = "Back";

        [PropertyIsComponentState]
        public string SelectButtonText { get; set; } = "Select";

        [PropertyIsComponentState]
        public bool ShowPreviewLink { get; set; } = false;

        public void SetCustomer(int id, string fullName)
        {
            SelectedCustomerContext = new EntityContext(id, fullName, fullName);
            // TODO: Set current customer context;
            //SelectedCustomerContext = CtxMan.SetContext(id, "customer", fullName) as EntityContext;
        }

    }
}