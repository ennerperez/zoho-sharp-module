using System;
using System.Collections.Generic;
using Zoho.Abstractions.Models;
using Zoho.Campaign.Models;
using Zoho.Shared.Models;
using Newtonsoft.Json;

namespace Zoho.Subscriptions.Models
{
    public class Invoice : Model
    {
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("unbilled_charges_id")]
        public string UnbilledChargesId { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("invoice_date")]
        public string InvoiceDate { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("zcrm_potential_id")]
        public string ZcrmPotentialId { get; set; }

        [JsonProperty("payment_expected_date")]
        public string PaymentExpectedDate { get; set; }

        [JsonProperty("can_send_invoice_sms")]
        public bool CanSendInvoiceSms { get; set; }

        [JsonProperty("stop_reminder_until_payment_expected_date")]
        public bool StopReminderUntilPaymentExpectedDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("can_edit_items")]
        public bool CanEditItems { get; set; }

        [JsonProperty("can_skip_payment_info")]
        public bool CanSkipPaymentInfo { get; set; }

        [JsonProperty("inprocess_transaction_present")]
        public bool InprocessTransactionPresent { get; set; }

        [JsonProperty("ach_payment_initiated")]
        public bool AchPaymentInitiated { get; set; }

        [JsonProperty("allow_partial_payments")]
        public bool AllowPartialPayments { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        // [JsonProperty("customer_custom_fields")]
        // public List<CustomerCustomField> CustomerCustomFields { get; set; }

        [JsonProperty("cf_userid")]
        public string CfUserid { get; set; }

        [JsonProperty("cf_userid_unformatted")]
        public string CfUseridUnformatted { get; set; }

        // [JsonProperty("customer_custom_field_hash")]
        // public CustomerCustomFieldHash CustomerCustomFieldHash { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("pricebook_id")]
        public string PricebookId { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("tax_rounding")]
        public string TaxRounding { get; set; }

        [JsonProperty("is_viewed_by_client")]
        public bool IsViewedByClient { get; set; }

        [JsonProperty("client_viewed_time")]
        public string ClientViewedTime { get; set; }

        [JsonProperty("invoice_items")]
        public List<InvoiceItem> InvoiceItems { get; set; }

        [JsonProperty("taxes")]
        public List<object> Taxes { get; set; }

        [JsonProperty("shipping_charge")]
        public double ShippingCharge { get; set; }

        [JsonProperty("adjustment")]
        public double Adjustment { get; set; }

        [JsonProperty("sub_total")]
        public double SubTotal { get; set; }

        [JsonProperty("tax_total")]
        public double TaxTotal { get; set; }

        [JsonProperty("discount_total")]
        public double DiscountTotal { get; set; }

        [JsonProperty("payment_reminder_enabled")]
        public bool PaymentReminderEnabled { get; set; }

        [JsonProperty("auto_reminders_configured")]
        public bool AutoRemindersConfigured { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("discount_percent")]
        public double DiscountPercent { get; set; }

        [JsonProperty("bcy_shipping_charge")]
        public double BcyShippingCharge { get; set; }

        [JsonProperty("bcy_adjustment")]
        public double BcyAdjustment { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("bcy_sub_total")]
        public double BcySubTotal { get; set; }

        [JsonProperty("bcy_discount_total")]
        public double BcyDiscountTotal { get; set; }

        [JsonProperty("bcy_tax_total")]
        public double BcyTaxTotal { get; set; }

        [JsonProperty("bcy_total")]
        public double BcyTotal { get; set; }

        [JsonProperty("payment_made")]
        public double PaymentMade { get; set; }

        [JsonProperty("coupons")]
        public List<object> Coupons { get; set; }

        [JsonProperty("unused_credits_receivable_amount")]
        public double UnusedCreditsReceivableAmount { get; set; }

        [JsonProperty("credits_applied")]
        public double CreditsApplied { get; set; }

        [JsonProperty("credits")]
        public List<object> Credits { get; set; }

        [JsonProperty("price_precision")]
        public int PricePrecision { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("write_off_amount")]
        public double WriteOffAmount { get; set; }

        [JsonProperty("payments")]
        public List<Payment> Payments { get; set; }

        [JsonProperty("salesperson_id")]
        public string SalespersonId { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("submitter_id")]
        public string SubmitterId { get; set; }

        [JsonProperty("approver_id")]
        public string ApproverId { get; set; }

        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }

        // [JsonProperty("custom_field_hash")]
        // public CustomFieldHash CustomFieldHash { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }

        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("last_modified_by_id")]
        public string LastModifiedById { get; set; }

        [JsonProperty("invoice_url")]
        public string InvoiceUrl { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; set; }

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonProperty("subscriptions")]
        public List<Subscription> Subscriptions { get; set; }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("page_width")]
        public string PageWidth { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("template_name")]
        public string TemplateName { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("contactpersons")]
        public List<ContactPerson> ContactPersons { get; set; }

        [JsonProperty("payment_gateways")]
        public List<PaymentGateway> PaymentGateways { get; set; }

        [JsonProperty("documents")]
        public List<object> Documents { get; set; }

        [JsonProperty("can_send_in_mail")]
        public bool CanSendInMail { get; set; }

        public class InvoiceItem : Model
        {
            [JsonProperty("item_id")]
            public string ItemId { get; set; }

            [JsonProperty("product_id")]
            public string ProductId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("account_id")]
            public string AccountId { get; set; }

            [JsonProperty("account_name")]
            public string AccountName { get; set; }

            [JsonProperty("price")]
            public double Price { get; set; }

            [JsonProperty("quantity")]
            public int Quantity { get; set; }

            [JsonProperty("discount_amount")]
            public double DiscountAmount { get; set; }

            [JsonProperty("item_total")]
            public double ItemTotal { get; set; }

            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }

            [JsonProperty("tax_name")]
            public string TaxName { get; set; }

            [JsonProperty("tax_type")]
            public string TaxType { get; set; }

            [JsonProperty("tax_percentage")]
            public int TaxPercentage { get; set; }

            [JsonProperty("tax_id")]
            public string TaxId { get; set; }

            [JsonProperty("item_custom_fields")]
            public List<ItemCustomField> ItemCustomFields { get; set; }
        }
    }
}