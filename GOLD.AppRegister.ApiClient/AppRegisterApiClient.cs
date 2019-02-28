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
            var component = new ComponentByInterfaceFullName();

            // IMPORTANT: Trailing slash in URL mitigates period problem in interface full name (e.g. namespace)!
            var response = await httpClient.GetAsync($"api/Components/GetComponentByInterfaceFullName/{componentInterfaceFullName}/");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                component = JsonConvert.DeserializeObject<ComponentByInterfaceFullName>(result);
            }

            return component;
        }

    }
}
