using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GiLuFlixBack.Models
{
    public class User
    {
        private int _id;
        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        private string _password { get; set; }

        private string _role { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }


        // get method
        public string GetPassword()
        {
            return _password;
        }

        //different way to write the get method :)
        public int Id
        {
            get => _id;
        }

        public User() { }

    }
}
