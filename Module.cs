using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Zoho
{
    public class Module
    {
        public string Url { get; set; }

        public List<string> Scopes { get; set; }
        public Dictionary<string, string> Keys { get; set; }
    }
}