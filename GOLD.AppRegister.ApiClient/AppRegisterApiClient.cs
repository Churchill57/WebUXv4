using GOLD.AppRegister.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppRegister.ApiClient
{
    public class AppRegisterApiClient
    {
        private readonly Lazy<HttpClient> lazyHttpClient;

        public AppRegisterApiClient(Lazy<HttpClient> lazyHttpClient)
        {
            this.lazyHttpClient = lazyHttpClient;
        }

        private HttpClient httpClient => lazyHttpClient.Value;

        public async Task<ComponentByInterfaceFullName> GetComponentByInterfaceFullName(string componentInterfaceFullName)
        {
            var response = await httpClient.GetAsync($"api/Components/GetComponentByInterfaceFullName/{componentInterfaceFullName}/");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<ComponentByInterfaceFullName>().Result;
        }

    }
}
