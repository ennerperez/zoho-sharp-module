#if NET6_0_OR_GREATER
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record Task(
        string id,
        string name,
        CustomField[] custom_fields,
        Status status,
        bool completed,
        string priority,
        string description,
        bool subtasks
    );
}
#endif
