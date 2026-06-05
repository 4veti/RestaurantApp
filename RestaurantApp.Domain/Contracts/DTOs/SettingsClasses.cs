namespace RestaurantApp.Domain.Contracts.DTOs;

public class JwtOptions
{
    public string SigningSecret { get; set; } = string.Empty;
    public int AccessTokenExpiryMinutes { get; set; }
    public int RefreshTokenExpiryHours { get; set; }
}

public class TerminalSecrets
{
    public Dictionary<string, string> Secrets { get; set; } = new();
}