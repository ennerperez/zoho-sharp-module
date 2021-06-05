using Zoho.Abstractions.Models;
using Newtonsoft.Json;

namespace Zoho.Subscriptions.Models
{
    public class Payment : Model
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("payment_mode")]
        public string PaymentMode { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("invoice_payment_id")]
        public string InvoicePaymentId { get; set; }

        [JsonProperty("amount_refunded")]
        public double AmountRefunded { get; set; }

        [JsonProperty("gateway_transaction_id")]
        public string GatewayTransactionId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("bank_charges")]
        public double BankCharges { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("last_four_digits")]
        public string LastFourDigits { get; set; }

        [JsonProperty("settlement_status")]
        public string SettlementStatus { get; set; }
    }
}