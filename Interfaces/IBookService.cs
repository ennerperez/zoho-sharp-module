﻿using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/books/api/v3/
    /// </summary>
    public interface IBookService : IZohoService
    {
        Task<JObject> CreateBillAsync(JObject input);
        Task<JObject> CreateInvoiceAsync(JObject input);
    }
}
