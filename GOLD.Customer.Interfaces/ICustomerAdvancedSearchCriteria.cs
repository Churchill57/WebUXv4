using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Customer.Interfaces
{
    public interface ICustomerAdvancedSearchCriteria
    {
        int Age { get; set; }
        string FirstName { get; set; }
        string NINO { get; set; }
        string PostCode { get; set; }
        string Surname { get; set; }
        string Town { get; set; }
        Gender Gender { get; set; }
    }
    public class CustomerAdvancedSearchCriteria : ICustomerAdvancedSearchCriteria
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string NINO { get; set; }
        public string PostCode { get; set; }
        public string Surname { get; set; }
        public string Town { get; set; }
        public Gender Gender { get; set; }
    }
}
