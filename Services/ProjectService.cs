using System;
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
        public async Task<T[]> GetTasks<T>(string projectId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/", "tasks");
            return response;
        }

        /// <summary>
        /// GET  /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/subtasks/
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <param name="portalId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T[]> GetSubTasks<T>(string projectId, string taskId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/subtasks/", "tasks");
            return response;
        }

        /// <summary>
        /// GET /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/attachments/
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <param name="portalId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T[]> GetTaskAttachments<T>(string projectId, string taskId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments/");
            return response;
        }

        /// <summary>
        /// POST  /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/
        /// </summary>
        /// <returns></returns>
        public async Task<JObject> UpdateTask(string projectId, string taskId, object input, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync<JObject>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/", input, mediaType: string.Empty);
            return response;
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

            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/search?search_term={encodedToSearch}/", "tasks");
            return response;
        }

        public async Task<JObject> CreatedProject(object input, long? portalId = null)
        {
            //portal/[PORTALID]/projects/
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync<JObject>(Name, $"portal/{portalId}/projects/", input, mediaType: string.Empty);
            return response;
        }

        /// <summary>
        /// POST /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/attachments/
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="input"></param>
        /// <param name="portalId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<JObject> UploadAttachmentTask(string projectId, string taskId, byte[] input, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync<JObject>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments/", input, mediaType: string.Empty);
            return response;
        }

        /// <summary>
        /// GET /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/attachments/
        /// </summary>
        /// <returns></returns>
        public async Task<T[]> GetAttachmentsTask<T>(string projectId, string taskId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments/");
            return response;
        }

        /// <summary>
        /// GET /portal/[PORTALID]/projects/[PROJECTID]/documents/
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="portalId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T[]> GetDocumentsTask<T>(string projectId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/documents/");
            return response;
        }
    }
}
