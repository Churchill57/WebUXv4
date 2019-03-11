using GOLD.CustomerDomain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.ViewModels
{
    public class UxSelectCustomerViewModel
    {
        public List<Customer> Customers { get; set; }
        public bool ShowBackButton { get; set; }
        public bool BackButtonAsLink { get; set; }
        public string BackButtonText { get; set; }
        public string SelectButtonText { get; set; }
        public bool ShowPreviewLink { get; set; }
        public string txid { get; set; }
    }
}