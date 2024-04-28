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
    public int reviewId { get; set; }

    public int userId { get; set; }
    public int itemId { get; set; }
    public int rating { get; set; }
    public string reviewText { get; set; }
    
}