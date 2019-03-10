using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOLD.CustomerDomain.ApiModels;

namespace GOLD.CustomerDomain.ApiClient.Interfaces
{
    public interface ICustomerDomainApiClient
    {
        Task<IEnumerable<Customer>> GetCustomerAsync();
        Task<Customer> GetCustomerAsync(int id);
    }
}