using System;
using System.Collections.Generic;
using Zoho.Abstractions.Models;
using Zoho.Campaign.Models;
using Zoho.Shared.Models;
using Newtonsoft.Json;

namespace Zoho.Subscriptions.Models
{
    public class Subscription : Model
    {
        [JsonProperty("subscription_id")]
        public string SubscriptionId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subscription_number")]
        public string SubscriptionNumber { get; set; }

        [JsonProperty("is_metered_billing")]
        public bool? IsMeteredBilling { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("sub_total")]
        public double? SubTotal { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("activated_at")]
        public string ActivatedAt { get; set; }

        [JsonProperty("current_term_starts_at")]
        public string CurrentTermStartsAt { get; set; }

        [JsonProperty("current_term_ends_at")]
        public string CurrentTermEndsAt { get; set; }

        [JsonProperty("last_billing_at")]
        public string LastBillingAt { get; set; }

        [JsonProperty("next_billing_at")]
        public string NextBillingAt { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonProperty("interval")]
        public int? Interval { get; set; }

        [JsonProperty("interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty("shipping_interval")]
        public int? ShippingInterval { get; set; }

        [JsonProperty("shipping_interval_unit")]
        public string ShippingIntervalUnit { get; set; }

        [JsonProperty("billing_mode")]
        public string BillingMode { get; set; }

        [JsonProperty("next_shipment_at")]
        public string NextShipmentAt { get; set; }

        [JsonProperty("next_shipment_day")]
        public string NextShipmentDay { get; set; }

        [JsonProperty("total_orders")]
        public int? TotalOrders { get; set; }

        [JsonProperty("orders_created")]
        public int? OrdersCreated { get; set; }

        [JsonProperty("orders_remaining")]
        public int? OrdersRemaining { get; set; }

        [JsonProperty("last_shipment_at")]
        public string LastShipmentAt { get; set; }

        [JsonProperty("last_shipment_day")]
        public string LastShipmentDay { get; set; }

        [JsonProperty("auto_collect")]
        public bool? AutoCollect { get; set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonProperty("updated_time")]
        public DateTime? UpdatedTime { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("salesperson_id")]
        public string SalespersonId { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("coupon_duration")]
        public string CouponDuration { get; set; }

        [JsonProperty("end_of_term")]
        public bool? EndOfTerm { get; set; }

        [JsonProperty("remaining_billing_cycles")]
        public int? RemainingBillingCycles { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("pricebook_id")]
        public string PricebookId { get; set; }

        // [JsonProperty("items_associated")]
        // public List<object> ItemsAssociated { get; set; } 

        [JsonProperty("channel_source")]
        public string ChannelSource { get; set; }

        [JsonProperty("channel_reference_id")]
        public string ChannelReferenceId { get; set; }

        // [JsonProperty("addons")]
        // public List<object> Addons { get; set; } 
        //
        // [JsonProperty("taxes")]
        // public List<object> Taxes { get; set; } 

        [JsonProperty("exchange_rate")]
        public double? ExchangeRate { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool? IsInclusiveTax { get; set; }

        [JsonProperty("tax_rounding")]
        public string TaxRounding { get; set; }

        [JsonProperty("payment_terms")]
        public int? PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("can_add_bank_account")]
        public bool? CanAddBankAccount { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("last_modified_by_id")]
        public string LastModifiedById { get; set; }

        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("contactpersons")]
        public List<ContactPerson> Contactpersons { get; set; }

        [JsonProperty("notes")]
        public List<object> Notes { get; set; }

        [JsonProperty("allow_partial_payments")]
        public bool? AllowPartialPayments { get; set; }

        // [JsonProperty("auto_apply_credits")]
        // public List<object> AutoApplyCredits { get; set; } 

        [JsonProperty("payment_gateways")]
        public List<PaymentGateway> PaymentGateways { get; set; }

        [JsonRequired]
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonRequired]
        [JsonProperty("plan")]
        public Plan Plan { get; set; }

        public override bool Validate()
        {
            var resull = base.Validate();
            if (resull)
            {
                var subResult = Customer.Validate();
                var planResult = Plan.Validate();
                if (!subResult || !planResult)
                    return false;
            }

            return resull;
        }

        #region CRM

        [JsonProperty("crm_owner_id")]
        public string CrmOwnerId { get; set; }

        [JsonProperty("zcrm_potential_id")]
        public string CrmPotentialId { get; set; }

        [JsonProperty("zcrm_potential_name")]
        public string CrmPotentialName { get; set; }

        #endregion
    }
}