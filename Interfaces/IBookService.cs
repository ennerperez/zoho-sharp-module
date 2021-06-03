using System.Threading.Tasks;
using Infrastructure.Enterprise.Abstractions.Interfaces;
using Infrastructure.Enterprise.Books.Models;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Enterprise.Interfaces
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