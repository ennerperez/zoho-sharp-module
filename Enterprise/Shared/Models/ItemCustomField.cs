using Infrastructure.Enterprise.Abstractions.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Shared.Models
{
  public class ItemCustomField : Model
  {
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("account_id")]
    public string AccountId { get; set; }

    [JsonProperty("add_to_unbilled_charges")]
    public bool AddToUnbilledCharges { get; set; }

  }
}