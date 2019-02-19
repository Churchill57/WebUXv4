using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Customer.Interfaces
{
    public interface IUxCustomerAdvSearchCriteria
    {
        ICustomerAdvancedSearchCriteria Criteria { get; set; }
        bool ShowSwitchToBasic { get; set; }

        IList<ICustomerAdvancedSearchCriteria> Criterias { get; set; }
   }

}
