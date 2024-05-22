using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("Book")]
public class Book
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    [Display(Name = "Titulo")]
    [Column("Title")]
    public string? Title { get; set; }

    [Display(Name = "Autor")]
    [Column("Author")]
    public string? Author { get; set; }

    [Display(Name = "Editora")]
    [Column("Publisher")]
    public string? Publisher { get; set; }

    public Book(string tit, string aut){
      Title = tit;
      Author = aut;
    }

    public Book(){}
}
