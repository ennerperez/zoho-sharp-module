using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Enterprise.Abstractions.Services;
using Infrastructure.Enterprise.Campaign.Models;
using Infrastructure.Enterprise.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Enterprise.Services
{
    public class CampaignService : EnterpriseService, ICampaignService
    {
        public CampaignService(ILoggerFactory loggerFactory, IOptionsMonitor<Options> optionsMonitor, IConfiguration configuration) : base(loggerFactory, optionsMonitor, configuration)
        {
        }

        public async Task<JObject> CreateContactAsync(ContactPerson input, string campaignKey)
        {
            if (input == null) throw new ArgumentNullException("input");

            await RefreshTokenAsync();
            using (var httpClient = GetHttpClient("Campaign"))
            {
                var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("addlistsubscribersinbulk", content);
                var processResult = await ProcessResponse<JObject>(response);

                if (null != processResult.Error) throw processResult.Error;

                return processResult.Data;
            }
        }

        public async Task<List<Subscriber>> GetListSubscribersAsync(string designation)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");

            await RefreshTokenAsync();
            using (var httpClient = GetHttpClient("Campaigns"))
            {
                var listKey = GetOption(designation);
                //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
                var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
                var response = await httpClient.GetAsync(endpoint);
                var processResult = await ProcessResponse<Subscriber[]>(response);

                if (null != processResult.Error) throw processResult.Error;

                return processResult.Data.ToList();
            }
        }

        public async Task<Subscriber> GetSubscriberAsync(string designation, string email)
        {
            if (string.IsNullOrWhiteSpace(designation)) throw new ArgumentNullException("designation");

            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            await RefreshTokenAsync();
            using (var httpClient = GetHttpClient("Campaigns"))
            {
                var listKey = GetOption(designation);
                //getlistsubscribers?resfmt=XML&listkey=[listkey]&sort=[asc/desc]&fromindex=[number]&range=[number]&status=[active/recent/mostrecent/unsub/bounce]
                var endpoint = $"getlistsubscribers?resfmt=JSON&listkey={listKey}&status=active";
                var response = await httpClient.GetAsync(endpoint);
                var processResult = await ProcessResponse<Subscriber[]>(response, "list_of_details");

                if (null != processResult.Error) throw processResult.Error;

                return processResult.Data.FirstOrDefault(m => m.ContactEmail.ToLower().Trim() == email.ToLower().Trim());
            }
        }

        public string GetOption(string key)
        {
            return base.GetOption("Campaigns", key);
        }
    }
}