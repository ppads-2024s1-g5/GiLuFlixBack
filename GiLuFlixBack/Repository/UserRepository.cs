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

        public async Task<int> Create(string name, string email, string password)
        {
            _dbConnection?.Open();

            var parameters = new { Name = name, Email = email, Password = password };            
            string query = @"INSERT INTO catalog1.User (Name,Email, Password)
                            VALUES (@Name,@Email,@Password);";
            
            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
            return rowsAffected;
            _dbConnection?.Close();

        }

        public async Task<User> GetUserById(int Id)
        {

            var parameters = new { UserId = Id };            
            string query = @"SELECT * FROM catalog1.User
                            WHERE Id = @UserId;";
            
            User user = await _dbConnection.QuerySingleAsync<User>(query, parameters);
            Console.WriteLine($"{user} returned.");
            _dbConnection?.Close();
            return user;
        }

        public async Task<int> requestFriendship(int requesterId,int requestedId )
        {
            int rowsAffected =  await _dbConnection.ExecuteAsync(
                "INSERT INTO FriendshipRequests (RequesterId, RecipientId) VALUES (@SolicitanteId, @DestinatarioId)",
                new { SolicitanteId = requesterId, DestinatarioId = requestedId }
            );

            return rowsAffected;
        }

    }
}