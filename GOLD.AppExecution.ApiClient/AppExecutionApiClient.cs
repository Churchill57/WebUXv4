﻿using GOLD.AppExecution.ApiModels;
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


        public async Task<ExecutionThread> SaveNewExecutionThreadAsync(ExecutionThread executionThread)
        {
            var response = await httpClient.PostAsJsonAsync<ExecutionThread>("api/ExecutionThreads", executionThread);
            response.EnsureSuccessStatusCode();
            //var newResourceLocation = response.Headers.Location;
            return await response.Content.ReadAsAsync<ExecutionThread>();
        }


        public async Task<ExecutionThread> SaveExecutionThreadAsync(ExecutionThread executionThread)
        {
            var response = await httpClient.PutAsJsonAsync<ExecutionThread>("api/ExecutionThreads", executionThread);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ExecutionThread>();
        }

        public ExecutionThread SaveExecutionThread(ExecutionThread executionThread)
        {
            HttpResponseMessage response = null;
            Task task = Task.Run(async () => response = await  httpClient.PutAsJsonAsync<ExecutionThread>("api/ExecutionThreads", executionThread));
            task.Wait();
            return response.Content.ReadAsAsync<ExecutionThread>().Result;
        }


        public async Task<ExecutionThread> LoadExecutionThreadAsync(int ID)
        {
            var response = await httpClient.GetAsync($"api/ExecutionThreads/{ID}");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<ExecutionThread>().Result;
        }

        public ExecutionThread LoadExecutionThread(int ID)
        {
            HttpResponseMessage response = null;
            Task task = Task.Run(async () => response = await httpClient.GetAsync($"api/ExecutionThreads/{ID}"));
            task.Wait();
            return response.Content.ReadAsAsync<ExecutionThread>().Result;
        }

    }

}
