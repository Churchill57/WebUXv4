using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.CustomerDomain.ApiClient.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.Interfaces;
using GOLD.CustomerDomain.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    [ComponentInterface(typeof(IUxCustomerSearchCriteria))]
    [ComponentTitle("Search Customer")]
    [ComponentPrimaryRoute("Customer/UxCustomerSearchCriteria")]
    [ComponentSecondaryRoute("Customer/UxCustomerSearchCriteria")]
    public class UxCustomerSearchCriteria : UserExperience, IUxCustomerSearchCriteria, IUxPerformSearch<Customer>
    {
        private readonly ICustomerDomainApiClient customerDomainApiClient;

        public UxCustomerSearchCriteria(ICustomerDomainApiClient customerDomainApiClient)
        {
            this.customerDomainApiClient = customerDomainApiClient;
        }
        public UxCustomerSearchCriteria() : this(DependencyResolver.Current.GetService<ICustomerDomainApiClient>())
        {

        }

        //[ComponentInput("customer")]
        //public int? CustomerId { get; set;}
        [PropertyIsComponentState]
        public string CustomerName { get; set; }

        [PropertyIsComponentState]
        public DateTime? CustomerDateOfBirth { get; set; }

        [PropertyIsComponentState]
        public bool ShowSwitchToAdvanced { get; set; } = false;

        [PropertyIsComponentState]
        public bool ShowBackButton { get; set; } = true;

        [PropertyIsComponentState]
        public string BackButtonText { get; set; } = "Back";

        [PropertyIsComponentState]
        public string SearchButtonText { get; set; } = "Search";

        public void ResetFields()
        {
            CustomerName = null;
            CustomerDateOfBirth = null;
        }

        public async Task<IEnumerable<Customer>> PerformSearchAsync()
        {
            var customers = await customerDomainApiClient.GetCustomerAsync();

            if (String.IsNullOrEmpty(CustomerName))
            {
                return customers;
            }

            var result = new List<Customer>();
            foreach (var c in customers)
            {
                if ((c.FirstName + c.LastName).ToLower().Contains(CustomerName.ToLower()))
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public async Task SaveCustomerSearchCriteriaAsync(CustomerSearchCriteriaViewModel criteria)
        {
            CustomerName = criteria.Name;
            CustomerDateOfBirth = criteria.DOB;
            await SaveAsync();
        }

        public UxCustomerSearchCriteriaViewModel GetViewModel()
        {
            return new UxCustomerSearchCriteriaViewModel()
            {
                Criteria = new CustomerSearchCriteriaViewModel()
                {
                    Name = CustomerName,
                    DOB = CustomerDateOfBirth
                },
                BackButtonText = BackButtonText,
                SearchButtonText = SearchButtonText,
                ShowBackButton = ShowBackButton,
                ShowSwitchToAdvanced = ShowSwitchToAdvanced,
                txid = this.TXID.ToString()
            };

        }

    }
}