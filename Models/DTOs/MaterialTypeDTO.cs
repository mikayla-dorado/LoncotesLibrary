using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models.DTOs;

public class MaterialTypeDTO
{
  [Required]
  public string Name { get; set; }
  [Required]
  public int CheckoutDays { get; set; }
  public int Id { get; set; }
}