#if NET6_0_OR_GREATER
using System;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record Customer(
        string customer_id,
        string display_name,
        string salutation,
        string first_name,
        string last_name,
        string email,
        string phone,
        string mobile,
        string website,
        string status,
        DateTime created_time,
        DateTime updated_time,
        CustomField[] custom_fields
    );
}
#endif
