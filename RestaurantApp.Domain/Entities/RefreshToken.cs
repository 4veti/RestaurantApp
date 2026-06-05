using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Entities;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    public int TerminalId { get; set; }

    [ForeignKey(nameof(TerminalId))]
    public Terminal Terminal { get; set; } = null!;

    [Required]
    [StringLength(RefreshTokenMaxLength)]
    public string HashsedToken { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }
}