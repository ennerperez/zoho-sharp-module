using System.Collections.Generic;
using System.Threading.Tasks;
using Zoho.Campaign.Models;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/campaigns/help/developers/
    /// </summary>
    public interface ICampaignService : IZohoService
    {
        Task<JObject> CreateContactAsync(ContactPerson input, string campaignKey);
        Task<List<Subscriber>> GetListSubscribersAsync(string designation);
        Task<Subscriber> GetSubscriberAsync(string designation, string email);
    }
}