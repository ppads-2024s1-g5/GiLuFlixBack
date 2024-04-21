using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GiLuFlixBack.Models
{
    [Table("User")]
    public class User
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;

        [Column("name")]
        public string? name { get; set; }

        public int? age { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Column("userRole")]
        private string? _role { get; set; }
        
        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }

        //different way to write the get method :)
        public int Id
        {
            get => _id;
        }

        public bool isPasswordCorrect (string password)
        {
            return password == this.password;
        }

        public User() { }

    }
}