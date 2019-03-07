using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    [ComponentInterface(typeof(IUxCustomerSearchCriteria))]
    [ComponentTitle("Search Customer")]
    [ComponentPrimaryRoute("Customer/UxCustomerSearchCriteria")]
    [ComponentSecondaryRoute("Customer/UxCustomerSearchCriteria")]
    public class UxCustomerSearchCriteria : UserExperience, IUxCustomerSearchCriteria, IUxPerformSearch<Customer>
    {

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

        //public CustomerSearchSwitchAdvancedEventArgs SetAdvancedSearch(bool advancedSearch)
        //{
        //    return new CustomerSearchSwitchAdvancedEventArgs(this, advancedSearch);
        //}

        public IEnumerable<Customer> PerformSearch()
        {
            var customers = new List<Customer>()
            {
                new Customer() {ID=1, FirstName="Fred", LastName="Norris"},
                new Customer() {ID=2, FirstName="Harry", LastName="Grant"},
                new Customer() {ID=3, FirstName="John", LastName="Smith"},
                new Customer() {ID=4, FirstName="Penny", LastName="Jone"},
                new Customer() {ID=5, FirstName="Michael", LastName="Harris"},
                new Customer() {ID=6, FirstName="Claire", LastName="Front"}
            };

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


    }
}