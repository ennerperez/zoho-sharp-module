//#define EXPIRED_TOKEN

using System;
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
                return keys[key];

            return null;
        }

        public void SetHttpClient(bool isPdf = false)
        {
            var organizationId = _options.OrganizationId;

            if (_httpClient.DefaultRequestHeaders.CacheControl == null)
                _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            _httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
            _httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            _httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(isPdf ? "application/pdf" : "application/json"));

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("authorization", string.Format(CultureInfo.InvariantCulture, "Zoho-oauthtoken {0}", AuthToken));
            _httpClient.DefaultRequestHeaders.Add("x-com-zoho-subscriptions-organizationid", organizationId);
        }

        public async Task GetTokenAsync(bool force = false)
        {
            
            var authFilename = Path.Combine(Path.GetTempPath(), $"{_options.ClientSecret}.token");
            if (File.Exists(authFilename)) _authToken = File.ReadAllText(authFilename);
            
            if (force) _authToken = string.Empty;

#if EXPIRED_TOKEN
            if (!force)
            {
                _authToken = "1000.15c9c29cbac08d7743858b207df507f4.6ef626fe7785f2a46dbc7d297f1b63ec";
                File.WriteAllText(authFilename, _authToken);
            }
#endif

            if (!string.IsNullOrWhiteSpace(_authToken)) return;

            var retryCount = 0;
            bool IsSuccessStatusCode = false;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                throw new UnauthorizedAccessException("Unable to get token.");
        }

        public async Task<ProcessEntity<T>> ProcessResponse<T>(HttpResponseMessage response, string subnode = "")
        {
            if (null == response) throw new ArgumentNullException("response");

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
                                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                                var file = Path.Combine(path, DateTime.Now.Ticks.ToString() + ".json");
                                File.WriteAllText(file, rawErrorResponse);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(rawErrorResponse)) throw new InvalidDataException();

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
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            var file = Path.Combine(path, DateTime.Now.Ticks.ToString() + ".json");
                            File.WriteAllText(file, rawResponseContent);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(rawResponseContent)) throw new InvalidDataException();

                if (!string.IsNullOrWhiteSpace(subnode))
                {
                    var innerNodeContent = JsonConvert.DeserializeObject<JObject>(rawResponseContent);
                    if (innerNodeContent != null && innerNodeContent.ContainsKey(subnode) && innerNodeContent[subnode] != null)
                    {
                        var data = innerNodeContent[subnode].ToObject<T>();
                        return new ProcessEntity<T> { Data = data };
                    }
                }

                return new ProcessEntity<T> { Data = JsonConvert.DeserializeObject<T>(rawResponseContent) };
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

        public async Task<TOutput> InvokePostAsync<TOutput>(string module, string url, object input, string subnode = "")
        {
            if (input == null) throw new ArgumentNullException("input");

            // Sanity patch for base URL to end with /
            var apiBaseUrl = _options.Modules[module].Url;
            if (!apiBaseUrl.EndsWith("/"))
                apiBaseUrl = apiBaseUrl + "/";

            url = $"{apiBaseUrl}{url}";

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var retryCount = 0;
            bool IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.PostAsync(url, content);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    await GetTokenAsync(true);
                else
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                retryCount++;
            }

            if (processResult == null) throw new InvalidOperationException("API call did not completed successfully");
            else if (processResult.Error != null) throw processResult.Error;
            else return processResult.Data;
        }

        public async Task<JObject> InvokeGetAsync(string module, string url, string subnode = "")
        {
            return await InvokeGetAsync<JObject>(module, url, subnode);
        }

        public async Task<TOutput> InvokeGetAsync<TOutput>(string module, string url, string subnode = "")
        {

            // Sanity patch for base URL to end with /
            var apiBaseUrl = _options.Modules[module].Url;
            if (!apiBaseUrl.EndsWith("/"))
                apiBaseUrl = apiBaseUrl + "/";

            url = $"{apiBaseUrl}{url}";

            var retryCount = 0;
            bool IsSuccessStatusCode = false;
            ProcessEntity<TOutput> processResult = null;
            while (!IsSuccessStatusCode && retryCount < 3)
            {
                SetHttpClient();
                var response = await _httpClient.GetAsync(url);
                IsSuccessStatusCode = response.IsSuccessStatusCode;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    await GetTokenAsync(true);
                else
                    processResult = await ProcessResponse<TOutput>(response, subnode);
                retryCount++;
            }

            if (processResult == null) throw new InvalidOperationException("API call did not completed successfully");
            else if (processResult.Error != null) throw processResult.Error;
            else return processResult.Data;
        }
    }
}
