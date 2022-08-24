#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Records
{
    public record CustomerResponse(
        string customer_id,
        string display_name,
        string phone,
        string email,
        string status,
        DateTime created_time,
        DateTime updated_time
        
    );

}
#endif
