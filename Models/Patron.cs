using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models;

public class Patron
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
    public List<Checkout> Checkouts { get; set; }
    public decimal? Balance
  {
    get
    {
      // Sum up the fines for unpaid checkouts
      decimal unpaidFines = Checkouts
          .Where(c => !c.Paid && c.ReturnDate.HasValue) // Assuming fines are associated with returned checkouts
          .Sum(c => CalculateFine(c.CheckoutDate, c.ReturnDate.Value));

      return unpaidFines;
    }
  }
  private decimal CalculateFine(DateTime checkoutDate, DateTime returnDate)
  {
    // Implement your fine calculation logic here
    // For example, you could calculate fines based on the number of days overdue
    const decimal finePerDay = 0.50m; // Adjust this value based on your requirements

    int overdueDays = (int)(returnDate - checkoutDate).TotalDays;
    return overdueDays > 0 ? overdueDays * finePerDay : 0;
  }
}