#if NET6_0_OR_GREATER
using System;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Records
{
    public record Plan(
        string plan_code,
        string plan_id,
        string name,
        string billing_mode,
        string description,
        string status,
        string product_id,
        string tax_id,
        string tax_name,
        decimal tax_percentage,
        string tax_type,
        int trial_period,
        decimal setup_fee,
        string setup_fee_account_id,
        string setup_fee_account_name,
        string account_id,
        string account,
        decimal recurring_price,
        string unit,
        int interval,
        string interval_unit,
        int billing_cycles,
        string product_type,
        bool show_in_widget,
        string store_description,
        string store_markup_description,
        string url,
        int shipping_interval,
        string shipping_interval_unit,
        DateTime created_time,
        string created_time_formatted,
        DateTime? updated_time,
        string updated_time_formatted,
        Addon[] addons,
        CustomFields[] custom_fields
    );
}
#endif
