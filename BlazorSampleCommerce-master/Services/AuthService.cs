using BlazorSampleCommerce.DTOs;
using System.Net.Http.Json;

namespace BlazorSampleCommerce.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly LocalStorageService _storage;
        private readonly TokenStore _tokenStore;

        public bool IsLoggedIn { get; private set; }
        public UserDto? CurrentUser { get; private set; }
        public event Action? OnAuthStateChanged;

        public AuthService(HttpClient http, LocalStorageService storage, TokenStore tokenStore)
        {
            _http = http;
            _storage = storage;
            _tokenStore = tokenStore;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Auth/login", new { Email = email, Password = password });
                if (!response.IsSuccessStatusCode)
                    return false;

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (loginResponse is null || loginResponse.User is null)
                    return false;

                _tokenStore.Token = loginResponse.Token;
                IsLoggedIn = true;
                CurrentUser = loginResponse.User;
                await _storage.SetAsync("user_session", loginResponse.User);
                await _storage.SetAsync("auth_token", loginResponse.Token);
                OnAuthStateChanged?.Invoke();
                return true;
            }
            catch { return false; }
        }

        public async Task RestoreSessionAsync()
        {
            try
            {
                var user = await _storage.GetAsync<UserDto>("user_session");
                var token = await _storage.GetAsync<string>("auth_token");
                if (user is not null && !string.IsNullOrEmpty(token))
                {
                    CurrentUser = user;
                    _tokenStore.Token = token;
                    IsLoggedIn = true;
                    OnAuthStateChanged?.Invoke();
                }
            }
            catch { }
        }

        public async Task LogoutAsync()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            _tokenStore.Token = null;
            await _storage.RemoveAsync("user_session");
            await _storage.RemoveAsync("auth_token");
            OnAuthStateChanged?.Invoke();
        }
    }
}
