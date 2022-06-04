using Newtonsoft.Json;

namespace Zoho.Abstractions.Models
{
    public class PageContext
    {
        [JsonProperty("page")] 
        public int Page { get; set; }
        [JsonProperty("per_page")] 
        public int PerPage { get; set; }
        [JsonProperty("has_more_page")] 
        public bool HasMorePage { get; set; }
        [JsonProperty("applied_filter")] 
        public string AppliedFilter { get; set; }
        [JsonProperty("sort_column")] 
        public string AortColumn { get; set; }
        [JsonProperty("sort_order")] 
        public string SortOrder { get; set; }
    }
}
