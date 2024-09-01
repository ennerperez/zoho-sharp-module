#if NET6_0_OR_GREATER
using System;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record Task(
        string id,
        string name,
        DateTime start_date_format,
        DateTime end_date_format,
        CustomField[] custom_fields,
        Status status,
        bool completed,
        string priority,
        string description,
        bool subtasks,
        Attachtment[] attachments
    );
}
#endif
