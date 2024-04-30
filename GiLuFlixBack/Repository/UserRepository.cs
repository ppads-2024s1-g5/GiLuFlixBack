using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using MySqlConnector;
using System.Data;
using Dapper;


namespace GiLuFlixBack.Repository
{
    public class UserRepository : IUserRepository
    {
        //private static readonly Lazy<UserRepository> _instance = new Lazy<UserRepository>(() => new UserRepository());
        //private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;


        public UserRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<User> SearchByEmail(string email)
        {

            _dbConnection?.Open();

            string query = @"SELECT * FROM catalog1.User WHERE email = @email;";
            //query and return just a single row
            var user = await _dbConnection.QuerySingleAsync<User>(query, new { email = email });
            return user;
            
            _dbConnection?.Close();

        }
    }
}