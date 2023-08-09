using System;
using System.Threading.Tasks;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class BookService : IBookService
    {
        private string Name => Enum.GetName(Enums.Module.Books);

        private readonly Factory _factory;

        public BookService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<JObject> CreateBillAsync(JObject input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync(Name, "bills", input);
        }

        public async Task<JObject> CreateInvoiceAsync(JObject input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync(Name, "invoices", input);
        }

    }
}
