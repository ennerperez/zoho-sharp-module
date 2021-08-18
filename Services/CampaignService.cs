using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zoho.Campaign.Models;
using Zoho.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zoho.Services
{
    public class CampaignService : ZohoService, ICampaignService
    {
        public CampaignService(HttpClient httpClient, ILoggerFactory loggerFactory) : base(httpClient, loggerFactory)
        {
        }

        public async Task<JObject> CreateContactAsync(ContactPerson input, string campaignKey)
        {
            if (input == null) throw new ArgumentNullException("input");

            await GetTokenAsync();
            SetHttpClient("Campaign");

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("addlistsubscribersinbulk", content);
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data;
        }

        public async Task<List<Subscriber>> GetListSubscribersAsync(string designation)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");

            await GetTokenAsync();
            SetHttpClient("Campaigns");

            var listKey = GetOption(designation);
            //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
            var response = await _httpClient.GetAsync(endpoint);
            var processResult = await ProcessResponse<Subscriber[]>(response);

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data.ToList();
        }

        public async Task<Subscriber> GetSubscriberAsync(string designation, string email)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");

            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            await GetTokenAsync();
            SetHttpClient("Campaigns");

            var listKey = GetOption(designation);
            //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
            var response = await _httpClient.GetAsync(endpoint);
            var processResult = await ProcessResponse<Subscriber[]>(response, "list_of_details");

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data.FirstOrDefault(m => m.ContactEmail.ToLower().Trim() == email.ToLower().Trim());
        }

        public string GetOption(string key)
        {
            return base.GetOption("Campaigns", key);
        }
    }
}