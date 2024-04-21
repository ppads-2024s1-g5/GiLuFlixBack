using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using MySqlConnector;
using Dapper;


namespace GiLuFlixBack.Repository
{
    public class UserRepository : IUserRepository
    {
        //private static readonly Lazy<UserRepository> _instance = new Lazy<UserRepository>(() => new UserRepository());
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<User> SearchByEmail(string email)
        {
            string sql = @"SELECT * FROM catalog1.User WHERE UPPER(email) = @email;";
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                //query and return just a single row
                var user = await conn.QuerySingleAsync<User>(sql, new { email = email.ToUpper() });

                Console.WriteLine("ID: " +user.Id);
                Console.WriteLine("EMAIL: " +user.email);
                Console.WriteLine("NOME: " + user.name);
                Console.WriteLine("IDADE: " + user.age);
                Console.WriteLine("SENHA: " + user.password);    

                return user;

            }
        }
    }
}