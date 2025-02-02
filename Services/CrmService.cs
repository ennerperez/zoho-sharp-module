﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Zoho.Interfaces;
using Zoho.Models;
using Zoho.Records.Project;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class CrmService : ICrmService
    {
        private string Name => Enum.GetName(Enums.Module.Crm);

        private readonly Factory _factory;

        public CrmService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _factory.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            };
        }

        public async Task<JObject> UploadAttachmentAsync(Enums.Module module, string recordId, byte[] input, string filename)
        {
            //{api-domain}/crm/{version}/{module_api_name}/{record_id}/Attachments
            var client = await _factory.CreateAsync();
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");
            return await client.InvokePostFileAsync<JObject>(Name, $"{moduleApiName}/{recordId}/Attachments", input, filename);
        }

        public async Task<PageResult<JObject>> GetRecords(Enums.Module module, int perPage = 3, params string[] fields)
        {
            return await GetRecords<JObject>(module, perPage, fields);
        }

        public async Task<PageResult<T>> GetRecordsSearch<T>(Enums.Module module, string word)
        {
            //GET /{module_api_name}/search?word={{search_word_here}}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");

            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<PageResult<T>>(Name, $"{moduleApiName}/search?word={word}");
            return response;
        }

        public async Task<PageResult<T>> GetRecords<T>(Enums.Module module, int perPage = 3, params string[] fields)
        {
            //{api-domain}/crm/{version}/{module_api_name}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");

            var client = await _factory.CreateAsync();
            if (fields == null || !fields.Any()) fields = new[] { "Last_Name", "Email" };
            var response = await client.InvokeGetAsync<PageResult<T>>(Name, $"{moduleApiName}?fields={string.Join(",", fields)}&per_page={perPage}");
            return response;
        }

        public async Task<Response<string>[]> CreateRecordAsync(Enums.Module module, object input)
        {
            var client = await _factory.CreateAsync();
            //{api-domain}/crm/{version}/{module_api_name}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");
            return await client.InvokePostAsync<Response<string>[]>(Name, $"{moduleApiName}", input, "data");
        }

        public async Task<Response<string>[]> UpdateRecordAsync(Enums.Module module, string recordId, object input)
        {
            var client = await _factory.CreateAsync();
            //{api-domain}/crm/{version}/{module_api_name}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");
            return await client.InvokePutAsync<Response<string>[]>(Name, $"{moduleApiName}/{recordId}", input, "data");
        }

        public async Task<PageResult<JObject>> GetAttachments(Enums.Module module, string recordId, params string[] fields)
        {
            return await GetAttachments<JObject>(module, recordId, fields);
        }

        public async Task<PageResult<T>> GetAttachments<T>(Enums.Module module, string recordId, params string[] fields)
        {
            //{api-domain}/crm/{version}/{module_api_name}/{record_id}/Attachments
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");

            var client = await _factory.CreateAsync();
            if (fields == null || !fields.Any()) fields = new[] { "id", "Owner", "File_Name", "Created_Time", "Parent_Id" };
            var response = await client.InvokeGetAsync<PageResult<T>>(Name, $"{moduleApiName}/{recordId}/Attachments?fields={string.Join(",", fields)}");
            return response;
        }
        public async Task<PageResult<T>> DeleteAttachment<T>(Enums.Module module, string accountId, string recordId)
        {
            //{api-domain}/crm/{version}/{module_api_name}/{record_id}/Attachments/{attachment_id}
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");

            var client = await _factory.CreateAsync();
            var response = await client.InvokeDeleteAsync<PageResult<T>>(Name, $"{moduleApiName}/{accountId}/Attachments/{recordId}", "Data");
            return response;
        }
        public async Task<Dictionary<string, byte[]>> GetDownloadAttachments<T>(Enums.Module module, string accountId, string recordId)
        {
            //{dominio-api}/crm/{versión}/{module_api_name}/{record_ID}/actions/download_fields_attachment   ?fields_attachment_id=554023000001736007
            //"https://www.zohoapis.com/crm/v6/Accounts/100023009/Attachments/100013547"
            var moduleApiName = Enum.GetName(typeof(Enums.Module), module)?.Replace("_", " ");

            var client = await _factory.CreateAsync();
            //if (fields == null || !fields.Any()) fields = new[] { "id", "Owner", "File_Name", "Created_Time", "Parent_Id" };
            //var response = await client.InvokeGetAsync<PageResult<T>>(Name, $"{moduleApiName}/{recordId}/download_fields_attachment?fields={string.Join(",", fields)}");
            var data = await client.InvokeGetImageAsync(Name, $"{moduleApiName}/{accountId}/Attachments/{recordId}");
            //var response = $"https://www.zohoapis.com/crm/v6/{moduleApiName}/{accountId}/Attachments/{recordId}";
            try
            {

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo: {ex.Message}");
            }

            return null;
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption(Name, key);
        }

    }
}
