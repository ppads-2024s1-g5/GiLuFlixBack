using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
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

        public async Task<int> PostReview(Review review, string UserId)
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
        // finally
        // {
        //     _dbConnection?.Close();
        // }
    }
}