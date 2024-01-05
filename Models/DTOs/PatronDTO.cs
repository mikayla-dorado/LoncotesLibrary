using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models.DTOs;

public class PatronDTO
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<CheckoutDTO> Checkouts { get; set; } = new List<CheckoutDTO>();
    public bool Paid { get; set; }
    public List<CheckoutWithLateFeeDTO> CheckoutsWithLateFee { get; set; } = new List<CheckoutWithLateFeeDTO>();
    public decimal? Balance
  {
    get
    {
      // total of unpaid fines a patron owes
      // filter out paid checkouts from checkouts

      List<CheckoutDTO> unpaidCheckouts = Checkouts.Where(co => !co.Paid).ToList();
      // compare checkoutswithlatefee and checkouts, and return list of cowlf that match co
      if (unpaidCheckouts == null)
      {
        return 0;
      }
      List<CheckoutWithLateFeeDTO> unpaidCheckoutsWithFees = CheckoutsWithLateFee
        .Where(clf => clf.Id != null && unpaidCheckouts.Select(uc => uc.Id).Contains(clf.Id))
        .ToList();

      var foundBalance = unpaidCheckoutsWithFees
        .Where(uco => uco.LateFee.HasValue)
        .Sum(co => co.LateFee.GetValueOrDefault());

      if (foundBalance == null)
      {
        return 0;
      } 
      else
      {
        return foundBalance;
      }
    }
  }
}