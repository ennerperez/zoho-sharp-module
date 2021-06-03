using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Shared.Models
{
    public class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("attention")]
        public string Attention { get; set; }
    }

    public class BillingAddress : Address
    {
    }

    public class ShippingAddress : Address
    {
    }
}