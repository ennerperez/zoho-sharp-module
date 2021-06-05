using Newtonsoft.Json;

namespace Zoho.Shared.Models
{
    public sealed class CustomField
    {
        [JsonProperty("customfield_id")]
        public string CustomFieldId { get; set; }

        [JsonProperty("index")]
        public int? Index { get; set; }

        [JsonProperty("value_formatted")]
        public string ValueFormatted { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("placeholder")]
        public string PlaceHolder { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }
    }
}