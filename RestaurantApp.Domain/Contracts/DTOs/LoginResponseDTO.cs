namespace RestaurantApp.Domain.Contracts.DTOs;

public class LoginResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RawRefreshToken { get; set; } = string.Empty;
    public int TerminalType { get; set; }
}
