using GOLD.CustomerDomain.ApiClient.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.CustomerDomain.ApiClient
{
    public class CustomerDomainApiClient : ICustomerDomainApiClient
    {
        private static readonly Lazy<HttpClient> lazyHttpClient = new Lazy<HttpClient>(() => {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["GOLD.CustomerDomain.WebApi.BaseURL"])
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        });

        private HttpClient httpClient => lazyHttpClient.Value;

        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {
            var response = await httpClient.GetAsync("api/Customer");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/Customer/{id}");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<Customer>().Result;
        }

    }
}
