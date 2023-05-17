#if NET6_0_OR_GREATER
using System;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record Product(
        string product_id,
        string name,
        string description,
        string status,
        string redirect_url,
        string emails_ids,
        int plan_count,
        int addons_count,
        DateTime created_time,
        DateTime? updated_time
    );
}
#endif
