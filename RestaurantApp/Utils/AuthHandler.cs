using RestaurantApp.Services;
using System.Net.Http.Headers;

namespace RestaurantApp.Utils;

public class AuthHandler : DelegatingHandler
{
    private readonly TokenStore _tokenStore;
    private readonly AuthService _authService;

    public AuthHandler(TokenStore tokenStore, AuthService authService)
    {
        _authService = authService;
        _tokenStore = tokenStore;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);

        HttpResponseMessage response = await base.SendAsync(request, ct);

        if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
        {
            return response;
        }

        (bool refreshed, _) = await _authService.TryRefreshAsync();

        if (refreshed == false)
        {
            await Shell.Current.DisplayAlert("Грешка!", "Възникна проблем при автентикацията.", "Добре");
        }

        HttpRequestMessage retryRequest = new HttpRequestMessage(request.Method, request.RequestUri)
        {
            Content = request.Content
        };

        retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);

        return await base.SendAsync(retryRequest, ct);
    }
}
