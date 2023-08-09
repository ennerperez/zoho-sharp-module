#if NET6_0_OR_GREATER
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record TaskCount(
        int closed,
        int open
    );
}

#endif
