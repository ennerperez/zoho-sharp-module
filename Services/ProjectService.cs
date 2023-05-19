using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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

        public Task<JObject> UpdateTask(long projectId, string taskId, object input, long? portalId = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// /portal/[PORTALID]/projects/[PROJECTID]/tasks/search?search_term=
        /// </summary>
        /// <returns></returns>
        public async Task<T[]> GetTasksSearch<T>(long projectId, long? portalId, string search)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            
            var encodedToSearch = Uri.EscapeDataString(search);
            
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/search?search_term={encodedToSearch}/","tasks");
            return response;
        }
        
        /// <summary>
        /// POST  /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/
        /// </summary>
        /// <returns></returns>
        public async Task<JObject> UpdateTask(long projectId,string taskId,JObject input, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/",input);
            return response;
        }
    }
}