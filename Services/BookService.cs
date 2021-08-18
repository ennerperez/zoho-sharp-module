using System.Net.Http;
using System.Threading.Tasks;
using Zoho.Books.Models;
using Zoho.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Zoho.Services
{
    public class BookService : ZohoService, IBookService
    {
        public BookService(HttpClient httpClient, ILoggerFactory loggerFactory) : base(httpClient, loggerFactory)
        {
        }

        public async Task<JObject> CreateBillAsync(Bill input)
        {
            return await InvokePostAsync("Books", "bills", input);
        }

        public async Task<JObject> CreateInvoiceAsync(Invoice input)
        {
            return await InvokePostAsync("Books", "invoices", input);
        }

        public string GetOption(string key)
        {
            return base.GetOption("Books", key);
        }
    }
}