namespace RestaurantApp.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantApp.Domain.Constants;

public class Food
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(FoodNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int NetGrams { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int FoodTypeId { get; set; }

    [ForeignKey(nameof(FoodTypeId))]
    public FoodType FoodType { get; set; } = null!;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Modified { get; set; }

    public IEnumerable<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();
}
