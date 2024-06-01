using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
using GiLuFlixBack.Models.ReviewDTO;
using GiLuFlixBack.Data;
using MySqlConnector;
using System.Data;
using Dapper;


namespace GiLuFlixBack.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<string> GetUserRole(string email)
        {
            _dbConnection?.Open();
            string query = @"SELECT UserRole FROM catalog1.User WHERE email = @email;";
            string role = await _dbConnection.QuerySingleAsync<string>(query, new { email = email });
            _dbConnection?.Close();
            return role;
        }
        public async Task<User> SearchByEmail(string email)
        {
            _dbConnection?.Open();

            string query = @"SELECT * FROM catalog1.User WHERE email = @email;";
            var user = await _dbConnection.QuerySingleAsync<User>(query, new { email = email });
            Console.WriteLine(user.RememberMe);
            Console.WriteLine(user.Role);
            Console.WriteLine(user.Name);
            _dbConnection?.Close();
            return user;
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
        public async Task<ICollection<ReviewResponse>> GetAllUserReviews(int Id)
        {
            var parameters = new { UserId = Id };            
            string query = @"SELECT ReviewId, UserId, Name, ItemId, Rating, ReviewText, Likes, DatetimeReview FROM catalog1.Review A LEFT JOIN catalog1.User ON UserId = Id
                             WHERE UserId = @UserId;";
            var reviews = await _dbConnection.QueryAsync<ReviewResponse>(query, parameters);
            return reviews.ToList();
        }
        
        public async Task<User> GetUserById(int Id)
        {

            var parameters = new { UserId = Id };            
            string query = @"SELECT * FROM catalog1.User A WHERE Id = @UserId;";
            User user = await _dbConnection.QuerySingleAsync<User>(query, parameters);
            
            user.Reviews = await GetAllUserReviews(Id);
            user.FriendshipRequests = await GetFriendshipRequests(Id);
            user.Friends = await GetFriends(Id);

            foreach (var item in user.Friends)
            {
                Console.WriteLine($"{item.Name} - friend name list.");
            }
            _dbConnection?.Close();
            return user;
        }

        public async Task<int> requestFriendship(int requesterId,int requestedId )
        {
            // Verificar se a amizade já existe na tabela
            bool friendshipExists = await _dbConnection.ExecuteScalarAsync<bool>(
                @"SELECT COUNT(*) FROM catalog1.FriendshipRequests 
                      WHERE (RequesterId = @RequesterId AND RecipientId = @RecipientId)
                      OR (RequesterId = @RecipientId AND RecipientId = @RequesterId)",
                new { RequesterId = requesterId, RecipientId = requestedId }
            );
            if (friendshipExists)
            {
                return -1;
            }
            int rowsAffected =  await _dbConnection.ExecuteAsync(
                @"INSERT INTO catalog1.FriendshipRequests (RequesterId, RecipientId) VALUES (@SolicitanteId, @DestinatarioId);",
                new { SolicitanteId = requesterId, DestinatarioId = requestedId }
            );

            return rowsAffected;
        }

        public async Task<int> acceptFriendship(int user1,int user2 )
        {
            int rowsAffected =  await _dbConnection.ExecuteAsync(
                @"INSERT INTO catalog1.Friendships (UserId1, UserId2) VALUES (@user1, @user2);
                  DELETE FROM catalog1.FriendshipRequests 
                  WHERE (RequesterId = @user1 and RecipientId = @user2) OR (RequesterId = @user2 and RecipientId = @user1) "
            );
            Console.WriteLine("LINHAS AFETADAS" + rowsAffected);
            return rowsAffected;
        }


        public async Task<ICollection<User>> GetFriendshipRequests(int requesterId)
        {
            IEnumerable<User> usersEnumerable = await _dbConnection.QueryAsync<User>(
                @"SELECT * FROM User 
                WHERE Id IN (SELECT RequesterId FROM catalog1.FriendshipRequests WHERE RecipientId = @requesterId);",
                new { requesterId = requesterId }
            );
            ICollection<User> usersCollection = usersEnumerable.ToList();

            return usersCollection;
        }

        public async Task<ICollection<User>> GetFriends(int requesterId)
        {
            IEnumerable<User> usersEnumerable = await _dbConnection.QueryAsync<User>(
                @"SELECT * FROM User 
                Where Id IN (SELECT UserId2 FROM catalog1.Friendships WHERE @requesterId = UserId1)
                OR Id IN (SELECT UserId1 FROM catalog1.Friendships WHERE @requesterId = UserId2); ",
                new { requesterId = requesterId }
            );
            ICollection<User> usersCollection = usersEnumerable.ToList();
            Console.WriteLine(usersEnumerable);
            return usersCollection;
        }
    }
}