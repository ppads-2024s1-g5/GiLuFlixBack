using GiLuFlixBack.Models;

namespace GiLuFlixBack.Repository;

public interface IReviewRepository
{
    Task<Review> PostReview(Review review)
}