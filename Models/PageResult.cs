using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zoho.Models
{
    public class PageResult<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
        [JsonProperty("info")]
        public PageInfo Info { get; set; }
        
    }
    public class PageInfo
    {
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        [JsonProperty("next_page_token")]
        public object NextPageToken { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("previous_page_token")]
        public object PreviousPageToken { get; set; }
        [JsonProperty("page_token_expiry")]
        public object PageTokenExpiry { get; set; }
        [JsonProperty("more_record")]
        public bool MoreRecord { get; set; }
    }
}
