using Microsoft.Extensions.Options;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Utils;
using System.Diagnostics;
using System.Net.Http.Json;

using static RestaurantApp.Utils.Constants;

namespace RestaurantApp.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly TokenStore _tokenStore;
    private readonly ApplicationSettings _config;
    private const string AUTHORIZATION_CONTROLLER = "Authorization";

    public AuthService(TokenStore tokenStore, IOptions<ApplicationSettings> config)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri(config.Value.BaseAPIUrl) };
        _tokenStore = tokenStore;
        _config = config.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>On successful login - the terminal's type.</returns>
    public async Task<int> LoginAsync()
    {
        try
        {
            LoginRequestDTO loginDto = new LoginRequestDTO() { RawSecret = _config.RawSecret };
            string uri = $"{AUTHORIZATION_CONTROLLER}/{ApiRoutes.Login}";

            HttpResponseMessage? response = await _httpClient.PostAsync(uri, JsonContent.Create(loginDto));
        
            if (response?.Content is null || response.IsSuccessStatusCode == false)
            {
                return -1;
            }

            LoginResponseDTO? responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (responseDTO is null)
            {
                return -1;
            }
            
            _tokenStore.AccessToken = responseDTO.AccessToken;
            await SecureStorage.SetAsync(RefreshTokenKey, responseDTO.RawRefreshToken);

            return responseDTO.TerminalType;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An exception occurred while trying to Login: {ex.Message}");
            return -1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The refresh success status, and on success the terminal's type.</returns>
    public async Task<(bool, int)> TryRefreshAsync()
    {
        try
        {
            string refreshToken = await SecureStorage.GetAsync(RefreshTokenKey) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return (false, -1);
            }

            RefreshAccessTokenRequestDTO refreshDto = new RefreshAccessTokenRequestDTO() { RefreshToken = refreshToken };
            string uri = $"{AUTHORIZATION_CONTROLLER}/{ApiRoutes.RefreshAccessToken}";

            HttpResponseMessage? response = await _httpClient.PostAsync(uri, JsonContent.Create(refreshDto));

            if (response?.Content is null || response.IsSuccessStatusCode == false)
            {
                return (false, -1);
            }

            LoginResponseDTO? responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (responseDTO is null)
            {
                return (false, -1);
            }

            _tokenStore.AccessToken = responseDTO.AccessToken;
            await SecureStorage.SetAsync(RefreshTokenKey, responseDTO.RawRefreshToken);
            return (true, responseDTO.TerminalType);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An exception occurred while trying to Login: {ex.Message}");
            return (false, -1);
        }
    }
}
