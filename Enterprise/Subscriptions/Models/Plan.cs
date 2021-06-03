using System;
using System.Collections.Generic;
using Infrastructure.Enterprise.Abstractions.Models;
using Infrastructure.Enterprise.Shared.Models;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Subscriptions.Models

{
  public class Plan : Model
  {
    [JsonRequired]
    [JsonProperty("plan_code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("billing_mode")]
    public string BillingMode { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("product_id")]
    public string ProductId { get; set; }

    [JsonProperty("tax_id")]
    public string TaxId { get; set; }

    [JsonProperty("tax_name")]
    public string TaxName { get; set; }

    [JsonProperty("tax_percentage")]
    public int? TaxPercentage { get; set; }

    [JsonProperty("tax_type")]
    public string TaxType { get; set; }

    [JsonProperty("trial_period")]
    public int? TrialPeriod { get; set; }

    [JsonProperty("setup_fee")]
    public double? SetupFee { get; set; }

    [JsonProperty("setup_fee_account_id")]
    public string SetupFeeAccountId { get; set; }

    [JsonProperty("setup_fee_account_name")]
    public string SetupFeeAccountName { get; set; }

    [JsonProperty("account_id")]
    public string AccountId { get; set; }

    [JsonProperty("account")]
    public string Account { get; set; }

    [JsonProperty("recurring_price")]
    public double? RecurringPrice { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("interval")]
    public int? Interval { get; set; }

    [JsonProperty("interval_unit")]
    public string IntervalUnit { get; set; }

    [JsonProperty("billing_cycles")]
    public int? BillingCycles { get; set; }

    [JsonProperty("product_type")]
    public string ProductType { get; set; }

    [JsonProperty("show_in_widget")]
    public bool? ShowInWidget { get; set; }

    [JsonProperty("store_description")]
    public string StoreDescription { get; set; }

    [JsonProperty("store_markup_description")]
    public string StoreMarkupDescription { get; set; }

    [JsonProperty("created_time")]
    public DateTime? CreatedTime { get; set; }

    [JsonProperty("created_time_formatted")]
    public string CreatedTimeFormatted { get; set; }

    [JsonProperty("updated_time")]
    public DateTime? UpdatedTime { get; set; }

    [JsonProperty("updated_time_formatted")]
    public string UpdatedTimeFormatted { get; set; }

    // [JsonProperty("addons")]
    // public List<object> Addons { get; set; } 

    [JsonProperty("custom_fields")]
    public List<CustomField> CustomFields { get; set; }

  }
}