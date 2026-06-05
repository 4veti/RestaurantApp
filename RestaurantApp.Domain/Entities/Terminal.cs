using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Domain.Entities;

public class Terminal
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(TerminalDescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;
    
    public int TerminalTypeId { get; set; }

    [ForeignKey(nameof(TerminalTypeId))]
    public TerminalType TerminalType { get; set; } = null!;

    public string HashedSecret { get; set; } = string.Empty;

    public bool IsLockedOut { get; set; }
}
