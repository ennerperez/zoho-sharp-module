using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Campaign.Models
{
    public class ContactPerson
    {
        [JsonProperty("contactperson_id")]
        public string ContactPersonId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        #region CRM

        [JsonProperty("zcrm_contact_id")]
        public string CrmContactId { get; set; }

        #endregion
    }
}