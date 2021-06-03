using System.Collections.Generic;
using Infrastructure.Enterprise.Abstractions.Models;
using Infrastructure.Enterprise.Shared.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Books.Models
{
  public class Bill : Model
  {
    [JsonProperty("vendor_id")]
    public string VendorId { get; set; }

    [JsonProperty("is_update_customer")]
    public bool IsUpdateCustomer { get; set; }

    [JsonProperty("purchaseorder_ids")]
    public List<object> PurchaseorderIds { get; set; }

    [JsonProperty("bill_number")]
    public string BillNumber { get; set; }

    // [JsonProperty("documents")]
    // public List<Document> Documents { get; set; }

    [JsonProperty("source_of_supply")]
    public string SourceOfSupply { get; set; }

    [JsonProperty("destination_of_supply")]
    public string DestinationOfSupply { get; set; }

    [JsonProperty("place_of_supply")]
    public string PlaceOfSupply { get; set; }

    [JsonProperty("gst_treatment")]
    public string GstTreatment { get; set; }

    [JsonProperty("tax_treatment")]
    public string TaxTreatment { get; set; }

    [JsonProperty("gst_no")]
    public string GstNo { get; set; }

    [JsonProperty("pricebook_id")]
    public long PricebookId { get; set; }

    [JsonProperty("reference_number")]
    public string ReferenceNumber { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("due_date")]
    public string DueDate { get; set; }

    [JsonProperty("payment_terms")]
    public int PaymentTerms { get; set; }

    [JsonProperty("payment_terms_label")]
    public string PaymentTermsLabel { get; set; }

    [JsonProperty("exchange_rate")]
    public double ExchangeRate { get; set; }

    [JsonProperty("is_item_level_tax_calc")]
    public bool IsItemLevelTaxCalc { get; set; }

    [JsonProperty("is_inclusive_tax")]
    public bool IsInclusiveTax { get; set; }

    [JsonProperty("adjustment")]
    public int Adjustment { get; set; }

    [JsonProperty("adjustment_description")]
    public string AdjustmentDescription { get; set; }

    [JsonProperty("custom_fields")]
    public List<CustomField> CustomFields { get; set; }

    [JsonProperty("line_items")]
    public List<LineItem> LineItems { get; set; }

    [JsonProperty("taxes")]
    public List<Tax> Taxes { get; set; }

    [JsonProperty("notes")]
    public string Notes { get; set; }

    [JsonProperty("terms")]
    public string Terms { get; set; }

    // [JsonProperty("approvers")]
    // public List<Approver> Approvers { get; set; }
    
    public class LineItem
    {
      [JsonProperty("line_item_id")]
      public string LineItemId { get; set; }

      [JsonProperty("item_id")]
      public string ItemId { get; set; }

      [JsonProperty("account_id")]
      public string AccountId { get; set; }

      [JsonProperty("rate")]
      public int Rate { get; set; }

      [JsonProperty("hsn_or_sac")]
      public int HsnOrSac { get; set; }

      [JsonProperty("reverse_charge_tax_id")]
      public long ReverseChargeTaxId { get; set; }

      [JsonProperty("warehouse_id")]
      public long WarehouseId { get; set; }

      [JsonProperty("quantity")]
      public int Quantity { get; set; }

      [JsonProperty("tax_id")]
      public string TaxId { get; set; }

      [JsonProperty("tax_treatment_code")]
      public string TaxTreatmentCode { get; set; }

      [JsonProperty("item_order")]
      public int ItemOrder { get; set; }

      [JsonProperty("unit")]
      public string Unit { get; set; }

      [JsonProperty("tags")]
      public List<Tag> Tags { get; set; }

      [JsonProperty("is_billable")]
      public bool IsBillable { get; set; }

      [JsonProperty("item_custom_fields")]
      public List<ItemCustomField> ItemCustomFields { get; set; }
    }

    
  }
}