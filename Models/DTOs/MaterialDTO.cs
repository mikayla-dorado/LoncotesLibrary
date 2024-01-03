using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models.DTOs;

public class MaterialDTO
{
    public int Id { get; set; }
  [Required]
  public string MaterialName { get; set; }
  [Required]
  public int MaterialTypeId { get; set; }
  [Required]
  public int GenreId { get; set; }
  public DateTime? OutofCirculationSince { get; set; }
  public MaterialTypeDTO MaterialType { get; set; }
  public GenreDTO Genre { get; set; }
}