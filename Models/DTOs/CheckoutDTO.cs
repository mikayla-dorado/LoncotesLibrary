using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models.DTOs;

public class CheckoutDTO
{
    public int Id { get; set; }
  [Required]
  public int MaterialId { get; set; }
  [Required]
  public int PatronId { get; set; }
  [Required]
  public DateTime CheckoutDate { get; set; }
  public DateTime? ReturnDate { get; set; }
  public MaterialDTO Material { get; set; }
  public PatronDTO Patron { get; set; }
}