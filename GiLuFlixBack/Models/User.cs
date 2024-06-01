using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GiLuFlixBack.Models.ReviewDTO;
using System.Globalization;

namespace GiLuFlixBack.Models
{
    [Table("User")]
    public class User
    {
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

        [Column("UserRole")]
        public string Role; 
        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }
        public ICollection<User> Friends { get; set; }

        public ICollection<User> FriendshipRequests { get; set; }

        public ICollection<ReviewResponse> Reviews { get; set; }
        
        public bool isPasswordCorrect (string password)
        {
            return password == this.Password;
        }

        public void AddUserToFriends(User user)
        {
            if (FriendshipRequests.Contains(user))
            {
                Friends.Add(user);
                FriendshipRequests.Remove(user);
            }
        }
        
        public User (){}

        public User( int Id, string Name,int Age,string Email,string Role,bool RememberMe)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Role = Role;
            this.Email = Email;
            this.RememberMe = RememberMe;
        }
    }
}
