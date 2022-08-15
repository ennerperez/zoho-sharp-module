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
        
        public async Task<JObject> CreateSubscriptionAsync(object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Subscriptions", "hostedpages/newsubscription", input);
        }

        public async Task<JObject> CreateRenewalAsync(string subscriptionId, object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Subscriptions", $"subscriptions/{subscriptionId}/postpone", input);
        }
        
        public async Task<JObject> AddChargeAsync(string subscriptionId, JObject input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync("Subscriptions", $"subscriptions/{subscriptionId}/charge", input);
            return response;
        }

        public async Task<bool> SetCardCollect(string subscriptionId, JObject input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync("Subscriptions", $"subscriptions/{subscriptionId}/card", input);

            if (null != response && response.Property("Error") != null)
                throw new Exception(response.Property("Error")?.Value.ToString());

            return true;
        }

        public async Task<JObject> RequestPaymentMethod(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            var client = await _factory.CreateAsync();

            var response = await client.InvokeGetAsync("Subscriptions", $"customers/{customerId}/requestpaymentmethod");

            if (null != response && response.Property("Error") != null)
                throw new Exception(response.Property("Error")?.Value.ToString());

            return response;
        }

        public async Task<List<JObject>> GetCards(string customerId)
        {
            if (customerId == null)
                throw new ArgumentNullException("customerId");

            var client = await _factory.CreateAsync();

            var response = await client.InvokeGetAsync<JObject[]>("Subscriptions", $"customers/{customerId}/cards");

            return response.ToList();
        }

        public async Task<List<JObject>> GetProducts()
        {
            return await GetProducts<JObject>();
        }

        public async Task<List<T>> GetProducts<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>("Subscriptions", "products", "products");
            return response.ToList();
        }

        public async Task<List<JObject>> GetPlans()
        {
            return await GetPlans<JObject>();
        }

        public async Task<List<T>> GetPlans<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>("Subscriptions", "plans", "plans");
            return response.ToList();
        }

        public async Task<List<JObject>> GetAddons()
        {
            return await GetAddons<JObject>();
        }

        public async Task<List<T>> GetAddons<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>("Subscriptions", "addons", "addons");
            return response.ToList();
        }
        
        public async Task<List<JObject>> GetCoupons()
        {
            return await GetCoupons<JObject>();
        }

        public async Task<List<T>> GetCoupons<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>("Subscriptions", "addons", "addons");
            return response.ToList();
        }
        
        public async Task<List<JObject>> GetSubscriptions()
        {
            return await GetSubscriptions<JObject>();
        }

        public async Task<List<T>> GetSubscriptions<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>("Subscriptions", "subscriptions", "subscriptions");
            return response.ToList();
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Subscriptions", key);
        }
    }
}
