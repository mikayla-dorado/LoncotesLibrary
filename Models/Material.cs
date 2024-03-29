using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace LoncotesLibrary.Models;

public class Material
{
    public int Id { get; set; }
    [Required]
    public string MaterialName { get; set; }
    [Required]
    public int MaterialTypeId { get; set; }
    [Required]
    public int GenreId { get; set; }
    public DateTime? OutOfCirculationSince { get; set; }
    public Genre Genre { get; set; }
    public MaterialType MaterialType { get; set; }
    public List<Checkout> Checkouts { get; set; }
}