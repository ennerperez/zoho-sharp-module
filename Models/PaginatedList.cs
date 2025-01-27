using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Zoho.Interfaces;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable once CheckNamespace
namespace Zoho.Models
{
    public class PaginatedList<T> : IResponseEntity<List<T>>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("page_context")]
        public PageContext PageContext { get; set; }

        [JsonIgnore]
        public List<T> Data { get; set; }

        [JsonIgnore]
        public Exception Error { get; set; }
    }
}
