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
    [Column("Title")]
    public string? Title { get; set; }

    [Display(Name = "Diretor")]
    [Column("Director")]
    public string? Director { get; set; }

    [Display(Name = "Elenco principal")]
    [Column("Cast")]
    public string? Cast { get; set; }

    public TvShow(string tit, string dir){
      Title = tit;
      Director = dir;
    }

    public TvShow(){}
}
