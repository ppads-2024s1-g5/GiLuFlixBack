using GiLuFlixBack.Models.ReviewDTO;
using MySqlConnector;
using System.Data;
using Dapper;


namespace GiLuFlixBack.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IDbConnection _dbConnection;

        public ReviewRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> PostReview(ReviewForm review, string userId)
        {
            _dbConnection?.Open();

            var parameters = new { UserId = userId, ItemId = review.ItemId, Rating = review.Rating, ReviewText = review.ReviewText };
            string query = @"INSERT INTO catalog1.Review (UserId, ItemId, Rating, ReviewText, DatetimeReview)
                             VALUES (@UserId, @ItemId, @Rating, @ReviewText, current_timestamp); ";
            
            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
            
            return rowsAffected;
        }
        

        public async Task<ICollection<ReviewResponse>> GetAllItemReviews(int itemId)
        {
            _dbConnection.Open();

            var parameters = new { ItemId = itemId };

            string query = @"SELECT ReviewId, UserId, Name, ItemId, Rating, ReviewText, Likes, DatetimeReview FROM catalog1.Review A LEFT JOIN catalog1.User ON UserId = Id
                             WHERE ItemId = @ItemId;";
            var reviews = await _dbConnection.QueryAsync<ReviewResponse>(query, parameters);
            var reviewResponses = reviews as ReviewResponse[] ?? reviews.ToArray();

            _dbConnection.Close();
            return reviewResponses.ToList();
        }

        public async Task<ICollection<ReviewResponse>> GetAllUserReviews(int userId)
        {
            _dbConnection.Open();

            var parameters = new { UserId = userId };

            string query = @"SELECT ReviewId, UserId, Name, ItemId, Rating, ReviewText, Likes, DatetimeReview FROM catalog1.Review A LEFT JOIN catalog1.User ON UserId = Id
                             WHERE UserId = @UserId;";
            var reviews = await _dbConnection.QueryAsync<ReviewResponse>(query, parameters);    

            _dbConnection.Close();
            return reviews.ToList();
        }

        public async Task<int> LikeComment(int id)
        {
            _dbConnection.Open();

            var parameters = new { ItemId = id };
            string query = @"UPDATE catalog1.Review SET Likes = Likes + 1 WHERE ReviewId = @ItemId;";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);

            _dbConnection.Close();

            return rowsAffected;
        }
    }
}