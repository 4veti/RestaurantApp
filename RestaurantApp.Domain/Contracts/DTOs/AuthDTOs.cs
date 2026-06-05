namespace RestaurantApp.Domain.Contracts.DTOs;

public class LoginRequestDTO
{
    public string RawSecret { get; set; } = string.Empty;
}

public class RefreshAccessTokenRequestDTO
{
    public string RefreshToken { get; set; } = string.Empty;
}
