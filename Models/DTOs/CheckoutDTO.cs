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
  public bool Paid { get; set; }
// private static decimal _lateFeePerDay
//   {
//     get
//     {
//       return .50M;
//     }
//   }
//   public decimal? LateFee
//   {
//     get
//     {
//       DateTime dueDate = CheckoutDate.AddDays(Material.MaterialType.CheckoutDays);
//       DateTime returnDate = ReturnDate ?? DateTime.Today;
//       int daysLate = (returnDate - dueDate).Days;
//       decimal fee = daysLate * _lateFeePerDay;
//       return daysLate > 0 ? fee : null;
//     }
//   }
}