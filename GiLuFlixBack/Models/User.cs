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
        public int Id;

        [Column("Name")]
        public string? Name { get; set; }

        public int? Age { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Column("userRole")]
        private string? _role { get; set; }
        
        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }

        //different way to write the get method :)
        // public int Id
        // {
        //     get => _id;
        // }

        public bool isPasswordCorrect (string password)
        {
            return password == this.Password;
        }

        public User() { }

    }
}