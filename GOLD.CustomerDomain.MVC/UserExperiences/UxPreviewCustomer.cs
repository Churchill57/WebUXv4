using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.ApiClient.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        private readonly ICustomerDomainApiClient customerDomainApiClient;

        public UxPreviewCustomer(ICustomerDomainApiClient customerDomainApiClient)
        {
            this.customerDomainApiClient = customerDomainApiClient;
        }
        public UxPreviewCustomer() : this(DependencyResolver.Current.GetService<ICustomerDomainApiClient>())
        {

        }

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

        public async Task<Customer> LoadCustomerAsync(int id)
        {
            var customer = await customerDomainApiClient.GetCustomerAsync(id);
            CustomerContext = new EntityContext(customer.ID, "customer", customer.LastName); // TODO: customer Fullname for context
            return customer;
        }

    }
}