#if NET6_0_OR_GREATER
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record Attachtment(
        string attachment_id,
        string name,
        string type,
        string size,
        string third_party_file_id,
        string entity_id,
        string entity_type,
        string app_domain,
        string app_id,
        string app_name,
        string created_by,
        string created_time,
        string associated_by,
        string associated_by_name,
        string associated_time_long,
        string preview_url,
        string download_url,
        string permanent_url,
        string uploadedZpuid,
        string trashed
    );
}
#endif
