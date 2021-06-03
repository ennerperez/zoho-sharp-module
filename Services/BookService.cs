using System.Threading.Tasks;
using Infrastructure.Enterprise.Abstractions.Services;
using Infrastructure.Enterprise.Books.Models;
using Infrastructure.Enterprise.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Enterprise.Services
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