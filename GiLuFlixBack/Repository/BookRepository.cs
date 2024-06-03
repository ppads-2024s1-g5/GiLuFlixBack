using GiLuFlixBack.Models;
using MySqlConnector;
using System.Data;
using Dapper;

namespace GiLuFlixBack.Repository;

public class BookRepository
{
        private readonly IDbConnection _dbConnection;


        public BookRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<ICollection<Book>> GetRecommendations(int id)
        {
            string query = @"WITH countries as (
                SELECT 
                author,
                count(1)
            FROM catalog1.Book 
                WHERE Id IN (SELECT DISTINCT ItemId FROM catalog1.Review WHERE UserId = @id)
            GROUP BY 1
                )
            SELECT * FROM catalog1.Book WHERE author IN (SELECT author from countries) LIMIT 5;";
            
            IEnumerable<Book> result = await _dbConnection.QueryAsync<Book>(query, new { id = id });
            ICollection<Book> recommendationCollection = result.ToList();
            return recommendationCollection;
        }
    }
    