#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record PlanCoupon(string plan_code, string Name);
    public record AddonCoupon(string AddonCode, string Name);

    public record Coupon(
        string CouponCode,
        string Name,
        string Description,
        string Type,
        int Duration,
        string Status,
        string DiscountBy,
        int DiscountValue,
        string ProductId,
        int MaxRedemption,
        int RedemptionCount,
        DateTime ExpiryAt,
        string ApplyToPlans,
        List<PlanCoupon> Plans,
        string ApplyToAddons,
        List<AddonCoupon> Addons,
        DateTime CreatedTime,
        DateTime UpdatedTime
    );
}
#endif
