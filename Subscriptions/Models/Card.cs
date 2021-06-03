using System;
using Infrastructure.Enterprise.Abstractions.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Subscriptions.Models
{
  public class Card : Model
  {
    
    [JsonRequired]
    [JsonProperty("card_id")]
    public string CardId { get; set; }

    [JsonProperty("customer_id")]
    public string CustomerId { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("last_four_digits")]
    public string LastFourDigits { get; set; }

    [JsonProperty("expiry_month")]
    public int ExpiryMonth { get; set; }

    [JsonProperty("expiry_year")]
    public int ExpiryYear { get; set; }

    [JsonProperty("payment_gateway")]
    public string PaymentGateway { get; set; }

    [JsonProperty("created_time")]
    public DateTime CreatedTime { get; set; }

    [JsonProperty("updated_time")]
    public string UpdatedTime { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("customer_name")]
    public string CustomerName { get; set; }

    [JsonProperty("auto_collect")]
    public bool AutoCollect { get; set; }

  }
}