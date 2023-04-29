using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Zoho.Models;

// ReSharper disable once CheckNamespace
namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zohoapis.com/crm/v3/
    /// </summary>
    public interface ICrmService : IZohoService
    {
        Task<PageResult<JObject>> GetRecords(Enums.Module module, int perPage = 3, params string[] fields);
        Task<PageResult<T>> GetRecords<T>(Enums.Module module, int perPage = 3, params string[] fields);
        Task<PageResult<JObject>> GetAttachments(Enums.Module module, string recordId, params string[] fields);
        Task<PageResult<T>> GetAttachments<T>(Enums.Module module, string recordId, params string[] fields);
    }
}
