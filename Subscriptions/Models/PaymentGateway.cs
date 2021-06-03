using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Subscriptions.Models

{
    public class PaymentGateway
    {
        [JsonProperty("payment_gateway")]
        public string Name { get; set; }
    }
}