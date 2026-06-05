using RestaurantApp.Domain.Contracts.DTOs;

namespace RestaurantApp.Services.Contracts;

public interface IAuthorizationService
{
    public Task<(bool, LoginResponseDTO?)> Login(LoginRequestDTO request);
    public Task<(bool, LoginResponseDTO?)> RefreshAccessToken(RefreshAccessTokenRequestDTO request);
}
