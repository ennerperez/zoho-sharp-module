using System.Collections.Generic;
using Infrastructure.Enterprise.Subscriptions.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Books.Models
{
    public class PaymentOptions
    {
        [JsonProperty("payment_gateways")]
        public List<PaymentGateway> PaymentGateways { get; set; }
    }
}