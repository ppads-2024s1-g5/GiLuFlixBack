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
    [Column("titulo")]
    public string? titulo { get; set; }

    [Display(Name = "Autor")]
    [Column("autor")]
    public string? autor { get; set; }

    [Display(Name = "Editora")]
    [Column("editora")]
    public string? editora { get; set; }

    public Book(string tit, string aut){
      titulo = tit;
      autor = aut;
    }

    public Book(){}
}
