using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models;

[Table("Review")]
public class Review
{

    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    public int UserId { get; set; }
    public int ItemId { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    
}