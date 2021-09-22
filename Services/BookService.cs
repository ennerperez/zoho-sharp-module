using System;
using System.Net.Http;
using System.Threading.Tasks;
using Zoho.Books.Models;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;

namespace Zoho.Services
{
    public class BookService : IBookService
    {
        private readonly Factory _factory;

        public BookService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<JObject> CreateBillAsync(Bill input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Books", "bills", input);
        }

        public async Task<JObject> CreateInvoiceAsync(Invoice input)
        {
            var client = await _factory.CreateAsync();
            return await client.InvokePostAsync("Books", "invoices", input);
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Books", key);
        }
    }
}