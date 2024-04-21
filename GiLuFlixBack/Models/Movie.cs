using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("Movie")]
public class Movie
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    [Display(Name = "Titulo")]
    [Column("titulo")]
    public string? titulo { get; set; }

    [Display(Name = "Diretor")]
    [Column("diretor")]
    public string? diretor { get; set; }

    [Display(Name = "Elenco Principal")]
    [Column("elenco_principal")]
    public string? elencoPrincipal { get; set; }

    [Display(Name = "Pais")]
    [Column("pais")]
    public string? pais { get; set; }

    [Display(Name = "Ano")]
    [Column("ano")]
    public int? ano { get; set; }

    public Movie(string titulo_, string diretor_){
      titulo = titulo_;
      diretor = diretor_;
    }

    public Movie(){}
}
