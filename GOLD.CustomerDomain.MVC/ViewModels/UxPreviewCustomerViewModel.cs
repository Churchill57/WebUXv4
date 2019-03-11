using GOLD.CustomerDomain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.ViewModels
{
    public class UxPreviewCustomerViewModel
    {
        public Customer Customer { get; set; }
        public bool PreviewDifferentCustomer { get; set; }
        public bool ShowBackButton { get; set; }
        public string BackButtonText { get; set; }
        public bool BackButtonAsLink { get; set; }
        public string DoneButtonText { get; set; }
        public string txid { get; set; }
    }
}