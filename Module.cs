using System.Collections.Generic;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Zoho
{
    public class Module
    {
        public Module()
        {
            Enabled = true;
        }

        public string Url { get; set; }

        public List<string> Scopes { get; set; }
        public Dictionary<string, string> Keys { get; set; }

        public bool Enabled { get; set; }
    }
}
