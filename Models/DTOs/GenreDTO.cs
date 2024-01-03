using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models.DTOs;

public class GenreDTO
{
    [Required]
    public string Name { get; set; }
    public int Id { get; set; }
}