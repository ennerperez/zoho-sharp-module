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
    }
}
