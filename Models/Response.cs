using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
namespace Zoho.Models
{
    public class Response : Response<int>
    {
        
    }
    public class Response<TCode>
    {
        [JsonProperty("code")]
        public TCode Code { get; set; }

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
        
        [JsonProperty("details")]
        public ResponseDetails Details { get; set; }
    }

    public class ResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ListOfDetails<T> : Response
    {
        [JsonProperty("list_of_details")]
        public IEnumerable<T> Items { get; set; }

        [JsonProperty("requestdetails")]
        public RequestDetails Details { get; set; }
    }

    public class RequestDetails
    {
        [JsonProperty("fromindex")]
        public int From { get; set; }

        [JsonProperty("range")]
        public int Range { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
