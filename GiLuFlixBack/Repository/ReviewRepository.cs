using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using MySqlConnector;
using Dapper;


namespace GiLuFlixBack.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IConfiguration _configuration;

        public ReviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> PostReview()
        {
            
            return 1;
            
            // var review = new { rating = 5, reviewText = "Amei o filme" };
            // string sql = @"INSERT INTO catalog1.Review (rating,reviewText) VALUES (@review.rating,@review.reviewText); ";
            // using ( var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            // {
            //         var rowsAffected = conn.Execute(sql, review);
            //         Console.WriteLine($"{rowsAffected} row(s) inserted.");
            //         return rowsAffected;
            // }
        }
    }
}