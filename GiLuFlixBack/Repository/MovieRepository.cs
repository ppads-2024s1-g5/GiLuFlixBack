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
    public class MovieRepository 
    //: IReviewRepository
    {
        private readonly IDbConnection _dbConnection;


        public MovieRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> GetReviews(string UserId)
        {
            _dbConnection?.Open();

            //var parameters = new { UserId = UserId, ItemId = review.ItemId, Rating = review.Rating, ReviewText = review.ReviewText };
            var parameters = "";
            string query = @"; ";

            var rowsAffected = await _dbConnection.ExecuteAsync(query, parameters);
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
            _dbConnection?.Close();
            
            return rowsAffected;        
        }
    }
}
