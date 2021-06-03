using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Campaign.Models
{
    public class Subscriber
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("added_time")]
        public string AddedTime { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("companyname")]
        public string CompanyName { get; set; }

        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("zuid")]
        public string Zuid { get; set; }
    }
}