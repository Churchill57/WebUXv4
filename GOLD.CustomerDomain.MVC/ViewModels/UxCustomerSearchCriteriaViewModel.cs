using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.ViewModels
{
    public class UxCustomerSearchCriteriaViewModel
    {
        public CustomerSearchCriteriaViewModel Criteria { get; set; }
        public bool ShowSwitchToAdvanced { get; set; }
        public bool ShowBackButton { get; set; }
        public string BackButtonText { get; set; }
        public string SearchButtonText { get; set; }
        public string txid { get; set; }
    }
}