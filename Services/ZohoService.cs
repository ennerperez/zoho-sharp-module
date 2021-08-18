using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zoho.Abstractions.Models;

namespace Zoho.Services
{
    public class ZohoService
    {
        private readonly ILogger _logger;

        protected readonly HttpClient _httpClient;
        //private readonly Utf8JsonSerializer _jsonSerializer;

        private DateTime _expiresIn;
        private string _authToken;

        internal DateTime ExpiresIn => _expiresIn;
        internal string AuthToken => _authToken;

        internal HttpClient HttpClient => _httpClient;

        public ZohoService(HttpClient httpClient, ILoggerFactory loggerFactory) //, Utf8JsonSerializer jsonSerializer
        {
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

        public void SetHttpClient(string module, bool isPdf = false)
        {
            var apiBaseUrl = _options.Modules[module].Url;
            var organizationId = _options.OrganizationId;

            //var _httpClient = new HttpClient();

            if (_httpClient.DefaultRequestHeaders.CacheControl == null) _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            _httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
            _httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            _httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

            _httpClient.Timeout = new TimeSpan(0, 0, 30);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(isPdf ? "application/pdf" : "application/json"));

            // Sanity patch for base URL to end with /
            if (!apiBaseUrl.EndsWith("/"))
                apiBaseUrl = apiBaseUrl + "/";

            _httpClient.BaseAddress = new Uri(apiBaseUrl);

            _httpClient.DefaultRequestHeaders.Add("authorization", string.Format(CultureInfo.InvariantCulture, "Zoho-oauthtoken {0}", AuthToken));
            _httpClient.DefaultRequestHeaders.Add("x-com-zoho-subscriptions-organizationid", organizationId);
        }

        public async Task GetTokenAsync()
        {
            if (!string.IsNullOrEmpty(_authToken) && DateTime.Now < _expiresIn) return;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(_options.RefreshToken), "refresh_token");
                content.Add(new StringContent(_options.ClientId), "client_id");
                content.Add(new StringContent(_options.ClientSecret), "client_secret");
                content.Add(new StringContent("refresh_token"), "grant_type");

                var result = await _httpClient.PostAsync(_options.Modules["Accounts"].Url, content);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        var jobject = JObject.Parse(data);
                        _authToken = jobject.GetValue("access_token")?.ToString();
#if DEBUG
                        _logger.LogInformation($"AuthToken: {_authToken}");
#endif

                        if (!string.IsNullOrWhiteSpace(_authToken))
                        {
                            var expiresIn = int.Parse(jobject.GetValue("expires_in")?.ToString() ?? "3600");
                            _expiresIn = DateTime.Now.AddMinutes(expiresIn);
                        }
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to get token.");
                }
            }
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
                                var path = "responses";
#if DEBUG
                                path = Path.Combine("bin", "Debug", "net5.0", "responses");
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
                            path = Path.Combine("bin", "Debug", "net5.0", "responses");
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
                    if (innerNodeContent.ContainsKey(subnode) && innerNodeContent[subnode] != null)
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

        public async Task<JObject> InvokePostAsync<TInput>(string module, string url, TInput input) where TInput : Model
        {
            if (input == null) throw new ArgumentNullException("input");

            await GetTokenAsync();
            SetHttpClient(module);

            var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data;
        }

        public async Task<JObject> InvokeGetAsync(string module, string url)
        {
            await GetTokenAsync();
            SetHttpClient(module);
            
            var response = await _httpClient.GetAsync(url);
            var processResult = await ProcessResponse<JObject>(response);

            if (null != processResult.Error) throw processResult.Error;

            return processResult.Data;
        }
    }
}