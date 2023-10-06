//#define EXPIRED_TOKEN

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zoho.Models;

// ReSharper disable RedundantAssignment
// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class ZohoService
    {
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient;

        //private readonly Utf8JsonSerializer _jsonSerializer;

        private static string _authToken;
        protected internal static string AuthToken => _authToken;

        public JsonSerializerSettings SerializerSettings { get; set; }

        internal HttpClient HttpClient => _httpClient;

        public ZohoService(IOptionsMonitor<Options> optionsMonitor, HttpClient httpClient, ILoggerFactory loggerFactory) //, Utf8JsonSerializer jsonSerializer
        {
            _options = optionsMonitor.CurrentValue;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            _logger = loggerFactory.CreateLogger(GetType());
        }

        private Options _options;

        public void Configure(Options options)
        {
            _options = options;
        }

        public string GetOption(string module, string key)
        {
            var keys = _options.Modules[module].Keys;
            if (keys != null && keys.Any(m => m.Key == key))
            {
                return keys[key];
            }

            return null;
        }

        public T GetOption<T>(string module, string key)
        {
            var keys = _options.Modules[module].Keys;
            if (keys != null && keys.Any(m => m.Key == key))
            {
                var value = keys[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return default;
        }

        public void SetHttpClient(bool isPdf = false)
        {
            var organizationId = _options.OrganizationId;

            if (_httpClient.DefaultRequestHeaders.CacheControl == null)
            {
                _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();
            }

            _httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
            _httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            _httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(isPdf ? System.Net.Mime.MediaTypeNames.Application.Pdf : System.Net.Mime.MediaTypeNames.Application.Json));

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("authorization", string.Format(CultureInfo.InvariantCulture, "Zoho-oauthtoken {0}", AuthToken));
            _httpClient.DefaultRequestHeaders.Add("x-com-zoho-subscriptions-organizationid", organizationId);
        }

        public async Task GetTokenAsync(bool force = false)
        {
            var authFilename = Path.Combine(Path.GetTempPath(), $"{_options.ClientSecret}.token");
            if (File.Exists(authFilename))
            {
                _authToken = File.ReadAllText(authFilename);
            }

            if (force)
            {
                _authToken = string.Empty;
            }

#if DEBUG && EXPIRED_TOKEN
            if (!force)
            {
                _authToken = "1000.15c9c29cbac08d7743858b207df507f4.6ef626fe7785f2a46dbc7d297f1b63ec";
                File.WriteAllText(authFilename, _authToken);
            }
#endif

            if (!string.IsNullOrWhiteSpace(_authToken))
            {
                return;
            }

            var retryCount = 0;
            var IsSuccessStatusCode = false;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(_options.RefreshToken), "refresh_token");
                    content.Add(new StringContent(_options.ClientId), "client_id");
                    content.Add(new StringContent(_options.ClientSecret), "client_secret");
                    content.Add(new StringContent("refresh_token"), "grant_type");

                    var result = await httpClient.PostAsync(_options.Modules["Accounts"].Url, content);
                    IsSuccessStatusCode = result.IsSuccessStatusCode;

                    if (IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            var jobject = JObject.Parse(data);
                            _authToken = jobject.GetValue("access_token")?.ToString();
                            File.WriteAllText(authFilename, _authToken);
#if DEBUG
                            _logger.LogInformation("AuthToken: {AuthToken}", _authToken);
#endif
                        }
                    }
                }

                retryCount++;
            }

            if (!IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Unable to get token.");
            }
        }

        public async Task<ProcessEntity<T>> ProcessResponse<T>(HttpResponseMessage response, string subnode = "")
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (!response.IsSuccessStatusCode)
            {
                Response errorResponse;
                try
                {
                    var rawErrorResponse = await response.Content.ReadAsStringAsync();

                    if (_options.Debug)
                    {
                        if (!string.IsNullOrWhiteSpace(rawErrorResponse))
                        {
                            try
                            {
                                // ReSharper disable once RedundantAssignment
                                var path = "responses";
#if DEBUG
                                path = Path.Combine("bin", "Debug", "responses");
#endif
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }

                                var file = Path.Combine(path, DateTime.Now.Ticks.ToString() + ".json");
                                File.WriteAllText(file, rawErrorResponse);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(rawErrorResponse))
                    {
                        throw new InvalidDataException();
                    }

                    errorResponse = JsonConvert.DeserializeObject<Response>(rawErrorResponse);
                }
                catch (Exception exception)
                {
                    return new ProcessEntity<T> { Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred", exception) };
                }

                if (null == errorResponse || string.IsNullOrWhiteSpace(errorResponse.Message)) return new ProcessEntity<T> { Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred") };

                return new ProcessEntity<T> { Error = new InvalidOperationException(errorResponse.Message) };
            }

            if (typeof(T) == typeof(bool)) return new ProcessEntity<T> { Data = (T)(object)response.IsSuccessStatusCode };

            try
            {
                var rawResponseContent = await response.Content.ReadAsStringAsync();

                if (_options.Debug)
                {
                    if (!string.IsNullOrWhiteSpace(rawResponseContent))
                    {
                        try
                        {
                            var path = "responses";
#if DEBUG
                            path = Path.Combine("bin", "Debug", "responses");
#endif
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            var file = Path.Combine(path, DateTime.Now.Ticks.ToString() + ".json");
                            File.WriteAllText(file, rawResponseContent);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(rawResponseContent))
                {
                    throw new InvalidDataException();
                }

                if (!string.IsNullOrWhiteSpace(subnode))
                {
                    var innerNodeContent = JsonConvert.DeserializeObject<JObject>(rawResponseContent);
                    if (innerNodeContent != null && innerNodeContent.ContainsKey(subnode) && innerNodeContent[subnode] != null)
                    {
                        var data = innerNodeContent[subnode].ToObject<T>();
                        return new ProcessEntity<T> { Data = data };
                    }
                }

                var result = new ProcessEntity<T> { Data = JsonConvert.DeserializeObject<T>(rawResponseContent) };
                if (typeof(T).IsAssignableTo(typeof(Response)))
                {
                    var isError = (result.Data as Response).Code != 0;
                    if (isError)
                        throw new InvalidOperationException((result.Data as Response).Message);
                }

                return result;
            }
            catch (Exception exception)
            {
                return new ProcessEntity<T> { Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred", exception) };
            }
        }

        public async Task<JObject> InvokePostAsync(string module, string url, object input, string subnode = "")
        {
            return await InvokePostAsync<JObject>(module, url, input, subnode);
        }

        public async Task<TOutput> InvokePostAsync<TOutput>(string module, string url, object input, string subnode = "", string mediaType = System.Net.Mime.MediaTypeNames.Application.Json, Dictionary<string, Zoho.Structures.Attachment> attachments = null)
        {
            if (input == null && attachments == null)
            {
                throw new ArgumentNullException("input");
            }

            if (!_options.Modules[module].Enabled)
            {
                throw new InvalidOperationException($"The required module ({module}) is not enabled");
            }

            if (!url.StartsWith("http"))
            {
                var apiBaseUrl = _options.Modules[module].Url;
                if (!apiBaseUrl.EndsWith("/"))
                {
                    apiBaseUrl = apiBaseUrl + "/";
                }

                url = $"{apiBaseUrl}{url}";
            }

            HttpContent content = null;
            if (mediaType == "MultipartFormData")
            {
                content = new MultipartFormDataContent();
                var props = input.GetType().GetProperties();
                var values = props.Select(m => new KeyValuePair<string, string>(m.Name, m.GetValue(input)?.ToString()));
                foreach (var value in values)
                {
                    (content as MultipartFormDataContent).Add(new StringContent(value.Value), value.Key);
                }
            }
            else if (attachments != null && attachments.Any())
            {
                var slap = 0;
                ProcessEntity<TOutput> processResultFile = null;
                while (slap < 3)
                {
                    var client3 = new HttpClient();
                    var request3 = new HttpRequestMessage(HttpMethod.Post, url);
                    var bearer = $"Bearer {AuthToken}";
                    request3.Headers.Add("Authorization", bearer);
                    request3.Headers.Add("Cookie", "906475c51c=7f345f363adec38c7a11fb62ec02d1c1; JSESSIONID=EAC5A7F20EBC9FC0ED77750F4B452194; _zcsr_tmp=641755b7-d70c-4c66-b4a7-6823b3fd6c56; zpct=641755b7-d70c-4c66-b4a7-6823b3fd6c56");
                    content = new MultipartFormDataContent();
                    foreach (var item in attachments)
                    {
                        var tempFile = Path.GetTempFileName();
                        await File.WriteAllBytesAsync(tempFile, item.Value.Bytes);
                        (content as MultipartFormDataContent).Add(new StreamContent(File.OpenRead(tempFile)), input.ToString(), item.Key);
                        if (item.Value.Details == null)
                            continue;

                        var jsonDetailValue = JsonConvert.SerializeObject(item.Value.Details, Formatting.None);
                        (content as MultipartFormDataContent).Add(new StringContent(jsonDetailValue), "attachment_details");
                        //(content as MultipartFormDataContent).Add(new StringContent("{\"location_details\":{\"folder_id\":\"-1\",\"project_id\":\"1947441000000114005\"},\"storage_type\":\"workdrive\"}"), "attachment_details");
                    }
                    request3.Content = content;
                    var responseZohoFile = await client3.SendAsync(request3);
                    responseZohoFile.EnsureSuccessStatusCode();

                    if (responseZohoFile.StatusCode == HttpStatusCode.Created)
                    {
                        processResultFile = await ProcessResponse<TOutput>(responseZohoFile, subnode);
                        break;
                    }
                    if (responseZohoFile.StatusCode == HttpStatusCode.Unauthorized)
                        await GetTokenAsync(true);

                    slap++;
                }
                if (processResultFile != null)
                {
                    return processResultFile.Data;
                }
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (mediaType == System.Net.Mime.MediaTypeNames.Application.Json)
            {
                var data = JsonConvert.SerializeObject(input, Formatting.None, SerializerSettings);
                content = new StringContent(data, Encoding.UTF8, mediaType);
            }
            else
            {
                var props = input.GetType().GetProperties();
                var values = props.Select(m => new KeyValuePair<string, string>(m.Name, m.GetValue(input)?.ToString()));
                content = new FormUrlEncodedContent(values);
            }

            var retryCount = 0;
            var IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.PostAsync(url, content);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                    IsSuccessStatusCode = true;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                }
                else
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                }

                retryCount++;
            }

            if (processResult == null)
            {
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (processResult.Error != null)
            {
                throw processResult.Error;
            }
            else
            {
                return processResult.Data;
            }
        }

        public async Task<TOutput> InvokePostZohoPdfAsync<TOutput>(string url, string[] attachmentsIds, string subnode = "")
        {
            HttpContent content = null;
            var isAuthorized2 = false;
            var attemptCount2 = 0;
            HttpResponseMessage response = null;
            ProcessEntity<TOutput> processResultFile2 = null;
            while (!isAuthorized2 && attemptCount2 <= 3)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var bearer = $"Bearer {AuthToken}";
                request.Headers.Add("Authorization", bearer);
                //request.Headers.Add("Cookie", "906475c51c=7f345f363adec38c7a11fb62ec02d1c1; JSESSIONID=4FF925555EA8295AD0D52A7BFEF9B716; _zcsr_tmp=641755b7-d70c-4c66-b4a7-6823b3fd6c56; zpct=641755b7-d70c-4c66-b4a7-6823b3fd6c56");
                content = new MultipartFormDataContent();
                var jsonDetailValue = JsonConvert.SerializeObject(attachmentsIds, Formatting.None);
                (content as MultipartFormDataContent).Add(new StringContent(jsonDetailValue), "attachment_ids");
                request.Content = content;
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                    isAuthorized2 = true;
                }
                else
                {
                    break;
                }
                attemptCount2++;
            }

            if (response != null)
            {
                processResultFile2 = await ProcessResponse<TOutput>(response, subnode);
            }

            if (processResultFile2 != null)
            {
                return processResultFile2.Data;
            }

            throw processResultFile2.Error;
        }

        public async Task<TOutput> InvokePostFileAsync<TOutput>(string module, string url, byte[] input, string fileName, string subnode = "")
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!_options.Modules[module].Enabled)
            {
                throw new InvalidOperationException($"The required module ({module}) is not enabled");
            }

            if (!url.StartsWith("http"))
            {
                var apiBaseUrl = _options.Modules[module].Url;
                if (!apiBaseUrl.EndsWith("/"))
                {
                    apiBaseUrl = apiBaseUrl + "/";
                }

                url = $"{apiBaseUrl}{url}";
            }

            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(new MemoryStream(input));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
            content.Add(fileContent);

            var retryCount = 0;
            var IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.PostAsync(url, content);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                }
                else
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                }

                retryCount++;
            }

            if (processResult == null)
            {
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (processResult.Error != null)
            {
                throw processResult.Error;
            }
            else
            {
                return processResult.Data;
            }
        }

        public async Task<JObject> InvokePutAsync(string module, string url, object input, string subnode = "")
        {
            return await InvokePutAsync<JObject>(module, url, input, subnode);
        }

        public async Task<TOutput> InvokePutAsync<TOutput>(string module, string url, object input, string subnode = "", string mediaType = System.Net.Mime.MediaTypeNames.Application.Json)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (!_options.Modules[module].Enabled)
            {
                throw new InvalidOperationException($"The required module ({module}) is not enabled");
            }

            if (!url.StartsWith("http"))
            {
                var apiBaseUrl = _options.Modules[module].Url;
                if (!apiBaseUrl.EndsWith("/"))
                {
                    apiBaseUrl = apiBaseUrl + "/";
                }

                url = $"{apiBaseUrl}{url}";
            }

            HttpContent content;
            if (mediaType == System.Net.Mime.MediaTypeNames.Application.Json)
            {
                var data = JsonConvert.SerializeObject(input, Formatting.None, SerializerSettings);
                content = new StringContent(data, Encoding.UTF8, mediaType);
            }
            else
            {
                var props = input.GetType().GetProperties();
                var values = props.Select(m => new KeyValuePair<string, string>(m.Name, m.GetValue(input)?.ToString()));
                content = new FormUrlEncodedContent(values);
            }

            var retryCount = 0;
            var IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.PutAsync(url, content);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                }
                else
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                }

                retryCount++;
            }

            if (processResult == null)
            {
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (processResult.Error != null)
            {
                throw processResult.Error;
            }
            else
            {
                return processResult.Data;
            }
        }


        public async Task<JObject> InvokeGetAsync(string module, string url, string subnode = "")
        {
            return await InvokeGetAsync<JObject>(module, url, subnode);
        }

        public async Task<TOutput> InvokeGetAsync<TOutput>(string module, string url, string subnode = "")
        {
            if (!_options.Modules[module].Enabled)
            {
                throw new InvalidOperationException($"The required module ({module}) is not enabled");
            }

            if (!url.StartsWith("http"))
            {
                var apiBaseUrl = _options.Modules[module].Url;
                if (!apiBaseUrl.EndsWith("/"))
                {
                    apiBaseUrl = apiBaseUrl + "/";
                }

                url = $"{apiBaseUrl}{url}";
            }

            var retryCount = 0;
            var isSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!isSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.GetAsync(url);
                isSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                }
                else
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);

                    if (processResult.Error != null)
                    {
                        isSuccessStatusCode = false;
                        await GetTokenAsync(true);
                    }
                }

                retryCount++;
            }

            if (processResult == null)
            {
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (processResult.Error != null)
            {
                throw processResult.Error;
            }
            else
            {
                return processResult.Data;
            }
        }

        public async Task<JObject> InvokeDeleteAsync(string module, string url, object input, string subnode = "")
        {
            return await InvokeDeleteAsync<JObject>(module, url, subnode);
        }

        public async Task<TOutput> InvokeDeleteAsync<TOutput>(string module, string url, string subnode = "", string mediaType = System.Net.Mime.MediaTypeNames.Application.Json)
        {
            if (!_options.Modules[module].Enabled)
            {
                throw new InvalidOperationException($"The required module ({module}) is not enabled");
            }

            // Sanity patch for base URL to end with /
            var apiBaseUrl = _options.Modules[module].Url;
            if (!apiBaseUrl.EndsWith("/"))
            {
                apiBaseUrl = apiBaseUrl + "/";
            }

            url = $"{apiBaseUrl}{url}";

            HttpContent content = null;

            var retryCount = 0;
            var IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.PostAsync(url, content);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await GetTokenAsync(true);
                }
                else
                {
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                }

                retryCount++;
            }

            if (processResult == null)
            {
                throw new InvalidOperationException("API call did not completed successfully");
            }
            else if (processResult.Error != null)
            {
                throw processResult.Error;
            }
            else
            {
                return processResult.Data;
            }
        }
    }
}
