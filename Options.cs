using System.Collections.Generic;
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Zoho
{
    public class Options
    {
        public string ClientId { get; set; }
        public string OrganizationId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public Dictionary<string, Module> Modules { get; set; }
        public bool Debug { get; set; }
    }
}
