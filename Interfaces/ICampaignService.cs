using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/campaigns/help/developers/
    /// </summary>
    public interface ICampaignService : IZohoService
    {
        Task<JObject> CreateContactAsync(JObject input, string campaignKey);
        Task<List<JObject>> GetListSubscribersAsync(string designation);
        Task<JObject> GetSubscriberAsync(string designation, string email);
    }
}