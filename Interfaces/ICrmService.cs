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
        Task<JObject> UploadAttachmentAsync(Enums.Module module, string recordId, byte[] input, string filename);
        Task<PageResult<JObject>> GetRecords(Enums.Module module, int perPage = 3, params string[] fields);
        Task<PageResult<T>> GetRecords<T>(Enums.Module module, int perPage = 3, params string[] fields);
        Task<PageResult<T>> GetRecordsSearch<T>(Enums.Module module, string word);
        Task<PageResult<JObject>> GetAttachments(Enums.Module module, string recordId, params string[] fields);
        Task<PageResult<T>> GetAttachments<T>(Enums.Module module, string recordId, params string[] fields);

        Task<Response<string>[]> CreateRecordAsync(Enums.Module module, object input);
        Task<Response<string>[]> UpdateRecordAsync(Enums.Module module, string recordId, object input);

        Task<byte[]> GetDownloadAttachments<T>(Enums.Module module, string accountId, string recordId);

        Task<PageResult<T>> DeleteAttachment<T>(Enums.Module module, string accountId, string recordId);
    }
}
