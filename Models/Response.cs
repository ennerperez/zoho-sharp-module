using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Models
{
    public class Response
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("page_context")]
        public PageContext PageContext { get; set; }
        
    }

    public class Response<T> : Response
    {
        public T Object { get; set; }
        
    }
    
}
