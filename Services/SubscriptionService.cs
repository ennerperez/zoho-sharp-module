using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zoho.Abstractions.Models;
using Zoho.Interfaces;
using Zoho.Subscriptions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zoho.Services
{
    public class SubscriptionService : ZohoService, ISubscriptionService
    {
        
        public SubscriptionService(HttpClient httpClient, ILoggerFactory loggerFactory) : base(httpClient, loggerFactory)
        {
        }

        public async Task<JObject> CreateAsync(Subscription input)
        {
            return await InvokePostAsync("Subscriptions", "subscriptions", input);
        }

        public async Task<JObject> AddChargeAsync(string subscriptionId, Charge input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var validationResult = input.Validate();
            if (!validationResult && input.Errors.Any())
                throw new ArgumentNullException(input.Errors.First());

            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"subscriptions/{subscriptionId}/charge", content);
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error)
                throw processResult.Error;

            return processResult.Data;
        }

        public async Task<bool> SetCardCollect(string subscriptionId, Card input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"subscriptions/{subscriptionId}/card", content);
            var processResult = await ProcessResponse<Response>(response);

            if (null != processResult.Error)
                throw processResult.Error;

            return true;
        }

        public async Task<JObject> CreateAsync(Customer input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var validationResult = input.Validate();
            if (!validationResult && input.Errors.Any())
                throw new ArgumentNullException(input.Errors.First());

            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("customers", content);
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error)
                throw processResult.Error;

            return processResult.Data;
        }

        public async Task<JObject> RequestPaymentMethod(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            //var data = JsonConvert.SerializeObject(customerId, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});

            var response = await _httpClient.GetAsync($"customers/{customerId}/requestpaymentmethod");
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error)
                throw processResult.Error;

            return processResult.Data;
        }

        public async Task<List<Card>> GetCards(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            //var data = JsonConvert.SerializeObject(customerId, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});

            var response = await _httpClient.GetAsync($"customers/{customerId}/cards");
            var processResult = await ProcessResponse<Card[]>(response);

            if (null != processResult.Error)
                throw processResult.Error;

            return processResult.Data.ToList();
        }

        public async Task<List<Plan>> GetPlans()
        {
            await GetTokenAsync();
            SetHttpClient("Subscriptions");

            var response = await _httpClient.GetAsync("plans");
            var processResult = await ProcessResponse<Plan[]>(response);

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data.ToList();
        }

        public string GetOption(string key)
        {
            return base.GetOption("Subscriptions", key);
        }
    }
}