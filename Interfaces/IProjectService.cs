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
        Task<T[]> GetTasks<T>(long projectId, long? portalId = null);
        Task<JObject> UpdateTask(long projectId, string taskId, object input, long? portalId = null);
        Task<T[]> GetTasksSearch<T>(long projectId, long? portalId,string search);
    }
}
