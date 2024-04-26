


namespace GiluFlixBack.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly IConfiguration _configuration;

    public ReviewRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Review> PostReview(Review review)
    {
        string sql = @"INSERT INTO catalog1.Review (rating,reviewText) VALUES (@review.rating,@review.reviewText); ";
        using ( var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
                var review = new { rating = 5, reviewText = "Amei o filme" };
                var rowsAffected = conn.Execute(sql, Review);
                Console.WriteLine($"{rowsAffected} row(s) inserted.");
                
        }
    }
}