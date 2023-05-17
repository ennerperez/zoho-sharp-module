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
        private string Name => Enum.GetName(Enums.Module.Subscriptions);
        
        private readonly Factory _factory;

        public SubscriptionService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<JObject> CreateAsync(object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync(Name, "subscriptions", input);
        }

        public async Task<JObject> CreateSubscriptionAsync(object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync(Name, "hostedpages/newsubscription", input);
        }

        public async Task<JObject> CreateCustomerAsync(object input)
        {
            var client = await _factory.CreateAsync();
            //https://subscriptions.zoho.com/api/v1/customers
            return await client.InvokePostAsync(Name, "customers", input);
        }

        public async Task<JObject> UpdateCustomerAsync(object input, string customerId)
        {
            var client = await _factory.CreateAsync();
            //https://subscriptions.zoho.com/api/v1/customers
            return await client.InvokePutAsync(Name, $"customers/{customerId}", input);
        }

        public async Task<JObject> CreateRenewalAsync(string subscriptionId, object input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync(Name, $"subscriptions/{subscriptionId}/postpone", input);
        }

        public async Task<JObject> AddChargeAsync(string subscriptionId, JObject input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync(Name, $"subscriptions/{subscriptionId}/charge", input);
            return response;
        }

        public async Task<bool> SetCardCollect(string subscriptionId, JObject input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var client = await _factory.CreateAsync();

            var response = await client.InvokePostAsync(Name, $"subscriptions/{subscriptionId}/card", input);

            if (null != response && response.Property("Error") != null)
            {
                throw new Exception(response.Property("Error")?.Value.ToString());
            }

            return true;
        }

        public async Task<JObject> RequestPaymentMethod(string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException("customerId");
            }

            var client = await _factory.CreateAsync();

            var response = await client.InvokeGetAsync(Name, $"customers/{customerId}/requestpaymentmethod");

            if (null != response && response.Property("Error") != null)
            {
                throw new Exception(response.Property("Error")?.Value.ToString());
            }

            return response;
        }

        public async Task<List<JObject>> GetCards(string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException("customerId");
            }

            var client = await _factory.CreateAsync();

            var response = await client.InvokeGetAsync<JObject[]>(Name, $"customers/{customerId}/cards");

            return response.ToList();
        }

        public async Task<List<JObject>> GetProducts()
        {
            return await GetProducts<JObject>();
        }

        public async Task<List<T>> GetProducts<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>(Name, "products", "products");
            return response.ToList();
        }

        public async Task<List<JObject>> GetCustomers()
        {
            return await GetCustomers<JObject>();
        }

        public async Task<List<T>> GetCustomers<T>()
        {
            var client = await _factory.CreateAsync();
            //https://subscriptions.zoho.com/api/v1/customers
            var response = await client.InvokeGetAsync<T[]>(Name, "customers", "customers");
            return response.ToList();
        }

        public async Task<List<JObject>> GetPlans()
        {
            return await GetPlans<JObject>();
        }

        public async Task<List<T>> GetPlans<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>(Name, "plans", "plans");
            return response.ToList();
        }

        public async Task<List<JObject>> GetAddons()
        {
            return await GetAddons<JObject>();
        }

        public async Task<List<T>> GetAddons<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>(Name, "addons", "addons");
            return response.ToList();
        }

        public async Task<List<JObject>> GetCoupons()
        {
            return await GetCoupons<JObject>();
        }

        public async Task<List<T>> GetCoupons<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>(Name, "addons", "addons");
            return response.ToList();
        }

        public async Task<List<JObject>> GetSubscriptions()
        {
            return await GetSubscriptions<JObject>();
        }

        public async Task<List<T>> GetSubscriptions<T>()
        {
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T[]>(Name, "subscriptions", "subscriptions");
            return response.ToList();
        }

    }
}
