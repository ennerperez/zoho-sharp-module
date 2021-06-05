using System.Collections.Generic;
using Zoho.Subscriptions.Models;
using Newtonsoft.Json;

namespace Zoho.Books.Models
{
    public class PaymentOptions
    {
        [JsonProperty("payment_gateways")]
        public List<PaymentGateway> PaymentGateways { get; set; }
    }
}