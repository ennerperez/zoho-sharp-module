using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoho.Interfaces;
using Newtonsoft.Json.Linq;
using Zoho.Models;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class CrmService : ICrmService
    {
        private readonly Factory _factory;

        public CrmService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public async Task<PageResult<JObject>> GetRecords(Enums.Module module, int perPage = 3, params string[] fields)
        {
            return await GetRecords<JObject>(module, perPage, fields);
        }

        public async Task<PageResult<T>> GetRecords<T>(Enums.Module module,int perPage = 3, params string[] fields)
        {
            //{api-domain}/crm/{version}/{module_api_name}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_"," ");
            
            var client = await _factory.CreateAsync();
            if (fields == null || !fields.Any()) fields = new[] { "Last_Name","Email" };
            var response = await client.InvokeGetAsync<PageResult<T>>("Crm", $"{moduleApiName}?fields={string.Join(",",fields)}&per_page={perPage}");
            return response;
        }

        public async Task<PageResult<JObject>> GetAttachments(Enums.Module module, string recordId, params string[] fields)
        {
            return await GetAttachments<JObject>(module, recordId, fields);
        }
        
        public async Task<PageResult<T>> GetAttachments<T>(Enums.Module module, string recordId, params string[] fields)
        {
            //{api-domain}/crm/{version}/{module_api_name}/{record_id}/Attachments
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_"," ");
            
            var client = await _factory.CreateAsync();
            if (fields == null || !fields.Any()) fields = new[] { "id", "Owner", "File_Name", "Created_Time", "Parent_Id" };
            var response = await client.InvokeGetAsync<PageResult<T>>("Crm", $"{moduleApiName}/{recordId}/Attachments?fields={string.Join(",",fields)}");
            return response;
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption("Crm", key);
        }
    }
}
