using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;
using Zoho.Models;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly Factory _factory;

        public CampaignService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<JObject> CreateContactAsync(JObject input, string campaignKey)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Campaigns", "addlistsubscribersinbulk", input);
        }

        public async Task<List<JObject>> GetListSubscribersAsync(string designation)
        {
            if (string.IsNullOrWhiteSpace(designation))
            {
                throw new ArgumentNullException("designation");
            }

            var client = await _factory.CreateAsync();

            var listKey = client.GetOption("Campaigns", designation);
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
            var response = await client.InvokeGetAsync<List<JObject>>("Campaigns", endpoint);

            return response;
        }

        public async Task<JObject> GetSubscriberAsync(string designation, string email)
        {
            if (string.IsNullOrWhiteSpace(designation))
            {
                throw new ArgumentNullException("designation");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            var client = await _factory.CreateAsync();

            var listKey = client.GetOption("Campaigns", designation);
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
            
            var response = await client.InvokeGetAsync<ListOfDetails<JObject>>("Campaigns", endpoint);

            return response.Items.FirstOrDefault(m => m.Value<string>("contact_email")?.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Campaigns", key);
        }
    }
}
