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
        Task<JObject> RequestPaymentMethod(string customerId);
        Task<List<JObject>> GetCards(string customerId);
        Task<List<JObject>> GetPlans();
        Task<List<JObject>> GetProducts();
        Task<List<JObject>> GetAddons();
    }
}