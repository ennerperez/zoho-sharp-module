#if NET6_0_OR_GREATER
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record Comments(
        string id,
        string content,
        string added_via,
        string id_string,
        string created_time
    );
}
#endif
