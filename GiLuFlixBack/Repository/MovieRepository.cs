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
        public async Task<ICollection<Movie>> GetRecommendations(int id)
        {
            string query = @"WITH countries as (
                SELECT 
                country,
                count(1)
            FROM catalog1.Movie 
                WHERE Id IN (SELECT DISTINCT ItemId FROM catalog1.Review WHERE UserId = @id)
            GROUP BY 1
                )
            SELECT * FROM catalog1.Movie WHERE Country IN (SELECT Country from countries) LIMIT 5;";
            
            IEnumerable<Movie> result = await _dbConnection.QueryAsync<Movie>(query, new { id = id });
            ICollection<Movie> recommendationCollection = result.ToList();
            return recommendationCollection;
        }
    }
}
