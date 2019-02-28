using GOLD.AppExecution.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppExecution.ApiClient
{
    public class AppExecutionApiClient
    {
        private readonly Lazy<HttpClient> lazyHttpClient;

        public AppExecutionApiClient(Lazy<HttpClient> lazyHttpClient)
        {
            this.lazyHttpClient = lazyHttpClient;
        }

        private HttpClient httpClient => lazyHttpClient.Value;


        public async Task<ExecutionThread> SaveNewExecutionThread(ExecutionThread executionThread)
        {
            var result = new ExecutionThread();
            var response = await httpClient.PostAsJsonAsync<ExecutionThread>("api/ExecutionThreads", executionThread);
            response.EnsureSuccessStatusCode();
            var newResourceLocation = response.Headers.Location;
            result = await response.Content.ReadAsAsync<ExecutionThread>();

            return result;

            //string strPayload = JsonConvert.SerializeObject(executionThread);
            //HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync($"api/ExecutionThreads", c);
            //HttpContent c = new StringContent("Hello John", Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync($"api/ExecutionThreads", c);
            //var result = response.Content.ReadAsStringAsync().Result;


            //Uri u = new Uri("http://localhost:31404/Api/Customers");
            //var payload = "{\"CustomerId\": 5,\"CustomerName\": \"Pepsi\"}";

            //HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            //var t = Task.Run(() => PostURI(u, c));
            //t.Wait();

            //Console.WriteLine(t.Result);
            //Console.ReadLine();

            //return 0;
        }

        async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
                HttpResponseMessage result = await httpClient.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            return response;
        }
        //static async Task<string> SendURI(Uri u, HttpContent c)
        //{
        //    var response = string.Empty;
        //    HttpRequestMessage request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = u,
        //        Content = c
        //    };

        //    HttpResponseMessage result = await httpClient.SendAsync(request);
        //    if (result.IsSuccessStatusCode)
        //    {
        //        response = result.StatusCode.ToString();
        //    }
        //    return response;
        //}

        //private GOLD.AppExecution.DataAccess.ExecutionThread PersistNewExecutionThread(ExecutionThread executionThread)
        //{
        //    var thread = new GOLD.AppExecution.DataAccess.ExecutionThread()
        //    {
        //        ComponentExecutingID = executionThread.ComponentExecutingID,
        //        ExecutingComponentsJson = JsonConvert()
        //    };
        //    return thread;
        //}


        //public async Task<ComponentByInterfaceFullName> GetComponentByInterfaceFullName(string componentInterfaceFullName)
        //{
        //    var component = new ComponentByInterfaceFullName();

        //    // IMPORTANT: Trailing slash in URL mitigates period problem in interface full name (e.g. namespace)!
        //    var response = await httpClient.GetAsync($"api/Components/GetComponentByInterfaceFullName/{componentInterfaceFullName}/");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = response.Content.ReadAsStringAsync().Result;
        //        component = JsonConvert.DeserializeObject<ComponentByInterfaceFullName>(result);
        //    }

        //    return component;
        //}

    }

    }
