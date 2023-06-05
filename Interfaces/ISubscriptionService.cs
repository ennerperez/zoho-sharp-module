using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/subscriptions/api/v1/
    /// </summary>
    public interface ISubscriptionService : IZohoService
    {
        Task<JObject> AddChargeAsync(string subscriptionId, JObject input);
        Task<bool> SetCardCollect(string subscriptionId, JObject input);
        Task<JObject> CreateAsync(object input);
        Task<JObject> CreateSubscriptionAsync(object input);
        Task<JObject> CreateRenewalAsync(string subscriptionId, object input);
        Task<JObject> RequestPaymentMethod(string customerId);
        Task<List<JObject>> GetCards(string customerId);
        Task<List<JObject>> GetProducts();
        Task<List<T>> GetProducts<T>();
        Task<List<JObject>> GetPlans();
        Task<List<T>> GetPlans<T>();
        Task<List<JObject>> GetAddons();
        Task<List<T>> GetAddons<T>();
        Task<List<JObject>> GetCoupons();
        Task<List<T>> GetCoupons<T>();

        Task<List<JObject>> GetCustomers();
        Task<List<T>> GetCustomers<T>();

        Task<JObject> CreateCustomerAsync(object input);

        Task<JObject> UpdateCustomerAsync(object input, string customerId);

        Task<List<JObject>> GetSubscriptions();

        Task<T> GetSubscriptionsRetrieve<T>(string subscriptionId);
        Task<List<T>> GetSubscriptions<T>();

        Task<JObject> CancelSusbscriptionAsync<T>(string susbscriptionId);
    }
}
