using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorSampleCommerce.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;
        private static readonly JsonSerializerOptions _opts = new(JsonSerializerDefaults.Web);

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value, _opts));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", key);
            if (string.IsNullOrEmpty(json)) return default;
            return JsonSerializer.Deserialize<T>(json, _opts);
        }

        public async Task RemoveAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
