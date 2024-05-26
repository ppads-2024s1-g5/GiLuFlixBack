using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models.ReviewDTO
{
    public class ReviewForm
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }

}

