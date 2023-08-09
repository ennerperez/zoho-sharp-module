#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record Subscription(
        string customer_id,
        string created_by,
        string customer_name,
        string email,
        decimal amount,
        decimal sub_total,
        string status,
        string interval_unit,
        DateTime? next_billing_at,
        DateTime? last_billing_at,
        string plan_code,
        string plan_name,
        string subscription_id,
        string subscription_number,
        DateTime activated_at,
        DateTime created_time,
        DateTime? updated_time,
        Plan plan,
        Addon[] addons
    );
}
#endif
