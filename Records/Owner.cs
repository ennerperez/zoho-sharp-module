#if NET6_0_OR_GREATER
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Records
{
    public record Owner
    (
        string name,
        string id,
        string email
    );
}
#endif
