using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("Filme")]
public class Filme
{
    [Column("id")]
    [Key]
    public string Id {get;set;}

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

    public Filme(string titulo_, string diretor_){
      titulo = titulo_;
      diretor = diretor_;
    }

    public Filme(){}
}
