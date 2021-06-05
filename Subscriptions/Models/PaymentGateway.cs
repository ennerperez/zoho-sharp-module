using Newtonsoft.Json;

namespace Zoho.Subscriptions.Models

{
    public class PaymentGateway
    {
        [JsonProperty("payment_gateway")]
        public string Name { get; set; }
    }
}