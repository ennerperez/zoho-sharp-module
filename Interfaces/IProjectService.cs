using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://projectsapi.zoho.com/restapi/
    /// </summary>
    public interface IProjectService
    {
        Task<T[]> GetProjets<T>(long? portalId = null);
        Task<T[]> GetTasks<T>(string projectId, long? portalId = null);
        Task<T[]> GetSubTasks<T>(string projectId, string taskId, long? portalId = null);
        Task<T[]> GetTaskAttachments<T>(string projectId, string taskId, long? portalId = null);
        Task<JObject> UpdateTask(string projectId, string taskId, object input, long? portalId = null);
        Task<T[]> GetTasksSearch<T>(long projectId, long? portalId, string search);

        Task<JObject> CreatedProject(object input, long? portalId = null);

        Task<JObject> UploadAttachmentTask(string projectId, string taskId, long? portalId = null, Dictionary<string, Zoho.Structures.Attachment> attachments = null);
        Task<T[]> GetAttachmentsTask<T>(string projectId, string taskId, long? portalId = null);
        Task<T[]> GetDocumentsTask<T>(string projectId, long? portalId = null);
    }
}
