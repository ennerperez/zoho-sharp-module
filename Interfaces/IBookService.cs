using System.Threading.Tasks;
using Zoho.Abstractions.Interfaces;
using Zoho.Books.Models;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/books/api/v3/
    /// </summary>
    public interface IBookService : IEnterpriseService
    {
        Task<JObject> CreateBillAsync(Bill input);
        Task<JObject> CreateInvoiceAsync(Invoice input);
    }
}