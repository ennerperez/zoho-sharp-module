using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        
        private readonly Factory _factory;
        
        public SubscriptionService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public async Task<JObject> CreateAsync(object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Subscriptions", "subscriptions", input);
        }

        public async Task<JObject> AddChargeAsync(string subscriptionId, JObject input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // var validationResult = input.Validate();
            // if (!validationResult && input.Errors.Any())
            //     throw new ArgumentNullException(input.Errors.First());

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync("Subscriptions", $"subscriptions/{subscriptionId}/charge", input);
            return response;

            // var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            // var content = new StringContent(data, Encoding.UTF8, "application/json");
            //
            // var response = await _httpClient.PostAsync($"subscriptions/{subscriptionId}/charge", content);
            // var processResult = await ProcessResponse<JObject>(response);
            //
            // if (null != processResult.Error)
            //     throw processResult.Error;
            //
            // return processResult.Data;
        }

        public async Task<bool> SetCardCollect(string subscriptionId, JObject input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync("Subscriptions", $"subscriptions/{subscriptionId}/card", input);

            if (null != response && response.Property("Error") != null)
                throw new Exception(response.Property("Error")?.Value.ToString());

            // var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            // var content = new StringContent(data, Encoding.UTF8, "application/json");
            //
            // var response = await _httpClient.PostAsync($"subscriptions/{subscriptionId}/card", content);
            // var processResult = await ProcessResponse<Response>(response);

            return true;
        }


        //         public async Task<JObject> CreateAsync(JObject input)
//         {
//             if (input == null)
//                 throw new ArgumentNullException("input");
//
//             // var validationResult = input.Validate();
//             // if (!validationResult && input.Errors.Any())
//             //     throw new ArgumentNullException(input.Errors.First());
//
//             var client = await _factory.CreateAsync();
//             
// //Subscriptions
//
//             var response = await client.InvokePostAsync("Subscriptions", "customers", input);
//             
//             if (null != response && response.Property("Error") != null)
//                 throw new Exception(response.Property("Error")?.Value.ToString());
//             
//             return response;
//
//             // var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
//             // var content = new StringContent(data, Encoding.UTF8, "application/json");
//             //
//             // var response = await _httpClient.PostAsync("customers", content);
//             // var processResult = await ProcessResponse<JObject>(response);
//             //
//             // if (null != processResult.Error)
//             //     throw processResult.Error;
//             //
//             // return processResult.Data;
//         }

        public async Task<JObject> RequestPaymentMethod(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            var client = await _factory.CreateAsync();
            
            var response = await client.InvokeGetAsync("Subscriptions", $"customers/{customerId}/requestpaymentmethod");
            
            if (null != response && response.Property("Error") != null)
                throw new Exception(response.Property("Error")?.Value.ToString());
            
            return response;

            // //var data = JsonConvert.SerializeObject(customerId, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            //
            // var response = await _httpClient.GetAsync($"customers/{customerId}/requestpaymentmethod");
            // var processResult = await ProcessResponse<JObject>(response);
            //
            // if (null != processResult.Error)
            //     throw processResult.Error;

            //return processResult.Data;
        }

        public async Task<List<JObject>> GetCards(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            var client = await _factory.CreateAsync();
            
            var response = await client.InvokeGetAsync<JObject[]>("Subscriptions", $"customers/{customerId}/cards");
            
            // if (null != response && response.Property("Error") != null)
            //     throw new Exception(response.Property("Error")?.Value.ToString());
            
            return response.ToList();

            // //var data = JsonConvert.SerializeObject(customerId, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            //
            // var response = await _httpClient.GetAsync($"customers/{customerId}/cards");
            // var processResult = await ProcessResponse<Card[]>(response);
            //
            // if (null != processResult.Error)
            //     throw processResult.Error;
            //
            // return processResult.Data.ToList();
        }

        public async Task<List<JObject>> GetPlans()
        {
            var client = await _factory.CreateAsync();
            
            var response = await client.InvokeGetAsync<JObject[]>("Subscriptions", $"plans");

            return response.ToList();

            // var response = await _httpClient.GetAsync("plans");
            // var processResult = await ProcessResponse<Plan[]>(response);

            // if (null != processResult.Error) throw processResult.Error;
            //
            // return processResult.Data.ToList();
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Subscriptions", key);
        }
    }
}