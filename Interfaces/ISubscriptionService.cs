using System.Collections.Generic;
using System.Threading.Tasks;
using Zoho.Subscriptions.Models;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/subscriptions/api/v1/
    /// </summary>
    public interface ISubscriptionService : IZohoService
    {
        Task<JObject> CreateAsync(Subscription input);
        Task<JObject> AddChargeAsync(string subscriptionId, Charge input);
        Task<bool> SetCardCollect(string subscriptionId, Card input);
        Task<JObject> CreateAsync(Customer input);
        Task<JObject> RequestPaymentMethod(string customerId);
        Task<List<Card>> GetCards(string customerId);
        Task<List<Plan>> GetPlans();
    }
}