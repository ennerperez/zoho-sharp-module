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
        Owner Owner,
        string File_Name,
        DateTime Created_Time,
        object Parent_Id,
        string id
    );
}
#endif
