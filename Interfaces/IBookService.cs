using System.Threading.Tasks;
using Zoho.Books.Models;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/books/api/v3/
    /// </summary>
    public interface IBookService : IZohoService
    {
        Task<JObject> CreateBillAsync(Bill input);
        Task<JObject> CreateInvoiceAsync(Invoice input);
    }
}