using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models.ReviewDTO;
using GiLuFlixBack.Data;
using MySqlConnector;
using System.Data;
using Dapper;
using System;


namespace GiLuFlixBack.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IDbConnection _dbConnection;


        public ReviewRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> PostReview(ReviewForm review, string UserId)
        {
            _dbConnection?.Open();

            var parameters = new { UserId = UserId, ItemId = review.ItemId, Rating = review.Rating, ReviewText = review.ReviewText };
            Console.WriteLine(parameters);
            string query = @"INSERT INTO catalog1.Review (UserId, ItemId, Rating, ReviewText, DatetimeReview)
                             VALUES (@UserId, @ItemId, @Rating, @ReviewText, current_timestamp); ";

            Console.WriteLine("EXECUTANDO A QUERY\n" + query);                  
            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
            return rowsAffected;        
        }

        public async Task<ICollection<ReviewResponse>> GetAllItemReviews(int ItemId)
        {
            _dbConnection.Open();

            var parameters = new { ItemId = ItemId };

            string query = @"SELECT ReviewId, UserId, Name, ItemId, Rating, ReviewText, Likes, DatetimeReview FROM catalog1.Review A LEFT JOIN catalog1.User ON UserId = Id
                             WHERE ItemId = @ItemId;";
            var reviews = await _dbConnection.QueryAsync<ReviewResponse>(query, parameters);
            foreach (var item in reviews)
            {
                Console.WriteLine(item);
            }          

            _dbConnection.Close();
            return reviews.ToList();
        }

        public async Task<int> LikeComment(int id)
        {
            _dbConnection.Open();

            var parameters = new { ItemId = id };
            string query = @"UPDATE catalog1.Review SET likes = likes + 1 WHERE ItemId = @ItemId;";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);

            _dbConnection.Close();

            return rowsAffected;
        }
    }
}