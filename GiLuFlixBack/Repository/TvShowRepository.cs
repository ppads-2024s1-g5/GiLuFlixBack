using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using MySqlConnector;
using System.Data;
using Dapper;
using System;

namespace GiLuFlixBack.Repository;

public class TvShowRepository
{
    private readonly IDbConnection _dbConnection;


    public TvShowRepository(IConfiguration configuration)
    {
        _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    public async Task<ICollection<TvShow>> GetRecommendations(int id)
    {
        // var parameters = new DynamicParameters();
        // parameters.Add("@Id", id);
        string query = @"WITH countries as (
                SELECT 
                Director,
                count(1)
            FROM catalog1.TvShow 
                WHERE Id IN (SELECT DISTINCT ItemId FROM catalog1.Review WHERE UserId = @id)
            GROUP BY 1
                )
            SELECT * FROM catalog1.TvShow WHERE Director IN (SELECT Director from countries) LIMIT 5;";
            
        IEnumerable<TvShow> result = await _dbConnection.QueryAsync<TvShow>(query, new { id = id });
        ICollection<TvShow> recommendationCollection = result.ToList();
        return recommendationCollection;
    }
}