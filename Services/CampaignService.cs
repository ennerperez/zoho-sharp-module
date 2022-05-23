using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;
using Zoho.Abstractions.Models;

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
            if (input == null) throw new ArgumentNullException("input");

            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Campaigns", "addlistsubscribersinbulk", input);

            // var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            // var content = new StringContent(data, Encoding.UTF8, "application/json");
            //
            // var response = await _httpClient.PostAsync("addlistsubscribersinbulk", content);
            // var processResult = await ProcessResponse<JObject>(response);
            //
            // if (null != processResult.Error) throw processResult.Error;
            //
            // return processResult.Data;
        }

        public async Task<List<JObject>> GetListSubscribersAsync(string designation)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");

            var client = await _factory.CreateAsync();

            var listKey = client.GetOption("Campaigns", designation);
            //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
            //var response = await _httpClient.GetAsync(endpoint);

            var response = await client.InvokeGetAsync<List<JObject>>("Campaigns", endpoint);

            return response;
        }

        public async Task<JObject> GetSubscriberAsync(string designation, string email)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            var client = await _factory.CreateAsync();

            var listKey = client.GetOption("Campaigns", designation);
            //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
            var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";

            var response = await client.InvokeGetAsync<Response<JObject[]>>("Campaigns", endpoint);

            // var response = await _httpClient.GetAsync(endpoint);
            // var processResult = await ProcessResponse<Subscriber[]>(response, "list_of_details");
            //
            // if (null != processResult.Error) throw processResult.Error;
            //
            return response.Object.FirstOrDefault(m => m.Value<string>("ContactEmail")?.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Campaigns", key);
        }
    }
}