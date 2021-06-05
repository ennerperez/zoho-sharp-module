using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Enterprise.Abstractions.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Enterprise.Abstractions.Services
{
    public abstract class EnterpriseService
    {
        public static string AuthToken;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly Options _options;
        private DateTime _expiresIn;

        public EnterpriseService(ILoggerFactory loggerFactory, IOptionsMonitor<Options> optionsMonitor, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _options = optionsMonitor.CurrentValue;
            _configuration = configuration;
        }

        public string GetOption(string module, string key)
        {
            var keys = _options.Modules[module].Keys;
            if (keys != null && keys.Any(m => m.Key == key))
                return keys[key];

            return null;
        }

        public async Task RefreshTokenAsync()
        {
            if (!string.IsNullOrEmpty(AuthToken) && DateTime.Now < _expiresIn) return;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_options.Modules["Accounts"].Url);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(_options.RefreshToken), "refresh_token");
                content.Add(new StringContent(_options.ClientId), "client_id");
                content.Add(new StringContent(_options.ClientSecret), "client_secret");
                content.Add(new StringContent("refresh_token"), "grant_type");

                var result = await httpClient.PostAsync(_options.Modules["Accounts"].Url, content);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        var jobject = JObject.Parse(data);
                        AuthToken = jobject.GetValue("access_token")?.ToString();
                        if (_configuration["AppSettings:Environment"] == "QA") _logger.LogInformation($"AuthToken: {AuthToken}");

                        if (!string.IsNullOrWhiteSpace(AuthToken))
                        {
                            var expiresIn = int.Parse(jobject.GetValue("expires_in")?.ToString() ?? "3600");
                            _expiresIn = DateTime.Now.AddMinutes(expiresIn);
                        }
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to refresh token.");
                }
            }
        }

        public HttpClient GetHttpClient(string module, bool isPdf = false)
        {
            var apiBaseUrl = _options.Modules[module].Url;
            var organizationId = _options.OrganizationId;

            var httpClient = new HttpClient();

            if (httpClient.DefaultRequestHeaders.CacheControl == null) httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
            httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

            httpClient.Timeout = new TimeSpan(0, 0, 30);

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(isPdf ? "application/pdf" : "application/json"));

            // Sanity patch for base URL to end with /
            if (!apiBaseUrl.EndsWith("/"))
                apiBaseUrl = apiBaseUrl + "/";

            httpClient.BaseAddress = new Uri(apiBaseUrl);

            httpClient.DefaultRequestHeaders.Add("authorization", string.Format(CultureInfo.InvariantCulture, "Zoho-oauthtoken {0}", AuthToken));
            httpClient.DefaultRequestHeaders.Add("x-com-zoho-subscriptions-organizationid", organizationId);

            return httpClient;
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
                    return new ProcessEntity<T> {Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred", exception)};
                }

                if (null == errorResponse || string.IsNullOrWhiteSpace(errorResponse.Message)) return new ProcessEntity<T> {Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred")};

                return new ProcessEntity<T> {Error = new InvalidOperationException(errorResponse.Message)};
            }

            if (typeof(T) == typeof(bool)) return new ProcessEntity<T> {Data = (T) (object) response.IsSuccessStatusCode};

            try
            {
                var rawResponseContent = await response.Content.ReadAsStringAsync();

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

                if (string.IsNullOrWhiteSpace(rawResponseContent)) throw new InvalidDataException();

                if (!string.IsNullOrWhiteSpace(subnode))
                {
                    var innerNodeContent = JsonConvert.DeserializeObject<JObject>(rawResponseContent);
                    if (innerNodeContent.ContainsKey(subnode) && innerNodeContent[subnode] != null)
                    {
                        var data = innerNodeContent[subnode].ToObject<T>();
                        return new ProcessEntity<T> {Data = data};
                    }
                }

                return new ProcessEntity<T> {Data = JsonConvert.DeserializeObject<T>(rawResponseContent)};
            }
            catch (Exception exception)
            {
                return new ProcessEntity<T> {Error = new InvalidOperationException("API call did not completed successfully or response parse error occurred", exception)};
            }
        }

        public async Task<JObject> InvokePostAsync<TInput>(string module, string url, TInput input) where TInput : Model
        {
            if (input == null) throw new ArgumentNullException("input");

            await RefreshTokenAsync();
            using (var httpClient = GetHttpClient(module))
            {
                var data = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                var processResult = await ProcessResponse<JObject>(response);

                if (null != processResult.Error) throw processResult.Error;

                return processResult.Data;
            }
        }

        public async Task<JObject> InvokeGetAsync(string module, string url)
        {
            await RefreshTokenAsync();
            using (var httpClient = GetHttpClient(module))
            {
                var response = await httpClient.GetAsync(url);
                var processResult = await ProcessResponse<JObject>(response);

                if (null != processResult.Error) throw processResult.Error;

                return processResult.Data;
            }
        }
    }
}