using System.Collections.Generic;
using Infrastructure.Enterprise.Abstractions.Models;
using Infrastructure.Enterprise.Shared.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Subscriptions.Models
{
    public class Charge : Model
    {
        [JsonRequired]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("item_custom_fields")]
        public List<ItemCustomField> ItemCustomFields { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("add_to_unbilled_charges")]
        public bool AddToUnbilledCharges { get; set; }
    }
}