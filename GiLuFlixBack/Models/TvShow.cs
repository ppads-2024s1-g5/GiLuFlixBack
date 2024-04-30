using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("TvShow")]
public class TvShow
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

    [Display(Name = "Elenco principal")]
    [Column("elenco_principal")]
    public string? elenco_principal { get; set; }

    public TvShow(string tit, string dir){
      titulo = tit;
      diretor = dir;
    }

    public TvShow(){}
}
