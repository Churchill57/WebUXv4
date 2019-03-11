using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.Interfaces;
using GOLD.CustomerDomain.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<UxSelectCustomerViewModel> GetSearchResultsViewModelAsync()
        {
            var uxPerformCustomerSearch = Load(SearchTaskId.Value) as IUxPerformSearch<Customer>;
            var customers = await uxPerformCustomerSearch.PerformSearchAsync();
            return new UxSelectCustomerViewModel()
            {
                Customers = customers.ToList(),
                BackButtonAsLink = BackButtonAsLink,
                BackButtonText = BackButtonText,
                SelectButtonText = SelectButtonText,
                ShowBackButton = ShowBackButton,
                ShowPreviewLink = ShowPreviewLink,
                txid = this.TXID.ToString()
            };
        }

        public async Task SaveSelectedCustomerContextAsync(int id, string fullName)
        {
            // TODO: Set current customer context;
            SelectedCustomerContext = new EntityContext(id, "customer", fullName);

            // TODO: Is explicit saver really necessary. Could not executionManager.RaiseOutcomeAsync somehow do the save if the Ux and/or execution thread is dirty?
            await SaveAsync();
        }

    }
}