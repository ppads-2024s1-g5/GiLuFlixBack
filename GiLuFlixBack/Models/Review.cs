using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System;

namespace GiLuFlixBack.Models;

[Table("Review")]
public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    public User user { get; set; } 
    public int ItemId { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public int Likes { get; set; }

}
