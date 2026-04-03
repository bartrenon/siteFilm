using System.ComponentModel.DataAnnotations;

namespace siteFilm.Models;

public class Film
{
   
   public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Titre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le genre est obligatoire")]
    public string Genre { get; set; } = string.Empty;

    [Range(1900,2030)]
    public int Annee { get; set; }
}
