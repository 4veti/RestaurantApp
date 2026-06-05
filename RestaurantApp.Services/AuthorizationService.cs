using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantApp.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly JwtOptions _options;
    private readonly TerminalSecrets _terminalSecrets;

    public AuthorizationService(IRepositoryManager repositoryManager,
        IOptions<JwtOptions> authOptions,
        IOptions<TerminalSecrets> terminalSecrets)
    {
        _repositoryManager = repositoryManager;
        _options = authOptions.Value;
        _terminalSecrets = terminalSecrets.Value;
    }

    public async Task<(bool, LoginResponseDTO?)> Login(LoginRequestDTO request)
    {
        Terminal? terminal = await _repositoryManager.TerminalRepository.GetBySecretAsync(ComputeSha256Lowered(request.RawSecret), asNoTracking: true);

        if (terminal is null || terminal.IsLockedOut)
        {
            return (false, null);
        }

        string accessToken = GenerateJwt(terminal);
        string rawRefreshToken = GenerateSecureRandomString();

        _repositoryManager.RefreshTokenRepository.Insert(new RefreshToken()
        {
            TerminalId = terminal.Id,
            HashsedToken = ComputeSha256Lowered(rawRefreshToken),
            ExpiresAt = DateTime.UtcNow.AddHours(_options.RefreshTokenExpiryHours)
        });

        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return (true, new LoginResponseDTO()
        {
            AccessToken = accessToken,
            RawRefreshToken = rawRefreshToken,
            TerminalType = terminal.TerminalTypeId
        });
    }
    
    public async Task<(bool, LoginResponseDTO?)> RefreshAccessToken(RefreshAccessTokenRequestDTO request)
    {
        RefreshToken? token = await _repositoryManager.RefreshTokenRepository
            .GetAll()
            .Include(t => t.Terminal)
                .ThenInclude(t => t.TerminalType)
            .Where(t => t.HashsedToken == ComputeSha256Lowered(request.RefreshToken)
                     && t.ExpiresAt > DateTime.UtcNow
                     && t.IsRevoked == false)
            .FirstOrDefaultAsync();

        if (token is null)
        {
            return (false, null);
        }

        Terminal terminal = token.Terminal;

        if (terminal.IsLockedOut)
        {
            return (false, null);
        }

        string accessToken = GenerateJwt(terminal);
        string rawRefreshToken = GenerateSecureRandomString();

        _repositoryManager.RefreshTokenRepository.Insert(new RefreshToken()
        {
            TerminalId = terminal.Id,
            HashsedToken = ComputeSha256Lowered(rawRefreshToken),
            ExpiresAt = DateTime.UtcNow.AddHours(_options.RefreshTokenExpiryHours)
        });

        _repositoryManager.RefreshTokenRepository.Remove(token);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return (true, new LoginResponseDTO()
        {
            AccessToken = accessToken,
            RawRefreshToken = rawRefreshToken,
            TerminalType = terminal.TerminalTypeId
        });
    }

    private string GenerateJwt(Terminal terminal)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.SigningSecret));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(ClaimTypes.Role, terminal.TerminalType.Name),
            new Claim("terminalId", terminal.Id.ToString()),
        ];

        JwtSecurityToken token = new(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateSecureRandomString()
    {
        byte[] bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string ComputeSha256Lowered(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash).ToLower();
    }
}
