#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record PlanCoupon(string plan_code, string name);
    public record AddonCoupon(string addon_code, string name);

    public record Coupon(
        string coupon_id,
        string coupon_code,
        string name,
        string product_name,
        string description,
        bool is_active,
        string type,
        int? duration,
        string status,
        string discount_by,
        int? discount_value,
        string product_id,
        int? max_redemption,
        int? max_redemption_count,
        DateTime? expiry_at,
        string apply_to_plans,
        List<PlanCoupon> plans,
        string apply_to_addons,
        List<AddonCoupon> addons,
        DateTime? created_time,
        DateTime? updated_time
    );
}
#endif
