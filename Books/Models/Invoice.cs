using System.Collections.Generic;
using Zoho.Abstractions.Models;
using Zoho.Shared.Models;
using Newtonsoft.Json;

namespace Zoho.Books.Models
{
    public class Invoice : Model
    {
        [JsonRequired]
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("contact_persons")]
        public List<string> ContactPersons { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("place_of_supply")]
        public string PlaceOfSupply { get; set; }

        [JsonProperty("tax_treatment")]
        public string TaxTreatment { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("gst_no")]
        public string GstNo { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("template_id")]
        public long TemplateId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("is_discount_before_tax")]
        public bool IsDiscountBeforeTax { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("recurring_invoice_id")]
        public string RecurringInvoiceId { get; set; }

        [JsonProperty("invoiced_estimate_id")]
        public string InvoicedEstimateId { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("payment_options")]
        public PaymentOptions PaymentOptions { get; set; }

        [JsonProperty("allow_partial_payments")]
        public bool AllowPartialPayments { get; set; }

        [JsonProperty("custom_body")]
        public string CustomBody { get; set; }

        [JsonProperty("custom_subject")]
        public string CustomSubject { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("shipping_charge")]
        public int ShippingCharge { get; set; }

        [JsonProperty("adjustment")]
        public int Adjustment { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("tax_authority_id")]
        public long TaxAuthorityId { get; set; }

        [JsonProperty("tax_exemption_id")]
        public long TaxExemptionId { get; set; }

        [JsonProperty("tax_id")]
        public long TaxId { get; set; }

        [JsonProperty("expense_id")]
        public string ExpenseId { get; set; }

        [JsonProperty("salesorder_item_id")]
        public string SalesorderItemId { get; set; }

        // [JsonProperty("time_entry_ids")]
        // public List<object> TimeEntryIds { get; set; }

        public class LineItem
        {
            [JsonProperty("item_id")]
            public long ItemId { get; set; }

            [JsonProperty("project_id")]
            public long ProjectId { get; set; }

            [JsonProperty("time_entry_ids")]
            public List<object> TimeEntryIds { get; set; }

            [JsonProperty("product_type")]
            public string ProductType { get; set; }

            [JsonProperty("hsn_or_sac")]
            public int HsnOrSac { get; set; }

            [JsonProperty("warehouse_id")]
            public string WarehouseId { get; set; }

            [JsonProperty("expense_id")]
            public string ExpenseId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("item_order")]
            public int ItemOrder { get; set; }

            [JsonProperty("bcy_rate")]
            public int BcyRate { get; set; }

            [JsonProperty("rate")]
            public int Rate { get; set; }

            [JsonProperty("quantity")]
            public int Quantity { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }

            [JsonProperty("discount_amount")]
            public int DiscountAmount { get; set; }

            [JsonProperty("discount")]
            public int Discount { get; set; }

            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }

            [JsonProperty("tax_id")]
            public long TaxId { get; set; }

            [JsonProperty("tax_name")]
            public string TaxName { get; set; }

            [JsonProperty("tax_type")]
            public string TaxType { get; set; }

            [JsonProperty("tax_percentage")]
            public double TaxPercentage { get; set; }

            [JsonProperty("tax_treatment_code")]
            public string TaxTreatmentCode { get; set; }

            [JsonProperty("header_name")]
            public string HeaderName { get; set; }
        }
    }
}