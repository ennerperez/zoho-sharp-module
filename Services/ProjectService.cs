﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Zoho.Interfaces;
using Task = Zoho.Records.Project.Task;

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
        public async Task<T[]> GetProjects<T>(long? portalId = null)
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

        public async Task<T[]> GetTaskDetails<T>(string taskId, string projectId, long? portalId = null)
        {
            //GET  /restapi/portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/", "tasks");
            return response;
        }
        public async Task<T[]> GetProject<T>(string projectId, long? portalId = null)
        {
            //GET  /restapi/portal/[PORTALID]/projects/[PROJECTID]/
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/", "projects");
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
        public async Task<Task> UpdateTask(string projectId, string taskId, object input, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync<Task>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/", input, mediaType: string.Empty);
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

        public async Task<JObject> CreatedCommentTask(string projectId, string taskId, object input, long? portalId = null)
        {
            return await CreatedCommentTask<JObject>(projectId, taskId, input);
        }

        public async Task<T> CreatedCommentTask<T>(string projectId, string taskId, object input, long? portalId = null)
        {
            //POST /restapi /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/comments/
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokePostAsync<T>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/comments/", input, mediaType: string.Empty);
            return response;
        }

        ///GET  /restapi/portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/comments/
        public async Task<T[]> GetCommentsTask<T>(string taskId, string projectId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"portal/{portalId}/projects/{projectId}/tasks/{taskId}/comments/", "comments");
            return response;
        }

        /// <summary>
        /// POST /portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/attachments/
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="portalId"></param>
        /// <param name="projectId"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task<JObject> UploadAttachmentTask(string projectId, string taskId, long? portalId = null, Dictionary<string, Structures.Attachment> attachments = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response1 = new List<JObject[]>();
            if (attachments != null)
            {
                foreach (var item in attachments)
                {
                    var attachmentsNew = new Dictionary<string, Structures.Attachment>();
                    attachmentsNew.Add(item.Key, item.Value);
                    var responseAux = await client.InvokePostAsync<JObject[]>(Name, $"https://projects.zoho.com/api/v3/portal/{portalId}/attachments", "upload_file", attachments: attachmentsNew, subnode: "attachment");
                    response1.Add(responseAux);
                }
            }
            //var attachment_ids = response1.Select(m => m["attachment_id"].Value<string>()).ToArray();
            var attachmentIds = response1.SelectMany(m => m.Select(n => n["attachment_id"].Value<string>())).ToArray();
            //"https://projects.zoho.com/api/v3/portal/777023207/projects/1947441000000114005/tasks/1947441000000115008/attachments"
            //var response2 = await client.InvokePostAsync<JObject>(Name, $"https://projects.zoho.com/api/v3/portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments", attachment_ids, mediaType: "MultipartFormData");

            var response2 = await client.InvokePostZohoPdfAsync<JObject>($"https://projects.zoho.com/api/v3/portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments", attachmentIds);
            return response2;

        }

        /// <summary>
        /// GET https://projects.zoho.com/api/v3/portal/[PORTALID]/projects/[PROJECTID]/tasks/[TASKID]/attachments
        /// </summary>
        /// <returns></returns>
        public async Task<T[]> GetAttachmentsTask<T>(string projectId, string taskId, long? portalId = null)
        {
            var client = await _factory.CreateAsync();
            portalId ??= client.GetOption<long>(Name, "PortalId");
            var response = await client.InvokeGetAsync<T[]>(Name, $"https://projects.zoho.com/api/v3/portal/{portalId}/projects/{projectId}/tasks/{taskId}/attachments", subnode: "attachment");
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

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption(Name, key);
        }
    }
}
