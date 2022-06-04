#if NET6_0_OR_GREATER
using System;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Records
{
    public record Addon(
        string name,
        string addon_code,
        string addon_id,
        string description,
        string status,
        string unit_name,
        string product_id,
        string tax_id,
        string tax_name,
        decimal tax_percentage,
        string tax_type,
        bool applicable_to_all_plans,
        string product_type,
        bool show_in_widget,
        string store_description,
        string store_markup_description,
        string type,
        string interval_unit,
        DateTime created_time,
        DateTime? updated_time,
        string pricing_scheme,
        Plan[] plans,
        Brackets[] price_brackets
    );
}
#endif
