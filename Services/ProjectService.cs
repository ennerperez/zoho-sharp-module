using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Zoho.Interfaces;

namespace Zoho.Services
{
    public class ProjectService : IProjectService
    {
        private readonly Factory _factory;

        private string Name => Enum.GetName(Enums.Module.Projects);

        public ProjectService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// //{baseUrl}/portals
        /// </summary>
        /// <param name="portalId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T[]> GetProjets<T>(long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/", "projects");
            return response;
        }

        /// <summary>
        /// /portal/[PORTALID]/projects/[PROJECTID]/tasks/
        /// </summary>
        /// <returns></returns>
        public async Task<T[]> GetTasks<T>(long projectId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/","tasks");
            return response;
        }

    }
}
