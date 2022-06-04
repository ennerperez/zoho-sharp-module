#if NET6_0_OR_GREATER
using System;

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
        DateTime? updated_time);
}
#endif
