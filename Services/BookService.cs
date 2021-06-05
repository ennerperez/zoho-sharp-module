using System.Threading.Tasks;
using Zoho.Abstractions.Services;
using Zoho.Books.Models;
using Zoho.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Zoho.Services
{
    public class BookService : EnterpriseService, IBookService
    {
        public BookService(ILoggerFactory loggerFactory, IOptionsMonitor<Options> optionsMonitor, IConfiguration configuration) : base(loggerFactory, optionsMonitor, configuration)
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