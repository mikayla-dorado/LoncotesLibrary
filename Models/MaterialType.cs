using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models;

public class MaterialType
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int CheckoutDays { get; set; }
    public int Id { get; set; }
}