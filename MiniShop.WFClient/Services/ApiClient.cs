using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniShop.WFClient.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(string? baseUrl = null)
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? AppConfig.ApiBaseUrl)
            };
        }

        public void SetToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var res = await _http.GetAsync(endpoint);
            if (!res.IsSuccessStatusCode) return default;
            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _http.PostAsync(endpoint, content);
            var response = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var res = await _http.DeleteAsync(endpoint);
            return res.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> PostMultipartAsync(string endpoint, MultipartFormDataContent content)
        {
            return await _http.PostAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> PutMultipartAsync(string endpoint, MultipartFormDataContent content)
        {
            return await _http.PutAsync(endpoint, content);
        }
    }
}
