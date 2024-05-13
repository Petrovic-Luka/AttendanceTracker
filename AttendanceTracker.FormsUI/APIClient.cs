using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using MongoDB.Bson;

namespace AttendanceTracker.FormsUI
{
    public class APIClient
    {
        private readonly TimeSpan _timeout;
        private HttpClient _httpClient;
        private HttpClientHandler _httpClientHandler;
        private readonly string _baseUrl;
        private const string ClientUserAgent = "my-api-client-v1";
        private const string MediaTypeJson = "application/json";

        public APIClient(string baseUrl, TimeSpan? timeout = null)
        {
            _baseUrl = NormalizeBaseUrl(baseUrl);
            _timeout = timeout ?? TimeSpan.FromSeconds(90);
        }

        public async Task<string> PostAsync(string url, object input)
        {
            EnsureHttpClientCreated();

            using (var requestContent = new StringContent(ConvertToJsonString(input), Encoding.UTF8, MediaTypeJson))
            {
                using (var response = await _httpClient.PostAsync(url, requestContent))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task<TResult> PostAsync<TResult>(string url, object input) where TResult : class, new()
        {
            var strResponse = await PostAsync(url, input);

            return JsonSerializer.Deserialize<TResult>(strResponse);
        }

        public async Task<TResult> GetAsync<TResult>(string url) where TResult : class, new()
        {
            EnsureHttpClientCreated();
            var response=await _httpClient.GetAsync(url);
            var result =await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(result);
        }

        public async Task<string> GetAsync(string url)
        {
            

            using (var response = await _httpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PutAsync(string url, object input)
        {
            return await PutAsync(url, new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, MediaTypeJson));
        }

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            EnsureHttpClientCreated();

            using (var response = await _httpClient.PutAsync(url, content))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> DeleteAsync(string url)
        {
            EnsureHttpClientCreated();

            using (var response = await _httpClient.DeleteAsync(url))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public void Dispose()
        {
            _httpClientHandler?.Dispose();
            _httpClient?.Dispose();
        }

        private void CreateHttpClient()
        {
            _httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            _httpClient = new HttpClient(_httpClientHandler, false)
            {
                Timeout = _timeout
            };

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(ClientUserAgent);

            if (!string.IsNullOrWhiteSpace(_baseUrl))
            {
                _httpClient.BaseAddress = new Uri(_baseUrl);
            }

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
        }

        private void EnsureHttpClientCreated()
        {
            if (_httpClient == null)
            {
                CreateHttpClient();
            }
        }

        private static string ConvertToJsonString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return JsonSerializer.Serialize(obj);
        }

        private static string NormalizeBaseUrl(string url)
        {
            return url.EndsWith("/") ? url : url + "/";
        }
    }
}
