using System.Collections.Generic;

namespace Zoho
{
    public class Module
    {
        public string Url { get; set; }

        public List<string> Scopes { get; set; }
        public Dictionary<string, string> Keys { get; set; }
    }
}