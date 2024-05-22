using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("Movie")]
public class Movie
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    [Display(Name = "Title")]
    [Column("Title")]
    public string? Title { get; set; }

    [Display(Name = "Director")]
    [Column("Director")]
    public string? Director { get; set; }

    [Display(Name = "Elenco Principal")]
    [Column("Cast")]
    public string? Cast { get; set; }

    [Display(Name = "Country")]
    [Column("Country")]
    public string? Country { get; set; }

    [Display(Name = "Year")]
    [Column("Year")]
    public int? Year { get; set; }

    public Movie(string titulo_, string diretor_){
      Title = titulo_;
      Director = diretor_;
    }

    public Movie(){}
}
