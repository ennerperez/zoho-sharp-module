using Infrastructure.Enterprise.Abstractions.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Shared.Models
{
    public class Tag : Model
    {
        [JsonProperty("tag_id")]
        public int TagId { get; set; }

        [JsonProperty("tag_option_id")]
        public int TagOptionId { get; set; }
    }
}