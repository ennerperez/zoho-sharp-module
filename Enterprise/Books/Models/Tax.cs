using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Books.Models
{
    public class Tax
    {
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_amount")]
        public double TaxAmount { get; set; }
    }
}