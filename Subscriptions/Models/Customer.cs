using System.Collections.Generic;
using Infrastructure.Enterprise.Abstractions.Models;
using Infrastructure.Enterprise.Shared.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Subscriptions.Models
{
    public class Customer : Model
    {
        [JsonRequired]
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("salutation")]
        public string Salutation { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonRequired]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; set; }

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonProperty("payment_terms")]
        public int? PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("ach_supported")]
        public bool? IsAchSupported { get; set; }

        [JsonProperty("twitter")]
        public bool? Twitter { get; set; }

        [JsonProperty("facebook")]
        public bool? Facebook { get; set; }

        [JsonProperty("skype")]
        public bool? Skype { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("channel_reference_id")]
        public string ChannelReferenceId { get; set; }

        [JsonProperty("channel_customer_id")]
        public string ChannelCustomerId { get; set; }

        #region CRM

        [JsonProperty("zcrm_account_id")]
        public string CrmAccountId { get; set; }

        [JsonProperty("zcrm_contact_id")]
        public string CrmContactId { get; set; }

        #endregion
    }
}