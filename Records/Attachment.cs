#if NET6_0_OR_GREATER
using System;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records
{
    public record Attachment
    (
        Owner owner,
        string file_name,
        DateTime created_time,
        object parent_id,
        string id
    );
}
#endif
