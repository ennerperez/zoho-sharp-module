﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Enterprise.Abstractions.Interfaces;
using Infrastructure.Enterprise.Subscriptions.Models;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Enterprise.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/subscriptions/api/v1/
    /// </summary>
    public interface ISubscriptionService : IEnterpriseService
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